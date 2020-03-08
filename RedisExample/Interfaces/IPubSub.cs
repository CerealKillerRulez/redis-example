using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace RedisExample
{
    public interface IPubSub
    {
        long PublishMessage(string channel, string message);
        Task<long> PublishMessageAsync(string channel, string message);
        void PullMessages(string channel, Func<RedisValue, bool> func);
    }
}
