﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPOC.Cache
{
    public abstract class Cache : ICache, ICacheStatus
    {
        private readonly bool isCacheEnable;

        public Cache(bool isCacheEnable)
        {
            this.isCacheEnable = isCacheEnable;
        }

        public void Set<T>(string key, T objectToCache, TimeSpan? expiry = null) where T : class
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if (this.isCacheEnable)
            {
                try
                {
                    var serializedObjectToCache = JsonConvert.SerializeObject(objectToCache
                         , Formatting.Indented
                         , new JsonSerializerSettings
                         {
                             ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                             PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                             TypeNameHandling = TypeNameHandling.All
                         });

                    this.SetStringProtected(key, serializedObjectToCache, expiry);
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Cannot Set {0}", key));
                    //Log.Error(string.Format("Cannot Set {0}", key), e);
                }
            }
        }

        public T Get<T>(string key) where T : class
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if (this.isCacheEnable)
            {
                try
                {
                    var stringObject = this.GetStringProtected(key);
                    if (stringObject == null)
                    {
                        return default(T);
                    }
                    else
                    {
                        var obj = JsonConvert.DeserializeObject<T>(stringObject
                            , new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                                TypeNameHandling = TypeNameHandling.All
                            });
                        return obj;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Cannot Set {0}", key));
                }
            }
            return null;
        }

        public void Delete(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if (this.isCacheEnable)
            {
                try
                {
                    this.DeleteProtected(key);
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Cannot Delete key {0}", key));
                }
            }
        }

        //public void DeleteByPattern(string prefixKey)
        //{
        //    if (string.IsNullOrEmpty(prefixKey))
        //    {
        //        throw new ArgumentNullException("prefixKey");
        //    }
        //    if (this.isCacheEnable)
        //    {
        //        try
        //        {
        //            this.DeleteByPatternProtected(prefixKey);
        //        }
        //        catch (Exception e)
        //        {
        //            throw new Exception(string.Format("Cannot DeleteByPattern key {0}", prefixKey));
        //        }
        //    }
        //}

        public void FlushAll()
        {
            if (this.isCacheEnable)
            {
                try
                {
                    this.FlushAllProtected();
                }
                catch (Exception e)
                {
                    throw new Exception("Cannot Flush", e);
                }
            }
        }

        public string GetString(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if (this.isCacheEnable)
            {
                try
                {
                    return this.GetStringProtected(key);
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Cannot Set key {0}", key));
                }
            }
            return null;
        }

        public void SetString(string key, string objectToCache, TimeSpan? expiry = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if (this.isCacheEnable)
            {
                try
                {
                    this.SetStringProtected(key, objectToCache, expiry);
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Cannot Set {0}", key));
                }
            }
        }
        public bool IsCacheEnabled
        {
            get { return this.isCacheEnable; }

        }

        protected abstract void SetStringProtected(string key, string objectToCache, TimeSpan? expiry = null);
        protected abstract string GetStringProtected(string key);
        protected abstract void DeleteProtected(string key);
        protected abstract void FlushAllProtected();
        //protected abstract void DeleteByPatternProtected(string key);
        public abstract bool IsCacheRunning { get; }
    }
}
