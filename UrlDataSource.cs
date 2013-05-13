using System.Collections.Generic;

namespace WebScreenSaver
{
    internal class UrlDataSource : IDataSource
    {
        private const string DefaultUrl = "http://whatthecommit.com/";
        private List<string> _urlList = new List<string>();
        private int _listIndex;

        public UrlDataSource()
        {
            _urlList.Add(DefaultUrl);
        }

        public UrlDataSource(IEnumerable<string> urls)
        {
            Assign(urls);
        }

        public string GetNext()
        {
            if (_listIndex >= _urlList.Count)
                _listIndex = 0;

            return _urlList[_listIndex++];
        }

        public void Add(string url)
        {
            _urlList.Add(url);
        }

        public void Assign(IEnumerable<string> urls)
        {
            _urlList = new List<string>(urls);
            if (_urlList.Count == 0)
                _urlList.Add(DefaultUrl);
        }
    }
}