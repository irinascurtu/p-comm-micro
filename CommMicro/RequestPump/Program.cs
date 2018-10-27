using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;

namespace RequestPump
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            int i = 0;
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(TimeSpan.FromSeconds(20));
            while (!tokenSource.IsCancellationRequested)
            {
                var uri = "https://localhost:44364/api/values?i=" + i;
                var client = new HttpClient();
                client.GetAsync(uri);
                Console.WriteLine($"pushed {i}");
                i++;
            }
            timer.Stop();
            Console.WriteLine($"pushed {i} requests in {timer.Elapsed}");
            Console.ReadLine();
        }

    }
}
