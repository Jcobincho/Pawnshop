using System.Reflection;

namespace Pawnshop.Web.Components.Common
{
    public static class QueryStringExtensions
    {
        public static Dictionary<string, string> ToQueryDictionary(this object obj, string prefix = "")
        {
            var dict = new Dictionary<string, string>();
            if (obj == null) return null;

            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(obj);
                if (value == null) continue;

                var propName = string.IsNullOrEmpty(prefix) ? prop.Name : $"{prefix}.{prop.Name}";
                var type = prop.PropertyType;

                if (type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(decimal) || type == typeof(DateTime))
                {
                    dict.Add(propName, value.ToString());
                }
                else if (type.IsClass)
                {
                    var nestedDict = value.ToQueryDictionary(propName);
                    foreach (var kvp in nestedDict)
                    {
                        dict.Add(kvp.Key, kvp.Value);
                    }
                }
            }
            return dict;
        }
    }
}