using RestFul.API.Host;

namespace RestFul.API.HttpHandlers
{
    public class RestHandler : HttpHandlerBase
    {
        public string RequestName { get; set; }

        public RestHandler() : base(null) { }

        public RestPath RestPath { get; set; }

        public static RestPath FindMatchingRestPath(string httpMethod, string pathInfo)
        {
            return null;
        }
    }
}
