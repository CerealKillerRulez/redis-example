using StackExchange.Redis;
using System;
using System.Linq;

namespace RedisExample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var redis = ConnectionMultiplexer.Connect("localhost"))
            {
                RedisCacheContextExample(redis);
                RedisPubSubContextExample(redis);                
            }
            Console.ReadKey();
        }

        private static void RedisCacheContextExample(IConnectionMultiplexer connection)
        {
            const string KEY = "CHIAVE";
            var cache = new CacheService(connection);
            
            //Salvo un valore in cache
            cache.SetKey(KEY, "nuovo valore", TimeSpan.FromSeconds(2));
            // System.Threading.Thread.Sleep(5000);
            Console.WriteLine($"Valore in cache: '{cache.GetKey(KEY)}'");

            //Salvo un valore volatile in cache impostando la scadenza dopo 2 secondi
            cache.SetKey(KEY, "nuovo valore", TimeSpan.FromSeconds(2));
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine($"Valore volatile in cache: '{cache.GetKey(KEY)}'");

            cache.SetKey(KEY, "nuovo valore", TimeSpan.FromSeconds(2));
            Console.WriteLine($"Valore volatile in cache (senza Sleep): '{cache.GetKey(KEY)}'");


        }

        private static void RedisPubSubContextExample(IConnectionMultiplexer connection)
        {
            const string KEY_CHANNEL = "CANALE";
            var pubsub = new PubSubService(connection);

            //Questi messaggi verranno ignorati poiché il subscriber non è ancora in ascolto della coda
            pubsub.PublishMessage(KEY_CHANNEL, "prova 1");
            pubsub.PublishMessage(KEY_CHANNEL, "prova 2");

            pubsub.PullMessages(KEY_CHANNEL, (message) => {
                Console.WriteLine(message);
                return true;
            });

            //Da qui in poi tutti i messaggi che arrivano verranno pushati al subscriber
            foreach (int i in Enumerable.Range(1, 1000))
            {
                pubsub.PublishMessage(KEY_CHANNEL, $"prova {i}");
                System.Threading.Thread.Sleep(200);
            }
        }
    }
}
