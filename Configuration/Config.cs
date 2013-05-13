using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WebScreenSaver.Configuration
{
    static internal class Config
    {
        static public IConfig Current
        {
            get { return new WebpageConfig(); }
        }
    }
}