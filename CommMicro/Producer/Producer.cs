using RabbitMQ.Client;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Producer
{
    class Producer
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            int i = 0;
            var timer = new Stopwatch();
            timer.Start();

            while (timer.ElapsedMilliseconds < 3000)
            {
                channel.BasicPublish(exchange: "",
                   routingKey: "rpc_queue",
                   basicProperties: channel.CreateBasicProperties(),
                   body: Encoding.UTF8.GetBytes(i.ToString()));
                System.Console.WriteLine($"Publishing : {i} to the queue");
                i++;
                Task.Delay(1000);
            }

            System.Console.WriteLine($"Time finished: produced {i} requests");
            //for (int i = 0; i < 3000; i++)
            //{

            //    channel.BasicPublish(exchange: "",
            //        routingKey: "rpc_queue",
            //        basicProperties: channel.CreateBasicProperties(),
            //        body: Encoding.UTF8.GetBytes(i.ToString()));
            //    System.Console.WriteLine($"Publishing : {i} to the queue");
            //}

        }
    }
}
