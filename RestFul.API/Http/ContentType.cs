namespace RestFul.API.Http
{
    public static class ContentType
    {
        public const string Utf8Suffix = "; charset=utf-8";

        public const string HeaderContentType = "Content-Type";

        public const string FormUrlEncoded = "application/x-www-form-urlencoded";

        public const string MultiPartFormData = "multipart/form-data";

        public const string Html = "text/html";

        public const string JsonReport = "text/jsonreport";

        public const string Xml = "application/xml";

        public const string XmlText = "text/xml";

        public const string Soap11 = " text/xml; charset=utf-8";

        public const string Soap12 = " application/soap+xml";

        public const string Json = "application/json";

        public const string JsonText = "text/json";

        public const string JavaScript = "application/javascript";

        public const string Jsv = "application/jsv";

        public const string JsvText = "text/jsv";

        public const string Csv = "text/csv";

        public const string Yaml = "application/yaml";

        public const string YamlText = "text/yaml";

        public const string PlainText = "text/plain";

        public const string MarkdownText = "text/markdown";

        public const string ProtoBuf = "application/x-protobuf";

        public const string Binary = "application/octet-stream";

        public static string GetContentType(string contentType)
        {
            if (contentType == null)
                return null;

            var realContentType = contentType.Split(';')[0].Trim();
            realContentType = realContentType.ToLower();
            switch (realContentType)
            {
                case Json:
                case JsonText:
                    return Json;

                case Xml:
                case XmlText:
                    return Xml;

                case Html:
                    return Html;
            }

            return null;
        }
    }
}
