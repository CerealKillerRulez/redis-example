using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace RedisExample
{
    public class PubSubService : IPubSub
    {
        private readonly IConnectionMultiplexer Connection;
        private readonly ISubscriber Subscriber;

        public PubSubService(IConnectionMultiplexer connection)
        {
            Connection = connection;
            Subscriber = Connection.GetSubscriber();
        }

        public long PublishMessage(string channel, string message)
        {
            return Subscriber.Publish(channel, message);
        }

        public Task<long> PublishMessageAsync(string channel, string message)
        {
            return Subscriber.PublishAsync(channel, message);
        }

        public void PullMessages(string channel, Func<RedisValue, bool> func)
        {
            Subscriber.Subscribe(channel, (channel, message) =>
            {
                func(message);
            });
        }
    }
}
