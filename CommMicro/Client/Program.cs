using RabbitMQ.Client;
using RPCClient;
using System;
using System.Diagnostics;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var rpcClient = new RpcClient(channel);

            Stopwatch timer = new Stopwatch();
            timer.Start();
            int i = 0;

            while (timer.ElapsedMilliseconds < 3000)
            {
                Console.WriteLine($"[x] Asking a question({i})");

                var response = rpcClient.Call(i.ToString());

                Console.WriteLine($" [.] Got {response} as a reply");
                i++;
            }
            Console.WriteLine($"Time finished. Processed {i} messages");

            Console.WriteLine(timer.Elapsed);
          //  rpcClient.Close();
        }
    }
}

