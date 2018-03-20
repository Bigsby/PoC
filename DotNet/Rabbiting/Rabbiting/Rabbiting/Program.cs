using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace Rabbiting
{
    class Program
    {
        static IModel channel;
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            var connectionFactoy = new ConnectionFactory
            {
                Uri = new Uri("amqp://localhost:5672/")
                //,
                //Ssl = new SslOption
                //{
                //    Enabled = false,
                //    AcceptablePolicyErrors = System.Net.Security.SslPolicyErrors.None
                //},
                //AutomaticRecoveryEnabled = true,
                //NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
                //TopologyRecoveryEnabled = true
            };

            var connection = connectionFactoy.CreateConnection();

            var exchangeName = "theExchange";
            var queueName = "theQueue";
            var routingKey = "theRoutingKey";
            var consumerTag = "theConsumerTag";

            channel = connection.CreateModel();
            channel.BasicQos(0, 3, false);
            channel.ModelShutdown += OnChannelShutdown;
            channel.CallbackException += OnChannelCallbackException;
            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true, false, null);
            channel.QueueDeclare(queueName, true, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.ConsumerTag = consumerTag;
            consumer.ConsumerCancelled += OnConsumerCancelled;
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Received += HandleMessage;

            channel.BasicConsume(queueName, false, consumerTag, consumer);
            Console.WriteLine("Listening...");
            Console.ReadLine();
        }

        private static void OnChannelCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void OnChannelShutdown(object sender, ShutdownEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void HandleMessage(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine($"Received {e.BasicProperties.Type}: " + System.Text.Encoding.UTF8.GetString(e.Body));
            channel.BasicAck(e.DeliveryTag, true);
        }

        private static void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void OnConsumerCancelled(object sender, ConsumerEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
