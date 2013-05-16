using System.Windows.Forms;

namespace WebScreenSaver.Configuration
{
    interface IConfig
    {
        IDataSource DataSource { get; }
        TabPage Tab { get; }
    }
}
