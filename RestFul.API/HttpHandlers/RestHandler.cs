using System;
using RestFul.API.Common;
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
                /*处理访问前的用户设置*/
                
                /*调用方法，处理返回*/

                /*处理理用户的用户设置*/

                /*将信息发回给调用者*/
            }
            catch(Exception e){
                /*处理错误，改写httpStatus, 将错误发给客户端*/
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
            return restPath.CreateRequest(httpReq.PathInfo, requestParms, requestDTO);
        }

        private object CreateContentTypeRequest(AspNetRequest httpReq, Type requestType, string contentType)
        {
            try
            {
                if(!string.IsNullOrEmpty(contentType) && (httpReq.ContentLength > 0 || httpReq.IsChunked))
                    return SerializeManager.DeserializeFromStream(contentType, requestType, httpReq.InputStream);
            }
            catch (Exception ex)
            {
                throw new Exception("");
            }
            return null;
        }
    }
}
