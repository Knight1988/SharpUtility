using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SharpUtility.Runtime.Caching
{
    public class SimpleFileCache
    {
        private string _name;
        public string CachePath { get; set; }

        public SimpleFileCache(string name)
        {
            var processName = Process.GetCurrentProcess().ProcessName;
            _name = name;
            CachePath = Path.Combine(Path.GetTempPath(), processName, name);
            Directory.CreateDirectory(CachePath);
        }

        public void Add(string name, object obj, DateTime expireDate)
        {
           var item = new SimpleFileCacheItem()
            {
                Content = obj,
                ExpireDate = expireDate
            };

            var path = Path.Combine(CachePath, name);
            SerializeToFile(path, item);
        }

        /// <summary>
        ///     Writes the given object instance to a text file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="item"></param>
        private void SerializeToFile(string path, SimpleFileCacheItem item)
        {
            var json = JsonConvert.SerializeObject(item);
            File.WriteAllText(path, json);
        }
    }

    public class SimpleFileCacheItem
    {
        public object Content { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
