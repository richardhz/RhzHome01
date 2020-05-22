using RhzHome01.Client.Services.Interfaces;
using RhzHome01.Shared;
using System;
using System.Collections.Generic;

namespace RhzHome01.Client.Services
{
    public class CacheService : ICacheService
    {
        public Dictionary<string, CacheItem> List { get; }

        public CacheService()
        {
            List = new Dictionary<string, CacheItem>();
        }

        public void Add<T>(string key, T item, int maxAge = 120) where T : class
        {
            if (item != null)
            {
                var i = new CacheItem
                {
                    CreatedOn = DateTimeOffset.UtcNow,
                    Item = item,
                    MaxAge = maxAge
                };
                List.Add(key, i);
            }

        }


        public T Get<T>(string key) where T : class
        {
            if (List.TryGetValue(key, out CacheItem cacheItem) && cacheItem.Age <= cacheItem.MaxAge)
            {
                return cacheItem.Item as T;
            }
            if (cacheItem != null)
            {
                List.Remove(key);
            }
            return null;
        }

        public bool Peek(string key)
        {
            if (List.TryGetValue(key, out CacheItem cacheItem) && cacheItem.Age <= cacheItem.MaxAge)
            {
                return true;
            }
            if (cacheItem != null)
            {
                List.Remove(key);
            }
            return false;
        }
    }
}
