using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPOC.Cache
{
    public class RedisCache : Cache
    {
        private ConnectionMultiplexer redisConnections;

        private IDatabase RedisDatabase
        {
            get
            {
                if (this.redisConnections == null)
                {
                    InitializeConnection();
                }
                return this.redisConnections != null ? this.redisConnections.GetDatabase() : null;
            }
        }

        public RedisCache(bool isCacheEnabled)
            : base(isCacheEnabled)
        {
            InitializeConnection();
        }

        private void InitializeConnection()
        {
            try
            {
                this.redisConnections = ConnectionMultiplexer.Connect(ConfigurationManager.AppSettings["RedisConnectionString"]);
            }
            catch (RedisConnectionException errorConnectionException)
            {
                throw new Exception("Error connecting the redis cache : " + errorConnectionException.Message, errorConnectionException);
            }
        }

        protected override string GetStringProtected(string key)
        {
            if (this.RedisDatabase == null)
            {
                return null;
            }
            var redisObject = this.RedisDatabase.StringGet(key);
            if (redisObject.HasValue)
            {
                return redisObject.ToString();
            }
            else
            {
                return null;
            }
        }

        protected override void SetStringProtected(string key, string objectToCache, TimeSpan? expiry = null)
        {
            if (this.RedisDatabase == null)
            {
                return;
            }

            this.RedisDatabase.StringSet(key, objectToCache, expiry);
        }

        protected override void DeleteProtected(string key)
        {
            if (this.RedisDatabase == null)
            {
                return;
            }
            this.RedisDatabase.KeyDelete(key);
        }

        protected override void FlushAllProtected()
        {
            if (this.RedisDatabase == null)
            {
                return;
            }
            var endPoints = this.redisConnections.GetEndPoints();
            foreach (var endPoint in endPoints)
            {
                var server = this.redisConnections.GetServer(endPoint);
                server.FlushAllDatabases();
            }
        }

        public override bool IsCacheRunning
        {
            get { return this.redisConnections != null && this.redisConnections.IsConnected; }
        }
    }
}
