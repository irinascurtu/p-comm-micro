using RabbitMQ.Client;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Pump
{
    class Pump
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", VirtualHost = "/" };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "pump_queue", durable: true,
                 exclusive: false, autoDelete: false, arguments: null);

            int i = 0;
            var timer = new Stopwatch();
            timer.Start();
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(TimeSpan.FromSeconds(20));
            while (!tokenSource.IsCancellationRequested)
            {
                channel.BasicPublish(exchange: "",
                   routingKey: "pump_queue",
                   basicProperties: channel.CreateBasicProperties(),
                   body: Encoding.UTF8.GetBytes(i.ToString()));
                Console.WriteLine($"Pumping : {i} th message the queue");
                i++;
            }
            timer.Stop();
            Console.WriteLine($"pushed {i} requests in {timer.Elapsed}");

        }
    }
}
