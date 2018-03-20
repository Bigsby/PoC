using Microsoft.Azure.Amqp;
using System;
using System.Threading.Tasks;

namespace AmqpClient
{
    public interface IAmqpClient 
    {
        Task<bool> ConnectAsync();
        Task<bool> SendAsync(string body);
        event EventHandler<QueueMessageArgs> MessageArrived;
    }

    public class QueueMessageArgs : EventArgs
    {
        public QueueMessageArgs(string body)
        {
            Body = body;
        }

        public string Body { get; }
    }
}