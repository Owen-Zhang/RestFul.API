using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Web;
using RestFul.API.Configuration;
using RestFul.API.Http;
using ServiceStack.Text;

namespace RestFul.API.Extensions
{
    public class AspNetRequest
    {
        private readonly HttpRequest request;
        public string OperationName { get; set; }

        public AspNetRequest(HttpRequest request, string operationName = null)
        {
            this.OperationName = operationName;
            this.request = request;
        }

        public HttpRequest HttpRequest
        {
            get { return request; }
        }

        public object OriginalRequest
        {
            get { return request; }
        }
        
        /*
        public IResponse Response
        {
            get { return response; }
        }

        public IHttpResponse HttpResponse
        {
            get { return response; }
        }*/

        //public object Dto { get; set; }

        public string contentType
        {
            get { return request.ContentType; }
        }

        private string httpMethod;
        public string HttpMethod
        {
            get
            {
                return httpMethod
                    ?? (httpMethod = request.Headers[HttpHeaders.XHttpMethodOverride]
                    ?? request.HttpMethod);
            }
        }

        public string Verb
        {
            get { return HttpMethod; }
        }

        public string Param(string name)
        {
            return Headers[name]
                ?? QueryString[name]
                ?? FormData[name];
        }

        public bool IsLocal
        {
            get { return request.IsLocal; }
        }

        public string UserAgent
        {
            get { return request.UserAgent; }
        }

        private Dictionary<string, object> items;
        public Dictionary<string, object> Items
        {
            get
            {
                if (items == null)
                {
                    items = new Dictionary<string, object>();
                }
                return items;
            }
        }

        private string responseContentType;
        public string ResponseContentType
        {
            get
            {
                if (responseContentType == null)
                {
                    responseContentType = this.GetResponseContentType();
                }
                return responseContentType;
            }
            set
            {
                this.responseContentType = value;
            }
        }

        public bool HasExplicitResponseContentType { get; private set; }

        private Dictionary<string, Cookie> cookies;
        public IDictionary<string, Cookie> Cookies
        {
            get
            {
                if (cookies == null)
                {
                    cookies = new Dictionary<string, Cookie>();
                    for (var i = 0; i < this.request.Cookies.Count; i++)
                    {
                        var httpCookie = this.request.Cookies[i];
                        if (httpCookie == null)
                            continue;

                        Cookie cookie = null;

                        // try-catch needed as malformed cookie names (e.g. '$Version') can be returned
                        // from Cookie.Name, but the Cookie constructor will throw for these names.
                        try
                        {
                            cookie = new Cookie(httpCookie.Name, httpCookie.Value, httpCookie.Path, httpCookie.Domain)
                            {
                                HttpOnly = httpCookie.HttpOnly,
                                Secure = httpCookie.Secure,
                                Expires = httpCookie.Expires,
                            };
                        }
                        catch (Exception ex)
                        {
                            //log.Warn("Error trying to create System.Net.Cookie: " + httpCookie.Name, ex);
                        }

                        if (cookie != null)
                            cookies[httpCookie.Name] = cookie;
                    }
                }
                return cookies;
            }
        }

        public NameValueCollection Headers
        {
            get { return request.Headers; }
        }

        public NameValueCollection QueryString
        {
            get { return request.QueryString; }
        }

        public NameValueCollection FormData
        {
            get { return request.Form; }
        }

        public string GetRawBody()
        {
            using (var reader = new StreamReader(InputStream))
            {
                return reader.ReadToEnd();
            }
        }

        public string RawUrl
        {
            get { return request.RawUrl; }
        }

        public string AbsoluteUri
        {
            get
            {
                try
                {
                    return request.Url.AbsoluteUri.TrimEnd('/');
                }
                catch (Exception)
                {
                    return "http://" + request.UserHostName + request.RawUrl;
                }
            }
        }

