using IronXL;
using SWECVI.ApplicationCore.Entities;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Serialization;

namespace SWECVI.ApplicationCore.Utilities
{
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> AndAlso<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static T FromXml<T>(this string xml)
        {
            T returnedXmlClass = default(T);
            try
            {
                using (TextReader reader = new StringReader(xml))
                {
                    try
                    {
                        returnedXmlClass = (T)new XmlSerializer(typeof(T)).Deserialize(reader);
                    }
                    catch (InvalidOperationException e)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
            return returnedXmlClass;
        }

        public static Expression<Func<TItem, object>> GroupByExpression<TItem>(string[] propertyNames)
        {
            var properties = propertyNames.Select(name => typeof(TItem).GetProperty(name)).ToArray();
            var propertyTypes = properties.Select(p => p.PropertyType).ToArray();
            var tupleTypeDefinition = typeof(Tuple).Assembly.GetType("System.Tuple`" + properties.Length);
            var tupleType = tupleTypeDefinition.MakeGenericType(propertyTypes);
            var constructor = tupleType.GetConstructor(propertyTypes);
            var param = Expression.Parameter(typeof(TItem), "item");
            var body = Expression.New(constructor, properties.Select(p => Expression.Property(param, p)));
            var expr = Expression.Lambda<Func<TItem, object>>(body, param);
            return expr;
        }

        public static string ConvertValue(string unit, double? value)
        {
            if (unit == "%" || unit == "cm")
            {
                return (value * 100).ToString();
            }
            else if (unit == "ml")
            {
                return (value * 1000000).ToString();
            }
            else if (unit == "cm2")
            {
                return (value * 10000).ToString();
            }
            else if (unit == "g" || unit == "mm")
            {
                return (value * 1000).ToString();
            }

            return value.ToString();

        }

      
        public static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            if (!string.IsNullOrEmpty(property))
            {
                methodName = methodName == "DESC" ? "OrderByDescending" : "OrderBy";

                string[] props = property.Split('.');
                Type type = typeof(T);
                ParameterExpression arg = Expression.Parameter(type, "x");
                Expression expr = arg;
                foreach (string prop in props)
                {
                    PropertyInfo pi = type.GetProperty(prop);
                    if (pi == null)
                        pi = type.GetProperties().FirstOrDefault(x => x.Name.ToLower() == prop.ToLower());
                    expr = Expression.Property(expr, pi);
                    type = pi.PropertyType;
                }
                Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
                LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);
                object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                    && method.IsGenericMethodDefinition
                    && method.GetGenericArguments().Length == 2
                    && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
                return (IOrderedQueryable<T>)result;
            }

            return (IOrderedQueryable<T>)source;
        }
    }
}
