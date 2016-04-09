using System;
using System.Collections.Generic;
using System.Reflection;
using RestFul.API.Extensions;
using RestFul.API.Interface;
namespace RestFul.API.Host
{
    public class ServiceManager
    {
        private Assembly[] m_assembliesWithServices;

        public Dictionary<Type, Type> RequestServiceMap;

        public ServiceManager(Assembly[] assembliesWithServices)
        {
            if (assembliesWithServices != null && assembliesWithServices.Length > 0)
            {
                m_assembliesWithServices = assembliesWithServices;
            }

            //暂时还没用到
            RequestServiceMap = new Dictionary<Type, Type>();
        }

        public void Init()
        {
            Register();
        }

        /// <summary>
        /// 加载Dll中的路由和相关service
        /// </summary>
        public void Register()
        { 
            m_assembliesWithServices.ThrowIfNull("Assembly"); 
            //RegisterServiceCore()
        }

        /// <summary>
        /// 对类型进行注册
        /// </summary>
        private void RegisterService(Type serviceType)
        {
            //当有多次继承时，中间层要除去如：base<T> : IService<T>
            if (serviceType.IsAbstract || serviceType.ContainsGenericParameters) return;

            foreach (var service in serviceType.GetInterfaces())
            {
                if (!service.IsGenericType || service.GetGenericTypeDefinition() != typeof(IService<>))
                    continue;

                //得到Request实体 Restbase<T> T 为此处的值
                var requestType = service.GetGenericArguments()[0];

                //注册Request实体和Service之间的关联关系
                RegisterServiceAndRequestMap(requestType, service);
            }
        }

        private void RegisterServiceAndRequestMap(Type requestType, Type serviceType)
        {
            if (!RequestServiceMap.ContainsKey(requestType))
                RequestServiceMap.Add(requestType, serviceType);

            var typeFactoryFunction = CallServiceExecute(requestType, serviceType);

        }

        private CallServiceExecute(Type requestType, Type serviceType)
        {
            
        }

        /// <summary>
        /// 加载包含Service和URL的Dll, 可以有多个service Dll
        /// </summary>
        private List<Type> GetAssemblyType(Assembly[] assembliesWithServices)
        {
            var result = new List<Type>();
            foreach (var assmbly in assembliesWithServices)
            {
                if (assmbly == null) continue;
                foreach (var type in assmbly.GetTypes())
                {
                    if (type == null) continue;
                    result.Add(type);
                }
            }
            return result;
        }
    }
}
