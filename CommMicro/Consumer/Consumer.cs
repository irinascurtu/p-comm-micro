using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Diagnostics;
using System.Text;

namespace Consumer
{
    class Consumer
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[]Waiting for messages to consume");
            var factory = new ConnectionFactory() { HostName = "localhost" };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channel);
            var timer = new Stopwatch();
            timer.Start();
            //if (timer.ElapsedMilliseconds < 3000)
            //{

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var consumedMessage = Encoding.UTF8.GetString(body);


                //  Console.WriteLine("Consumed Message Body: " + consumedMessage);
                var fibonacci = fib(Convert.ToInt32(consumedMessage));

                Console.WriteLine($"Got {consumedMessage} and computed {fibonacci}");
                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume(
                consumer: consumer,
                queue: "rpc_queue",
                autoAck: false);

            //            }
            //          Console.WriteLine("Time finised. Consumed");
        }
        private static int fib(int n)
        {
            if (n == 0 || n == 1)
            {
                return n;
            }

            return fib(n - 1) + fib(n - 2);
        }
    }
}

