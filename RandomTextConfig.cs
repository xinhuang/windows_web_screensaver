using System;

namespace WebScreenSaver
{
    public class RandomTextConfig
    {
        readonly Random _rand = new Random();

        public string GetNext()
        {
            return _rand.Next().ToString();
        }
    }
}