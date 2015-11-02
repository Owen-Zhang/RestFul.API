using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using RestFul.API.Configuration;
using RestFul.API.Extensions;

namespace RestFul.API.Host
{
    public abstract class ServiceStackHost : IDisposable
    {
        private RestResourceSection m_section = (RestResourceSection)ConfigurationManager.GetSection("RestResourceSection");

        public List<Action<AspNetRequest, AspNetResponse, object>> GlobalRequestFilters
        {
            get
            {
                return ConfigManager.Instance.FilterManager.RequestFilters;
            }
        }

        public List<Action<AspNetRequest, AspNetResponse, object>> GlobalResponseFilters 
        {
            get
            {
                return ConfigManager.Instance.FilterManager.ResponseFilters;
            }
        }

        public List<Action<AspNetRequest>> OnBeginRequestCallbacks { get; set; }

        public List<Action<AspNetRequest>> OnEndRequestCallbacks { get; set; }

        /// <summary>
        /// (调用)实现此方法来启动框架
        /// </summary>
        public virtual void Init()
        {
            ApplyConfig();
            new ServiceManager(m_section.Resources.GetAllTypes()).Init();
        }

        private void ApplyConfig()
        {
            ConfigManager.Instance.APIName = m_section.APIName;
            ConfigManager.Instance.FilterManager = new FilterManager();
            SetDebug(m_section.DebugMode);
        }

        private void SetDebug(string debugMode)
        {
            bool debug = false;
            if (!string.IsNullOrEmpty(debugMode) && bool.TryParse(debugMode, out debug))
                ConfigManager.Instance.DebugMode = debug;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
