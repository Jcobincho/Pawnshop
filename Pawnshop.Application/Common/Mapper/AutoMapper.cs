using Pawnshop.Application.Common.Attributes;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace Pawnshop.Application.Common.Mapper
{
    public static class AutoMapper
    {
        private static readonly Dictionary<(Type, Type), Expression> _expressionCache = new();
        private static readonly Dictionary<(Type, Type), Delegate> _delegateCache = new();

        public static IQueryable<TDestination> MapTo<TDestination>(this IQueryable query)
        {
            var sourceType = query.ElementType;
            var destinationType = typeof(TDestination);
            var expression = GetMappingExpression(sourceType, destinationType);

            var selectMethod = typeof(Queryable).GetMethods()
                .First(x => x.Name == "Select" &&
                            x.GetParameters().Length == 2 &&
                            x.GetParameters()[1].ParameterType.GetGenericArguments()[0].GetGenericArguments().Length == 2
                      )
                .MakeGenericMethod(sourceType, destinationType);

            return (IQueryable<TDestination>)selectMethod.Invoke(null, new object[] { query, expression });
        }

        public static List<TDestination> MapTo<TDestination>(this IEnumerable source)
        {
            var sourceType = source.GetType().GetGenericArguments()[0];
            var destinationType = typeof(TDestination);

            var func = GetCompiledMapperFunc(sourceType, destinationType);

            var selectMethod = typeof(Enumerable).GetMethods()
                                                 .First(m =>
                                                            m.Name == "Select" &&
                                                            m.GetParameters().Length == 2
                                                       )
                                                 .MakeGenericMethod(sourceType, destinationType);

            var mappedEnum = selectMethod.Invoke(null, new object[] { source, func });
            var toListMethod = typeof(Enumerable).GetMethod("ToList").MakeGenericMethod(destinationType);

            return (List<TDestination>)toListMethod.Invoke(null, new object[] { mappedEnum });
        }

        public static TDestination MapTo<TDestination>(this object source) where TDestination : new()
        {
            if (source == null) return default;
            var sourceType = source.GetType();
            var destinationType = typeof(TDestination);

            var func = (Func<object, TDestination>)GetCompiledMapperFunc(sourceType, destinationType, wrapForObject: true);
            return func(source);
        }

        private static Delegate GetCompiledMapperFunc(Type sourceType, Type destinationType, bool wrapForObject = false)
        {
            var key = (sourceType, destinationType);
            if (_delegateCache.TryGetValue(key, out var cached))
                return cached;

            var expression = (LambdaExpression)GetMappingExpression(sourceType, destinationType);

            if (wrapForObject)
            {
                var param = Expression.Parameter(typeof(object), "src");
                var convertedParam = Expression.Convert(param, sourceType);
                var invoke = Expression.Invoke(expression, convertedParam);

                var delegateType = typeof(Func<,>).MakeGenericType(typeof(object), destinationType);
                var lambda = Expression.Lambda(delegateType, invoke, param);

                var compiled = lambda.Compile();
                _delegateCache[key] = compiled;
                return compiled;
            }
            else
            {
                var compiled = expression.Compile();
                _delegateCache[key] = compiled;
                return compiled;
            }
        }

        private static Expression GetMappingExpression(Type sourceType, Type destinationType)
        {
            var key = (sourceType, destinationType);
            if (_expressionCache.TryGetValue(key, out var cached))
                return cached;

            var parameter = Expression.Parameter(sourceType, "x");
            var bindings = new List<MemberBinding>();

            foreach (var destinationProp in destinationType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!destinationProp.CanWrite) continue;

                string sourcePath = destinationProp.Name;
                var mapSourceAttr = destinationProp.GetCustomAttribute<MapSourceAttribute>();
                if (mapSourceAttr != null) sourcePath = mapSourceAttr.SourceName;

                Expression sourcePropertyAccess;
                Type sourcePropType;

                try
                {
                    sourcePropertyAccess = GetSourceExpression(parameter, sourcePath);
                    sourcePropType = sourcePropertyAccess.Type;
                }
                catch { continue; }

                Expression propertyExpression;

                if (IsCollection(sourcePropType) && IsCollection(destinationProp.PropertyType))
                {
                    var sourceItemType = GetCollectionItemType(sourcePropType);
                    var destItemType = GetCollectionItemType(destinationProp.PropertyType);
                    var innerMap = (LambdaExpression)GetMappingExpression(sourceItemType, destItemType);

                    var selectMethod = typeof(Enumerable).GetMethods()
                                                         .First(m =>
                                                                     m.Name == "Select" &&
                                                                     m.GetParameters().Length == 2
                                                               )
                                                         .MakeGenericMethod(sourceItemType, destItemType);

                    var selectCall = Expression.Call(selectMethod, sourcePropertyAccess, innerMap);
                    var toListMethod = typeof(Enumerable).GetMethod("ToList").MakeGenericMethod(destItemType);

                    propertyExpression = Expression.Call(toListMethod, selectCall);
                }
                else if (sourcePropType.IsClass && destinationProp.PropertyType.IsClass && sourcePropType != typeof(string))
                {
                    var innerExpressionLambda = (LambdaExpression)GetMappingExpression(sourcePropType, destinationProp.PropertyType);
                    var nullCheck = Expression.Equal(sourcePropertyAccess, Expression.Constant(null));
                    var parsedInner = new ParameterReplace(innerExpressionLambda.Parameters[0], sourcePropertyAccess).Visit(innerExpressionLambda.Body);

                    propertyExpression = Expression.Condition(nullCheck, Expression.Constant(null, destinationProp.PropertyType), parsedInner);
                }
                else
                {
                    if (sourcePropType != destinationProp.PropertyType)
                    {
                        if (destinationProp.PropertyType.IsAssignableFrom(sourcePropType))
                            propertyExpression = sourcePropertyAccess;
                        else
                        {
                            try { propertyExpression = Expression.Convert(sourcePropertyAccess, destinationProp.PropertyType); }
                            catch { continue; }
                        }
                    }
                    else
                    {
                        propertyExpression = sourcePropertyAccess;
                    }
                }
                bindings.Add(Expression.Bind(destinationProp, propertyExpression));
            }

            var body = Expression.MemberInit(Expression.New(destinationType), bindings);
            var lambda = Expression.Lambda(body, parameter);

            _expressionCache[key] = lambda;
            return lambda;
        }

        private static Expression GetSourceExpression(Expression parameter, string path)
        {
            Expression expression = parameter;
            foreach (var propertyName in path.Split('.'))
            {
                var property = expression.Type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (property == null) throw new Exception();
                expression = Expression.Property(expression, property);
            }
            return expression;
        }

        private static bool IsCollection(Type type)
        {
            return type.IsGenericType &&
                   (
                        type.GetGenericTypeDefinition() == typeof(List<>) ||
                        type.GetGenericTypeDefinition() == typeof(IEnumerable<>) ||
                        type.GetGenericTypeDefinition() == typeof(ICollection<>) ||
                        type.GetInterfaces().Any(i =>
                                                      i.IsGenericType &&
                                                      i.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                                                )
                   );
        }

        private static Type GetCollectionItemType(Type type)
        {
            if (type.IsArray) return type.GetElementType();
            if (type.IsGenericType) return type.GetGenericArguments()[0];
            return type.GetInterfaces()
                       .Where(i =>
                                    i.IsGenericType &&
                                    i.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                             )
                       .Select(i => i.GetGenericArguments()[0])
                       .FirstOrDefault();
        }

        private class ParameterReplace : ExpressionVisitor
        {
            private readonly ParameterExpression _oldParameter;
            private readonly Expression _replacement;

            public ParameterReplace(ParameterExpression oldParameter, Expression replacement)
            {
                _oldParameter = oldParameter;
                _replacement = replacement;
            }
            protected override Expression VisitParameter(ParameterExpression parameter)
            {
                return parameter == _oldParameter ? _replacement : base.VisitParameter(parameter);
            }
        }
    }
}
