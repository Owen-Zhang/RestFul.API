namespace RestFul.API.Http
{
   public class HttpMethods
    {
        public const string Get = "GET";
        public const string Put = "PUT";
        public const string Post = "POST";
        public const string Delete = "DELETE";
        public const string Head = "HEAD";
        public const string Options = "OPTIONS";
        public const string Patch = "PATCH";

        public static string GetHttpMethod(string httpMethod)
        {
            switch (httpMethod.ToUpper())
            {
                case Get:
                    return Get;
                case Put:
                    return Put;
                case Post:
                    return Post;
                case Delete:
                    return Delete;
                case Patch:
                    return Patch;
                case Head:
                    return Head;
            }

            return string.Empty;
        }
    }
}
