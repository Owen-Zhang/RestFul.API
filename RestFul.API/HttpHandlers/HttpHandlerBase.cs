using System;
using System.Web;
using RestFul.API.Extensions;

namespace RestFul.API.HttpHandlers
{
    public class HttpHandlerBase : IHttpHandler
    {
        private string requestName { get; set; }

        public HttpHandlerBase(string requestName)
        {
            this.requestName = requestName;
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public virtual void ProcessRequest(HttpContext context)
        {
            var operationName = this.requestName ?? context.Request.GetOperationName();

            if (string.IsNullOrEmpty(operationName)) return;

            var httpReq = new AspNetRequest(context.Request, operationName);
            var httpResponse = new AspNetResponse(context.Response);

            ProcessRequest(httpReq, httpResponse, operationName);
        }

        public virtual void ProcessRequest(AspNetRequest httpReq, AspNetResponse httpRes, string operationName)
        {
            throw new NotImplementedException();
        }
    }
}
