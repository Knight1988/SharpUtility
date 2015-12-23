﻿using System;
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

        public object Get(string name)
        {
            var path = Path.Combine(CachePath, name);
            if (!File.Exists(path)) return null;

            var json = File.ReadAllText(path);
            var item = JsonConvert.DeserializeObject<SimpleFileCacheItem>(json);

            if (item.ExpireDate < DateTime.Now) return null;

            return item.Content;
        }

        public T Get<T>(string name)
        {
            return (T) Get(name);
        }

        public void Remove(string name)
        {
            var path = Path.Combine(CachePath, name);
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
        protected void SerializeToFile(string path, SimpleFileCacheItem item)
        {
            var json = JsonConvert.SerializeObject(item);
            File.WriteAllText(path, json);
        }

        protected void DeserializeFromFile(string path)
        {

            var json = File.ReadAllText(path);
        }
    }

    public class SimpleFileCacheItem
    {
        public object Content { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
