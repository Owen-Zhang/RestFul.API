using System;
namespace RestFul.API.Extensions
{
    public static class ObjectExtensions
    {
        public static void ThrowIfNull(this object obj, string varName)
        {
            if (obj == null)
                throw new ArgumentNullException(varName ?? "object");
        }

        public static string ToJson(this object obj)
        {
            if (obj == null)
                return "{}";

            return ServiceStack.Text.JsonSerializer.SerializeToString(obj);
        }
    }
}
