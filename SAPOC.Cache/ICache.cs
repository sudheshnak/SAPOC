using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPOC.Cache
{
    public interface ICache
    {
        void SetString(string key, string objectToCache, TimeSpan? expiry = null);

        void Set<T>(string key, T objectToCache, TimeSpan? expiry = null) where T : class;

        string GetString(string key);

        T Get<T>(string key) where T : class;

        void Delete(string key);

        void FlushAll();
    }
}
