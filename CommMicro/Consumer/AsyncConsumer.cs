using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer
{
    public class AsyncConsumer : AsyncEventingBasicConsumer
    {
        public AsyncConsumer(IModel model, SemaphoreSlim semaphoreSlim) : base(model)
        {
        }

    }
}
