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
            var config = (WebpageConfig)ConfigManager.CurrentMode;

            if (args.Length <= 0)
            {
                StartScreenSaver(config);
                return;
            }

            if (args[0].ToLower().Trim().Substring(0, 2) == "/c")
            {
                var configForm = new ConfigForm();
                if (configForm.ShowDialog() == DialogResult.OK)
                {
                }
            }
            else if (args[0].ToLower() == "/s")
            {
                StartScreenSaver(config);
            }
        }

        private static UrlDataSource LoadUrlList(WebpageConfig config)
        {
            return new UrlDataSource(config.Urls);
        }

        private static void StartScreenSaver(WebpageConfig config)
        {
            var urlDataSource = LoadUrlList(config);
            int screenCount = Screen.AllScreens.Length;
            var screensaverForms = new ScreensaverForm[screenCount];

            // Start the screen saver on all the displays the computer has
            for (int x = 0; x < screenCount; x++)
            {
                screensaverForms[x] = new ScreensaverForm(x, urlDataSource);
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