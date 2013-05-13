using System;
using System.Windows.Forms;
using WebScreenSaver.Configuration;

namespace WebScreenSaver
{
    public class ScreenSaver
    {
        [STAThread]
        private static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                StartScreenSaver();
                return;
            }

            var config = (WebpageConfig) Config.Current;
            if (args[0].ToLower().Trim().Substring(0, 2) == "/c")
            {
                var configForm = new ConfigForm { Urls = config.UrlText, Path = Config.Path };
                if (configForm.ShowDialog() == DialogResult.OK)
                {
                    Config.Save(configForm.Urls);
                }
            }
            else if (args[0].ToLower() == "/s")
            {
                StartScreenSaver();
            }
        }

        private static UrlDataSource LoadUrlList()
        {
            return new UrlDataSource(Config.Urls);
        }

        private static void StartScreenSaver()
        {
            int screenCount = Screen.AllScreens.Length;
            var screensaverForms = new ScreensaverForm[screenCount];

            // Start the screen saver on all the displays the computer has
            for (int x = 0; x < screenCount; x++)
            {
                screensaverForms[x] = new ScreensaverForm(x, LoadUrlList());
                screensaverForms[x].Show();
            }

            while (true)
            {
                Application.DoEvents();
                // if any screen is not visible then return
                for (int x = 0; x < screenCount; x++)
                {
                    if (screensaverForms[x].Visible == false) return;
                }
            }
        }
    }
}