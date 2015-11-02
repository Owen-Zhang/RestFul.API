namespace RestFul.API.Host
{
    public class ConfigManager
    {
        private static ConfigManager instance;

        /// <summary>
        /// 处理访问前后的自定义方法
        /// </summary>
        public FilterManager FilterManager { get; set; }
        public bool DebugMode { get; set; }
        public string APIName { get; set; }

        public static ConfigManager Instance {
            get {
                return instance ?? (instance = NewInstance());
            }
        }

        public static ConfigManager NewInstance()
        {
            var instanceTemp = new ConfigManager()
            {
                APIName = "RestFul.API"
            };
            return instanceTemp;
        }

        public static ConfigManager ResetInstace()
        {
            return NewInstance();
        }

        public ConfigManager() {}
    }
}
