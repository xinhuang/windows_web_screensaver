using System;
using System.Drawing;
using System.Windows.Forms;

namespace WebScreenSaver
{
    public partial class RandomTextView : UserControl
    {
        private readonly RandomTextConfig _config;
        private readonly Timer _timer = new Timer();

        public RandomTextView(RandomTextConfig config)
        {
            _config = config;
            _timer.Interval = 20000;
            _timer.Tick += OnTimerTick;
            _timer.Start();
            Text = _config.GetNext();

            InitializeComponent();
        }

        void OnTimerTick(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => OnTimerTick(sender, e)));
                return;
            }
            Next();
        }

        private void Next()
        {
            Text = _config.GetNext();
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            Next();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;

            var textBound = g.MeasureString(Text, Font);
            var x = 10;
            var y = (int)((Height - textBound.Height)/3.66667);
            g.DrawString(Text, Font, Brushes.Black, x, y);
        }
    }
}
