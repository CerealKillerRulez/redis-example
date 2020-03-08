using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace RedisExample
{
    public class CacheService : ICache
    {
        private readonly IConnectionMultiplexer Connection;
        private IDatabase Db { get; set; }

        public CacheService(IConnectionMultiplexer connection)
        {
            Connection = connection;
            Db = Connection.GetDatabase();
        }

        public bool SetKey(string key, string value, TimeSpan? expiry = null, bool isFireAndForget = false)
        {
            var commandFlags = isFireAndForget ? CommandFlags.FireAndForget : CommandFlags.None;
            return Db.StringSet(key, value, expiry, flags: commandFlags);
        }

        public Task<bool> SetKeyAsync(string key, string value, TimeSpan? expiry = null, bool isFireAndForget = false)
        {
            var commandFlags = isFireAndForget ? CommandFlags.FireAndForget : CommandFlags.None;
            return Db.StringSetAsync(key, value, expiry, flags: commandFlags);
        }

        public RedisValue GetKey(string key)
        {
            return Db.StringGet(key);
        }

        public Task<RedisValue> GetKeyAsync(string key)
        {
            return Db.StringGetAsync(key);
        }

        public RedisValue Wait(Task<RedisValue> valuePending)
        {
            return Db.Wait(valuePending);
        }

        #region NOT IMPLEMENTED METHODS
        public bool DeleteKey(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public bool ExpireKey(string key, TimeSpan expiry)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExpireKeyAsync(string key, TimeSpan expiry)
        {
            throw new NotImplementedException();
        }

        public bool MoveKey(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MoveKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public bool RenameKey(string key, string newkey)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RenameKeyAsync(string key, string newkey)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
