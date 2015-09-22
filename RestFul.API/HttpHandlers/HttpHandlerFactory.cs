using System.Web;

namespace RestFul.API.HttpHandlers
{
    public class HttpHandlerFactory : IHttpHandlerFactory
    {
        /*将访问指向不同的handller*/
        public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            throw new System.NotImplementedException();
        }

        public void ReleaseHandler(IHttpHandler handler)
        {
            throw new System.NotImplementedException();
        }
    }
}
