using System;
using System.Collections.Generic;
using System.IO;

namespace WebScreenSaver
{
    public class RandomTextConfig : IDisposable
    {
        readonly Random _rand = new Random();
        private const string Path = @"C:\";
        private const string Filter = "*.h";
        private const int MaxLine = 7;
        private readonly List<string> _fileList = new List<string>();

        private readonly ActiveObject _active = new ActiveObject();

        public RandomTextConfig()
        {
            _active.Send(() => Traverse(Path, Filter, _fileList));
        }

        private void Traverse(string path, string filter, List<string> fileList)
        {
            try
            {
                string[] files = Directory.GetFiles(path, filter);
                if (files.Length > 0)
                {
                    lock (_fileList)
                    {
                        _fileList.AddRange(files);
                    }
                }
                foreach (var dir in Directory.GetDirectories(path))
                {
                    if (dir == "." || dir == "..")
                        continue;
                    string temp = dir;
                    _active.Send(() => Traverse(temp, filter, fileList));
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
        }

        public string GetNext()
        {
            string file;
            lock (_fileList)
            {
                if (_fileList.Count == 0)
                    return Default;
                file = _fileList[_rand.Next(_fileList.Count)];
            }

            var lines = File.ReadAllLines(file);
            var startLineNumber = _rand.Next(lines.Length - MaxLine);
            string result = string.Empty;
            for (int i = 0; i < MaxLine; i++)
            {
                result += lines[startLineNumber + i] + Environment.NewLine;
            }
            return result;
        }

        private static string Default
        {
            get
            {
                return
                    @"qs [] = []" + Environment.NewLine +
                    @"qs (s:xs) = qs [x|x <- xs,x < s] ++ [s] ++ qs [x|x <- xs,x >= s] ";
            }
        }

        public void Dispose()
        {
            _active.Dispose();
        }
    }
}
