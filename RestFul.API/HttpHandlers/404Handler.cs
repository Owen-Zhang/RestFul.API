using System.Text;
using RestFul.API.Extensions;
using ServiceStack.Text;

namespace RestFul.API.HttpHandlers
{
    public class _404Handler : HttpHandlerBase
    {
        public _404Handler(): base(null) {}

        public override void ProcessRequest(AspNetRequest httpReq, AspNetResponse httpRes, string operationName)
        {
            var text = new StringBuilder("Request Url not found: \n")
                .AppendFormat("HttpMethod: {0} \n", httpReq.HttpMethod)
                .AppendFormat("PathInfo: {0} \n", httpReq.PathInfo)
                .AppendFormat("QueryString: {0} \n", httpReq.QueryString)
                .AppendFormat("RawUrl: {0} \n", httpReq.RawUrl)
                .ToString();

            httpRes.ContentType = "text/plain";
            httpRes.StatusCode = 404;
            httpRes.Write(text);
            httpRes.End();
        }
    }
}
