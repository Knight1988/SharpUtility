using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;
using SharpUtility.Core.Security.Cryptography;

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
           var item = new SimpleFileCacheItem<object>
           {
                Content = obj,
                ExpireDate = expireDate
            };

            var path = GetCachePath(name);
            SerializeToFile(path, item);
        }

        protected string GetCachePath(string name)
        {
            var md5 = MD5.Create();
            name = md5.ComputeHash(name);
            return Path.Combine(CachePath, name);
        }

        public object Get(string name)
        {
            var path = GetCachePath(name);
            var item = DeserializeFromFile<object>(path);

            if (item == null || item.ExpireDate < DateTime.Now) return null;

            return item.Content;
        }

        public T Get<T>(string name)
        {
            var path = GetCachePath(name);
            var item = DeserializeFromFile<T>(path);

            if (item == null || item.ExpireDate < DateTime.Now) return default(T);

            return item.Content;
        }

        public void Remove(string name)
        {
            var path = GetCachePath(name);
            if (File.Exists(path)) File.Delete(path);
        }

        public void Clear()
        {
            if (Directory.Exists(CachePath))
            {
                Directory.Delete(CachePath, true);
            }
        }

        /// <summary>
        ///     Writes the given object instance to a text file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="item"></param>
        protected void SerializeToFile(string path, SimpleFileCacheItem<object> item)
        {
            var json = JsonConvert.SerializeObject(item);
            File.WriteAllText(path, json);
        }

        protected SimpleFileCacheItem<T> DeserializeFromFile<T>(string path)
        {
            if (!File.Exists(path)) return null;

            var json = File.ReadAllText(path);
            var item = JsonConvert.DeserializeObject<SimpleFileCacheItem<T>>(json);
            return item;
        }
    }

    public class SimpleFileCacheItem<T>
    {
        public T Content { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
