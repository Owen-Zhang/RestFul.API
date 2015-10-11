using System;
using System.Collections.Generic;
using System.IO;
using RestFul.API.Http;
using ServiceStack.Text;

namespace RestFul.API.Common
{
    public delegate object StreamDeserializerDelegate(Type type, Stream inputStream);

    public class SerializeManager
    {
        private static Dictionary<string, StreamDeserializerDelegate> ContentTypeDeserializers
            = new Dictionary<string, StreamDeserializerDelegate>();

        public static object DeserializeFromStream(string contentType, Type type, Stream fromStream)
        {
            var deserializer = GetStreamDeserializer(contentType);
            if (deserializer == null)
                throw new NotSupportedException("ContentType not supported: " + contentType);

            return deserializer(type, fromStream);
        }

        private static StreamDeserializerDelegate GetStreamDeserializer(string contentType)
        {
            StreamDeserializerDelegate streamReader;
            var realContentType = ContentType.GetContentType(contentType);
            if (ContentTypeDeserializers.TryGetValue(realContentType, out streamReader))
                return streamReader;

            StreamDeserializerDelegate serializer = null;
            if (realContentType == ContentType.Xml)
                serializer = XmlSerializer.DeserializeFromStream;
            else if (realContentType == ContentType.Json)
                serializer = JsonSerializer.DeserializeFromStream;

            ContentTypeDeserializers.Add(contentType, serializer);
            return serializer;
        }
    }
}