        public string UserHostAddress
        {
            get
            {
                try
                {
                    return request.UserHostAddress;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public string XForwardedFor
        {
            get
            {
                return string.IsNullOrEmpty(request.Headers[HttpHeaders.XForwardedFor]) ? null : request.Headers[HttpHeaders.XForwardedFor];
            }
        }


        public bool IsSecureConnection
        {
            get { return request.IsSecureConnection; }
        }

        public string[] AcceptTypes
        {
            get { return request.AcceptTypes; }
        }

        public string PathInfo
        {
            get { return request.GetPathInfo(); }
        }

        public string UrlHostName
        {
            get { return request.GetUrlHostName(); }
        }

        public Stream InputStream
        {
            get { return request.InputStream; }
        }

        public long ContentLength
        {
            get { return request.ContentLength; }
        }

        /*
        private IHttpFile[] httpFiles;
        public IHttpFile[] Files
        {
            get
            {
                if (httpFiles == null)
                {
                    httpFiles = new IHttpFile[request.Files.Count];
                    for (var i = 0; i < request.Files.Count; i++)
                    {
                        var reqFile = request.Files[i];

                        httpFiles[i] = new HttpFile
                        {
                            ContentType = reqFile.ContentType,
                            ContentLength = reqFile.ContentLength,
                            FileName = reqFile.FileName,
                            InputStream = reqFile.InputStream,
                        };
                    }
                }
                return httpFiles;
            }
        }*/

        public Uri UrlReferrer
        {
            get { return request.UrlReferrer; }
        }

        public string GetResponseContentType()
        {
            var specifiedContentType = GetQueryStringContentType();
            if (!string.IsNullOrEmpty(specifiedContentType)) return specifiedContentType;

            var acceptContentTypes = request.AcceptTypes;
            var defaultContentType = request.ContentType;
            if (HasAnyOfContentTypes(ContentType.FormUrlEncoded, ContentType.MultiPartFormData))
            {
                defaultContentType = string.Empty;
            }

            var hasDefaultContentType = !string.IsNullOrEmpty(defaultContentType);
            if (acceptContentTypes != null)
            {
                var hasPreferredContentTypes = new bool[RestConfig.Instance.PreferredContentTypes.Count];
                foreach (var contentType in acceptContentTypes)
                {
                    //acceptsAnything = acceptsAnything || contentType == "*/*";

                    for (var i = 0; i < RestConfig.Instance.PreferredContentTypes.Count; i++)
                    {
                        if (hasPreferredContentTypes[i]) continue;
                        var preferredContentType = RestConfig.Instance.PreferredContentTypes[i];
                        hasPreferredContentTypes[i] = contentType.StartsWith(preferredContentType, StringComparison.InvariantCultureIgnoreCase);

                        //Prefer Request.ContentType if it is also a preferredContentType
                        if (hasPreferredContentTypes[i] && preferredContentType == defaultContentType)
                            return preferredContentType;
                    }
                }
                for (var i = 0; i < RestConfig.Instance.PreferredContentTypes.Count; i++)
                {
                    if (hasPreferredContentTypes[i]) return RestConfig.Instance.PreferredContentTypes[i];
                }
                //if (acceptsAnything && hasDefaultContentType) return defaultContentType;
            }

            return RestConfig.Instance.DefaultContentType;
        }

        public string GetQueryStringContentType()
        {
            var callback = request.QueryString["callback"];
            if (!string.IsNullOrEmpty(callback)) return ContentType.Json;

            var format = request.QueryString["format"];
            if (format == null)
            {
                const int formatMaxLength = 4;
                var pi = request.PathInfo;
                if (pi == null || pi.Length <= formatMaxLength) return null;
                if (pi[0] == '/') pi = pi.Substring(1);
                format = pi.SplitOnFirst('/')[0];
                if (format.Length > formatMaxLength) return null;
            }

            format = format.SplitOnFirst('.')[0].ToLower();
            if (format.Contains("json")) return ContentType.Json;
            if (format.Contains("xml")) return ContentType.Xml;
            //if (format.Contains("jsv")) return ContentType.Jsv;
            if (format.Contains("html")) return ContentType.Html;

            string contentType = string.Empty;

            return contentType;
        }

        public bool HasAnyOfContentTypes( params string[] contentTypes)
        {
            if (contentTypes == null || request.ContentType == null) return false;
            foreach (var contentType in contentTypes)
            {
                if (IsContentType(contentType)) return true;
            }
            return false;
        }

        public bool IsContentType(string contentType)
        {
            return request.ContentType.StartsWith(contentType, StringComparison.InvariantCultureIgnoreCase);
        }

        public Dictionary<string, string> GetRequestParams()
        {
            var paramsDic = new Dictionary<string, string>();

            //从URL中找参数
            foreach (var name in request.QueryString.AllKeys)
            {
                if (name == null) continue;
                paramsDic[name] = request.QueryString[name];
            }

            //从post和put中取值
            if ((httpMethod == HttpMethods.Post || httpMethod == HttpMethods.Put) &&
                FormData != null
                )
            {
                foreach (var name in FormData.AllKeys)
                {
                    if (null == name) continue;
                    paramsDic[name] = FormData[name];
                }
            }
            return paramsDic;
        }

        public bool IsChunked
        {
            get
            {
                var encoding = request.Headers[HttpHeaders.TransferEncoding];
                if (!string.IsNullOrEmpty(encoding) && encoding.ToLower().Trim() == "chunked")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
