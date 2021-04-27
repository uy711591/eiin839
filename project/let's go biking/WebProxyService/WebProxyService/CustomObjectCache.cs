using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace WebProxyService
{
    class CustomObjectCache<T> where T: class
    {
        private ObjectCache cache = MemoryCache.Default;
        private static double FIVE_MINUTES_IN_SECONDS = 500;

        public T get(string key)
        {
            if (cache.Get(key) == null)
            {
                System.Diagnostics.Debug.WriteLine("no cache found or old cache has expired");
                var cacheItemPolicy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(FIVE_MINUTES_IN_SECONDS),
                };
                T value = (T)Activator.CreateInstance(typeof(T), key);
                cache.Add(key, value, cacheItemPolicy);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("catch found");
            }
            return (T)cache.Get(key);
        }
    }
}
