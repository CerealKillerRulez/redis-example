using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace RedisExample
{
    public interface ICache
    {
        bool SetKey(string key, string value, TimeSpan? expiry = null, bool isFireAndForget = false);
        RedisValue GetKey(string key);
        bool DeleteKey(string key);
        bool MoveKey(string key);
        bool ExpireKey(string key, TimeSpan expiry);
        bool RenameKey(string key, string newkey);
        RedisValue Wait(Task<RedisValue> valuePending);

        Task<bool> SetKeyAsync(string key, string value, TimeSpan? expiry = null, bool isFireAndForget = false);
        Task<RedisValue> GetKeyAsync(string key);
        Task<bool> DeleteKeyAsync(string key);
        Task<bool> MoveKeyAsync(string key);
        Task<bool> ExpireKeyAsync(string key, TimeSpan expiry);
        Task<bool> RenameKeyAsync(string key, string newkey);


    }
}
