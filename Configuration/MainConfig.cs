using System;
using System.Windows.Forms;

namespace WebScreenSaver.Configuration
{
    public class MainConfig : IConfig
    {
        public MainConfig()
        {
            Tab = new MainConfigTab();
        }

        public IDataSource DataSource
        {
            get
            {
                throw new NotImplementedException("Main config is not associated with a data source.");
            }
        }
        public TabPage Tab { get; private set; }
    }
}