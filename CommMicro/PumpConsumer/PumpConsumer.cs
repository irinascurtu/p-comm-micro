using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

namespace PumpConsumer
{
    class PumpConsumer
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                VirtualHost = "/",
                DispatchConsumersAsync = true
            };

            CancellationToken stoppingToken = new CancellationToken();
            Console.WriteLine("[Started]");

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            while (!stoppingToken.IsCancellationRequested)
            {
                var consumer = new AsyncConsumer(channel);

                consumer.Received += async (model, ea) =>
                {
                    System.Console.WriteLine(Encoding.UTF8.GetString(ea.Body));
                    await consumer.HandleBasicDeliver(ea.ConsumerTag, ea.DeliveryTag, ea.Redelivered, ea.Exchange,
                           ea.RoutingKey, ea.BasicProperties, ea.Body);

                };

                channel.BasicConsume(
                    consumer: consumer,
                    queue: "pump_queue",
                    autoAck: false);

            }

            System.Console.ReadKey();
        }
    }
}
