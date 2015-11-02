using System;
using System.Collections.Generic;
using RestFul.API.Extensions;

namespace RestFul.API.Host
{
    public class FilterManager
    {
        public List<Action<AspNetRequest, AspNetResponse, object>> RequestFilters { get; set; }
        public List<Action<AspNetRequest, AspNetResponse, object>> ResponseFilters { get; set; }

        public FilterManager()
        {
            RequestFilters = new List<Action<AspNetRequest, AspNetResponse, object>>();
            ResponseFilters = new List<Action<AspNetRequest, AspNetResponse, object>>();
        }

        public bool ApplyRequestFilters(AspNetRequest req, AspNetResponse res, object requestDto)
        {
            /*
            req.ThrowIfNull("Request");
            req.ThrowIfNull("Response");
            */

            if (res.IsClosed)
                return res.IsClosed;

            foreach (var filter in RequestFilters)
            {
                filter(req, res, requestDto);
                if (res.IsClosed)
                    return res.IsClosed;
            }
            return res.IsClosed;
        }

        public bool ApplyResponseFilters(AspNetRequest req, AspNetResponse res, object responseDTO)
        {
            if (res.IsClosed)
                return res.IsClosed;

            foreach (var filter in ResponseFilters)
            {
                filter(req, res, responseDTO);
                if (res.IsClosed)
                    return res.IsClosed;
            }

            return res.IsClosed;
        }
    }
}
