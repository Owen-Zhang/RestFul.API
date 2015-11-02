using System.Reflection;
using RestFul.API.Extensions;
namespace RestFul.API.Host
{
    public class ServiceManager
    {
        private Assembly[] m_assembliesWithServices;

        public ServiceManager(Assembly[] assembliesWithServices)
        {
            if (assembliesWithServices != null && assembliesWithServices.Length > 0)
            {
                m_assembliesWithServices = assembliesWithServices;
            }

            /*
            this.Container = new Container();
            ServiceTypes = new HashSet<Type>();
            RequestResponseMap = new Dictionary<Type, List<Type>>();
            RequestServiceMap = new Dictionary<Type, Type>();
             */
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
    }
}
