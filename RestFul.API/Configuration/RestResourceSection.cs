using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace RestFul.API.Configuration
{
    public class RestResourceSection : ConfigurationSection
    {
        [ConfigurationProperty("Resources", IsRequired = true)]
        public Resources Resources
        {
            get
            {
                return this["Resources"] as Resources;
            }
        }

        [ConfigurationProperty("APIName", IsRequired = true)]
        public string APIName
        {
            get
            {
                return this["APIName"] as string;
            }
        }

        [ConfigurationProperty("DebugMode", IsRequired = true)]
        public string DebugMode
        {
            get
            {
                return this["DebugMode"] as string;
            }
        }
    }

    public class Resources : ConfigurationElementCollection
    {
        public ResourceElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as ResourceElement;
            }
        }

        public ResourceElement this[string key]
        {
            get
            {
                return base.BaseGet(key) as ResourceElement;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ResourceElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as ResourceElement).AssemblyName;
        }

        public Assembly[] GetAllTypes()
        {
            List<Assembly> assemblies = new List<Assembly>();
            foreach (var key in this.BaseGetAllKeys())
            {
                assemblies.Add(Assembly.Load(key.ToString()));
            }
            return assemblies.ToArray();
        }
    }

    public class ResourceElement : ConfigurationElement
    {
        [ConfigurationProperty("AssemblyName", IsRequired = true)]
        public string AssemblyName
        {
            get
            {
                return this["AssemblyName"] as string;
            }
        }
    }
}
