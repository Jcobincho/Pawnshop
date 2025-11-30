using System.Linq.Expressions;
using System.Reflection;

namespace Pawnshop.Application.Common.Mapper
{
    public static class AutoMapper
    {
        public static readonly Dictionary<(Type, Type), object> _expressionCache = new Dictionary<(Type, Type), object>();

        public static IQueryable<TDestination> MapTo<TDestination>(this IQueryable query)
        {
            var sourceType = query.ElementType;
            var destinationType = typeof(TDestination);

            var expression = GetMappingExpression(sourceType, destinationType);

            var select = typeof(Queryable).GetMethods()
                                          .First(x => 
                                                      x.Name == "Select" &&
                                                      x.GetParameters().Last().ParameterType.GetGenericArguments().Length == 2
                                                )
                                          .MakeGenericMethod(sourceType, destinationType);

            return (IQueryable<TDestination>)select.Invoke(null, new object[] { query, expression });
        }

        private static Expression GetMappingExpression(Type sourceType, Type destinationType)
        {
            var key = (sourceType, destinationType);
            if (_expressionCache.TryGetValue(key, out var cached))
                return (Expression)cached;

            var parameter = Expression.Parameter(sourceType, "x");
            var bindings = new List<MemberBinding>();

            foreach(var destinationProp in destinationType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if(!destinationProp.CanWrite) continue;

                var sourceProp = sourceType.GetProperty(destinationProp.Name);
                if(sourceProp == null) continue;

                Expression propertyExpression;

                if (IsCollection(sourceProp.PropertyType) && IsCollection(destinationProp.PropertyType))
                {
                    var sourceItemType = sourceProp.PropertyType.GetGenericArguments()[0];
                    var destItemType = destinationProp.PropertyType.GetGenericArguments()[0];

                    var innerMap = GetMappingExpression(sourceItemType, destItemType);

                    var sourcePropertyAccess = Expression.MakeMemberAccess(parameter, sourceProp);

                    var selectMethod = typeof(Enumerable).GetMethods()
                        .First(m => m.Name == "Select" && m.GetParameters().Length == 2)
                        .MakeGenericMethod(sourceItemType, destItemType);

                    var selectCall = Expression.Call(selectMethod, sourcePropertyAccess, innerMap);

                    var toListMethod = typeof(Enumerable).GetMethod("ToList")
                        .MakeGenericMethod(destItemType);

                    propertyExpression = Expression.Call(toListMethod, selectCall);
                }
                else if (sourceProp.PropertyType.IsClass && destinationProp.PropertyType.IsClass && sourceProp.PropertyType != typeof(string))
                {
                    var innerExpressionLambda = (LambdaExpression)GetMappingExpression(sourceProp.PropertyType, destinationProp.PropertyType);

                    var sourcePropertyAccess = Expression.MakeMemberAccess(parameter, sourceProp);

                    var nullCheck = Expression.Equal(sourcePropertyAccess, Expression.Constant(null));
                    var parsedInner = new ParameterReplace(innerExpressionLambda.Parameters[0], sourcePropertyAccess)
                                        .Visit(innerExpressionLambda.Body);

                    propertyExpression = Expression.Condition(nullCheck,
                        Expression.Constant(null, destinationProp.PropertyType),
                        parsedInner);
                }
                else
                {
                    propertyExpression = Expression.MakeMemberAccess(parameter, sourceProp);
                }

                bindings.Add(Expression.Bind(destinationProp, propertyExpression));
            }

            var body = Expression.MemberInit(Expression.New(destinationType), bindings);
            var lambda = Expression.Lambda(body, parameter);

            _expressionCache[key] = lambda;
            return lambda;
        }

        private static bool IsCollection(Type type)
        {
            return type.IsGenericType && (
                   type.GetGenericTypeDefinition() == typeof(List<>) ||
                   type.GetGenericTypeDefinition() == typeof(IEnumerable<>) ||
                   type.GetGenericTypeDefinition() == typeof(ICollection<>)
                                         );
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
