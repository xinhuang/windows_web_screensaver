using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WebScreenSaver
{
    public partial class ScreensaverForm
    {
        // Store the number of displays
        private readonly int _thisDisplayIdId;
        private readonly UrlDataSource _urlDataSource;
        private WebBrowser _webBrowser;
        private IContainer components;
        // Store the mouse coordinates
        private Point _mouseCoords;
        private Timer _newPageTimer;
        private bool _closeWhenMouseMove;

        internal ScreensaverForm(int thisDisplayId, UrlDataSource urlDataSource)
        {
            InitializeComponent();
            // Assign the number to an accessible variable
            _thisDisplayIdId = thisDisplayId;
            _urlDataSource = urlDataSource;
        }

        public bool CloseWhenMouseMove
        {
            get { return _closeWhenMouseMove; }
            set { _closeWhenMouseMove = value; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            // Set the bounds of the form, fill all the screen
            Bounds = Screen.AllScreens[_thisDisplayIdId].Bounds;
            _webBrowser.Left = 0;
            _webBrowser.Top = 0;
            _webBrowser.Width = Width;
            _webBrowser.Height = Height;
            _webBrowser.ScriptErrorsSuppressed = true;
            _webBrowser.PreviewKeyDown += OnWebBrowserPreviewKeyDown;
            _webBrowser.DocumentCompleted += OnWebBrowserDocumentCompleted;

            DisplayNextPage();
            // The form should be on top of all
            TopMost = true;
            // We don't need the cursor
            Cursor.Hide();
        }

        private void OnWebBrowserDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlDocument doc = _webBrowser.Document;
            doc.MouseMove += OnDocMouseMove;
        }

        private void OnDocMouseMove(object sender, HtmlElementEventArgs e)
        {
            if (!CloseWhenMouseMove)
                return;
            // If mouseCoords is empty don't close the screen saver
            if (!_mouseCoords.IsEmpty)
            {
                // If the mouse actually moved more than 10 pixes in any direction
                if (Math.Abs(_mouseCoords.X - e.MousePosition.X) > 10
                    || Math.Abs(_mouseCoords.Y - e.MousePosition.Y) > 10)
                {
                    // Close
                    Close();
                }
            }
            // Set the new point where the mouse is
            _mouseCoords = new Point(e.MousePosition.X, e.MousePosition.Y);
        }
        
        private void OnWebBrowserPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // ...close the screen saver
            Close();
        }

        // Triggered by timer component every 20 seconds...
        private void NewPageTimerTick(object sender, EventArgs e)
        {
            DisplayNextPage();
        }

        private void DisplayNextPage()
        {
            var uri = new Uri(_urlDataSource.GetNext());
            _webBrowser.Navigate(uri);
            _webBrowser.Refresh();
        }
    }
}