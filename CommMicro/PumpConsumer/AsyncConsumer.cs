using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace PumpConsumer
{
    public class AsyncConsumer : AsyncEventingBasicConsumer
    {
        private readonly IModel channel;

        public AsyncConsumer(IModel channel) : base(channel)
        {
            this.channel = channel;
        }

        public override async Task HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey,
          IBasicProperties properties, byte[] body)
        {
            try
            {
                var response = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Got {response}");

                channel.BasicAck(deliveryTag, false);

            }
            catch (Exception)
            {
                channel.BasicNack(deliveryTag, false, true);
            }

            Task.WaitAll();
            await Task.Yield();
        }

    }
}

