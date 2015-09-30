using System;
using System.Collections.Generic;
using System.Reflection;
using RestFul.API.Extensions;

namespace RestFul.API.Host
{
    public class ServiceStackHost : IDisposable
    {
        public string ServiceName { get; set; }

        public List<Action<AspNetRequest, AspNetResponse, object>> GlobalRequestFilters { get; set; }

        public List<Action<AspNetRequest, AspNetResponse, object>> GlobalResponseFilters { get; set; }

        public List<Action<AspNetRequest>> OnBeginRequestCallbacks { get; set; }

        public List<Action<AspNetRequest>> OnEndRequestCallbacks { get; set; }

        protected ServiceStackHost(string serviceName, params Assembly[] assembliesWithServices)
        {
            ServiceName = serviceName;
        }

        /*
        public virtual object OnPreExecuteServiceFilter(IService service, object request, AspNetRequest httpReq, AspNetResponse httpRes)
        {
            return request;
        }*/

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
