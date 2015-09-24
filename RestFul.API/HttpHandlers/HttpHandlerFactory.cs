using System.Web;
using RestFul.API.Extensions;

namespace RestFul.API.HttpHandlers
{
    public class HttpHandlerFactory : IHttpHandlerFactory
    {
        static HttpHandlerFactory()
        { 
            
        }

        /*将访问指向不同的handller*/
        public  IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            var request = context.Request;
            var pathInfo = request.GetPathInfo();

            /*对于一个纯API来说，此时没有用，可以返回404什么的, 其实也可以返回所有的接口说明：呵呵*/
            if (string.Equals(url, "/default.aspx", System.StringComparison.OrdinalIgnoreCase))
                pathInfo = "/";

            return GetHandlerForPathInfo(
                        request.HttpMethod, pathInfo, request.FilePath, pathTranslated) 
                   ?? new _404Handler();

        }

        public static IHttpHandler GetHandlerForPathInfo(string httpMethod, string pathInfo, string requestPath, string filePath)
        {
            var pathParts = pathInfo.TrimStart('/').Split('/');
            if (pathParts.Length == 0)
                return new _404Handler();

            /*如果是静态文件，而且是可以下载的文件类型，返回staticFileHandler*/

            /*从RestHandler中去找，如果存在访问RestHandler*/
            var restPath = RestHandler.FindMatchingRestPath(httpMethod, pathInfo);
            if (restPath != null)
                return new RestHandler { RestPath = restPath, RequestName = restPath.RequestType.Name };

            return new _404Handler();
        }

        public void ReleaseHandler(IHttpHandler handler)
        {
            
        }
    }
}
