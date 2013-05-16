using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WebScreenSaver.Configuration
{
    public partial class MainConfigTab : Component
    {
        public MainConfigTab()
        {
            InitializeComponent();
        }

        public MainConfigTab(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
