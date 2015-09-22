using System;
using System.Collections.Generic;
using System.Net;
using RestFul.API.Http;

namespace RestFul.API.Configuration
{
    public class RestConfig
    {
        public HashSet<string> AllowFileExtensions { get; set; }
        public bool DebugMode { get; set; }
        //public bool EnableMonitor { get; set; }
        public bool EnableAuth { get; set; }
        public string DefaultContentType { get; set; }
        public List<string> SupportLanguageCode { get; set; }
        public string DefaultContentLanguage { get; set; }
        public string APIName { get; set; }
        public TimeSpan DefaultExpireTime { get; set; }
        public List<string> PreferredContentTypes { get; set; }
        public int DefaultPageSize { get; set; }
        public int DefaultPageIndex { get; set; }
        public int MaxPageSize { get; set; }
        internal List<Type> IgnoreLoggingExceptions { get; set; }
        internal List<HttpStatusCode> IgnoreLoggingStatusCodes { get; set; }

        private static RestConfig instance;
        public static RestConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RestConfig
                    {
                        DebugMode = false,
                        EnableAuth = false,
                        DefaultContentType = ContentType.Html,
                        DefaultPageIndex = 0,
                        DefaultPageSize = 10,
                        MaxPageSize = 10000,
                        AllowFileExtensions = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase)
						{
							"js", "css", "htm", "html", "shtm", "txt", "xml", "rss", "csv", 
							"jpg", "jpeg", "gif", "png", "bmp", "ico", "tif", "tiff", 
							"avi", "divx", "m3u", "mov", "mp3", "mpeg", "mpg", "qt", "vob", "wav", "wma", "wmv", 
							"flv", "xap", "xaml", 
						},
                        DefaultExpireTime = TimeSpan.FromHours(1),
                        PreferredContentTypes = new List<string> { ContentType.Html, ContentType.Json, ContentType.JsonText, ContentType.Xml, ContentType.XmlText, ContentType.Jsv },
                        IgnoreLoggingExceptions = new List<Type>() { typeof(NotImplementedException) },
                        IgnoreLoggingStatusCodes = new List<HttpStatusCode>()
                    };
                }
                return instance;
            }
        }

        public RestConfig()
        {

        }
    }
}
