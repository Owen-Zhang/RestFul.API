using System;
using RestFul.API.Extensions;
using RestFul.API.Host;

namespace RestFul.API.HttpHandlers
{
    public class RestHandler : HttpHandlerBase
    {
        public string RequestName { get; set; }

        public RestHandler() : base(null) { }

        public RestPath RestPath { get; set; }

        public RestHandler(RestPath restPath, string requestName): base(requestName)
        {
            this.RestPath = restPath;
            this.RequestName = requestName;
        }

        public static RestPath FindMatchingRestPath(string httpMethod, string pathInfo)
        {
            return null;
        }

        public override void ProcessRequest(AspNetRequest httpReq, AspNetResponse httpRes, string operationName)
        {
            var resposeType = httpReq.ResponseContentType;

            try {
                var request = GetRequest(httpReq, RestPath);
            }
            catch(Exception e){
                
            }
        }

        private object GetRequest(AspNetRequest httpReq, RestPath restPath)
        {
            var requestType = restPath.RequestType;
            var requestParms = httpReq.GetRequestParams();

            object requestDTO = null;
            var contentType = httpReq.contentType;
            if (string.IsNullOrEmpty(contentType))
                contentType = httpReq.ResponseContentType;

            requestDTO = CreateContentTypeRequest(httpReq, requestType, contentType);
            
        }
    }
}
