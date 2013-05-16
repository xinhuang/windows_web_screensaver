using System.Collections.Generic;

namespace WebScreenSaver.Configuration
{
    static internal class ConfigManager
    {
        private static readonly ICollection<IConfig> _configs = new List<IConfig>();

        static ConfigManager()
        {
            _configs.Add(new MainConfig());
            _configs.Add(new WebpageConfig());
        }

        static public IConfig CurrentMode
        {
            get { return new WebpageConfig(); }
        }

        public static IEnumerable<IConfig> Configs
        {
            get { return _configs; }
        }
    }
}
