using System.Windows.Forms;
using WebScreenSaver.Configuration;

namespace WebScreenSaver
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
            foreach (var config in ConfigManager.Configs)
                tabControl.TabPages.Add(config.Tab);
        }
    }
}
