using System;
using System.Linq.Expressions;
using System.Reflection;

namespace RestFul.API.Common
{
    public delegate void PropertySetterDelegate(object instance, object value);
    public delegate object PropertyGetterDelegate(object instance);

    public static class PropertyInvoker
    {
        public static PropertySetterDelegate GetPropertySetterFn(this PropertyInfo propertyInfo)
        {
            var propertySetMethod = propertyInfo.GetSetMethod();
            if (propertySetMethod == null) return null;

            var instance = Expression.Parameter(typeof(object), "i");
            var argument = Expression.Parameter(typeof(object), "a");

            var instanceParam = Expression.Convert(instance, propertyInfo.DeclaringType);
            var valueParam = Expression.Convert(argument, propertyInfo.PropertyType);

            var setterCall = Expression.Call(instanceParam, propertyInfo.GetSetMethod(), valueParam);

            return Expression.Lambda<PropertySetterDelegate>(setterCall, instance, argument).Compile();

        }

        public static PropertyGetterDelegate GetPropertyGetterFn(this PropertyInfo propertyInfo)
        {
            var getMethodInfo = propertyInfo.GetGetMethod();
            if (getMethodInfo == null) return null;
            try
            {
                var oInstanceParam = Expression.Parameter(typeof(object), "oInstanceParam");
                var instanceParam = Expression.Convert(oInstanceParam, propertyInfo.DeclaringType);

                var exprCallPropertyGetFn = Expression.Call(instanceParam, getMethodInfo);
                var oExprCallPropertyGetFn = Expression.Convert(exprCallPropertyGetFn, typeof(object));

                var propertyGetFn = Expression.Lambda<PropertyGetterDelegate>
                    (
                        oExprCallPropertyGetFn,
                        oInstanceParam
                    ).Compile();

                return propertyGetFn;

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }
    }
}
