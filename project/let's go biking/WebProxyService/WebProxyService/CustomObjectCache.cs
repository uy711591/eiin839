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
        private static double ONE_DAY_IN_SECONDS = 86400;

        public T get(string key)
        {
            if (cache.Get(key) == null)
            {
                System.Diagnostics.Debug.WriteLine("no cache found or old cache has expired");
                var cacheItemPolicy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(ONE_DAY_IN_SECONDS),
                };
                T value = (T)Activator.CreateInstance(typeof(T), key);
                cache.Add(key, value, cacheItemPolicy);
            }
            return (T)cache.Get(key);
        }
    }
}
