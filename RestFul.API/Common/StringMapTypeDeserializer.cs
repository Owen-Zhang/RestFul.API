using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Text;
using ServiceStack.Text.Common;
using ServiceStack.Text.Jsv;
using System.Runtime.Serialization;

namespace RestFul.API.Common
{
    public class StringMapTypeDeserializer
    {
        internal class PropertySerializerEntry
        {
            public PropertySerializerEntry(SetPropertyDelegate propertySetFn, ParseStringDelegate propertyParseStringFn)
            {
                PropertySetFn = propertySetFn;
                PropertyParseStringFn = propertyParseStringFn;
            }

            public SetPropertyDelegate PropertySetFn;
            public ParseStringDelegate PropertyParseStringFn;
        }

        private readonly Type type;
        private readonly Dictionary<string, PropertySerializerEntry> propertySetterMap
            = new Dictionary<string, PropertySerializerEntry>(StringComparer.InvariantCultureIgnoreCase);

        public ParseStringDelegate GetParseFn(Type propertyType)
        {
            //Don't JSV-decode string values for string properties
            if (propertyType == typeof(string))
                return s => s;

            return JsvReader.GetParseFn(propertyType);
        }

        public StringMapTypeDeserializer(Type type)
        {
            this.type = type;

            if (type.IsOrHasGenericInterfaceTypeOf(typeof(IEnumerable<>)))
                return;

            foreach (var propertyInfo in type.GetProperties())
            {
                var propertySetFn = JsvDeserializeType.GetSetPropertyMethod(type, propertyInfo);
                var propertyType = propertyInfo.PropertyType;
                var propertyParseStringFn = GetParseFn(propertyType);
                var propertySerializer = new PropertySerializerEntry(propertySetFn, propertyParseStringFn);

                var attr = propertyInfo.FirstAttribute<DataMemberAttribute>();
                if (attr != null && attr.Name != null)
                {
                    propertySetterMap[attr.Name] = propertySerializer;
                }
                propertySetterMap[propertyInfo.Name] = propertySerializer;
            }
        }

        public object PopulateFromMap(object instance, IDictionary<string, string> keyValuePairs)
        {
            try
            {
                if (instance == null) instance = ReflectionUtils.CreateInstance(type);

                foreach (var pair in keyValuePairs)
                {
                    var propertyName = pair.Key;
                    var propertyTextValue = pair.Value;

                    PropertySerializerEntry propertySerializerEntry;
                    if (!propertySetterMap.TryGetValue(propertyName, out propertySerializerEntry))
                    {
                        continue;
                    }

                    if (!string.IsNullOrWhiteSpace(pair.Value))
                    {
                        var value = propertySerializerEntry.PropertyParseStringFn(propertyTextValue);
                        if (value == null)
                        {
                            continue;
                        }
                        propertySerializerEntry.PropertySetFn(instance, value);
                    }
                }
                return instance;

            }
            catch (Exception ex)
            {
                throw new SerializationException("KeyValueDataContractDeserializer: Error converting to type: " + ex.Message, ex);
            }
        }

        public object CreateFromMap(IDictionary<string, string> keyValuePairs)
        {
            return PopulateFromMap(null, keyValuePairs);
        }
    }
}
