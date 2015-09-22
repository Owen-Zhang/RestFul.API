using RestFul.API.Extensions;

namespace RestFul.API.HttpHandlers
{
    public class ForbiddenHttpHandler : HttpHandlerBase
    {
        public ForbiddenHttpHandler() : base(null) { }

        public override void ProcessRequest(AspNetRequest httpReq, AspNetResponse httpRes, string operationName)
        {
            
        }
    }
}
