namespace RestFul.API.Http
{
    public static class HttpHeaders
    {
        public const string XParamOverridePrefix = "X-Param-Override-";

        public const string XHttpMethodOverride = "X-Http-Method-Override";

        public const string XAcceptLanguageOverride = "X-Accept-Language-Override";

        public const string XUserAuthId = "X-UAId";

        public const string XForwardedFor = "X-Forwarded-For";

        public const string XClientUser = "X-Client-UserId";

        public const string CacheControl = "Cache-Control";

        public const string IfModifiedSince = "If-Modified-Since";

        public const string IfMatch = "If-Match";

        public const string IfNoneMatch = "If-None-Match";

        public const string LastModified = "Last-Modified";

        public const string Accept = "Accept";

        public const string AcceptEncoding = "Accept-Encoding";

        public const string ContentType = "Content-Type";

        public const string ContentEncoding = "Content-Encoding";

        public const string ContentLength = "Content-Length";

        public const string ContentLanguage = "Content-Language";

        public const string ContentDisposition = "Content-Disposition";

        public const string Location = "Location";

        public const string Link = "Link";

        public const string SetCookie = "Set-Cookie";

        public const string ETag = "ETag";

        public const string Authorization = "Authorization";

        public const string WwwAuthenticate = "WWW-Authenticate";

        public const string Referer = "Referer";

        public const string LanguageCode = "Accept-Language";

        public const string TransferEncoding = "Transfer-Encoding";

        //For Auth
        public const string InternalSystem = "X-SysName";
        public const string InternalUser = "X-UserToken";
        public const string ExternalSystem = "X-UserSecure";

        public const string AppKey = "X-AppKey";

        public const string AllowOrigin = "Access-Control-Allow-Origin";

        public const string AllowMethods = "Access-Control-Allow-Methods";

        public const string AllowHeaders = "Access-Control-Allow-Headers";

        public const string Origin = "Origin";

        public const string RequestHeaders = "Access-Control-Request-Headers";

        public const string MessageInvokeType = "X-Nemq-InvokeType";

        public const string MessageCallback = "X-Nemq-CallbackUri";

    }
}
