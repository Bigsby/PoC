using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;

namespace Rabbiting
{
    class Program
    {
        static IModel channel;
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole(restrictedToMinimumLevel: LogEventLevel.Verbose)
                .CreateLogger();

            Info(nameof(Program), "Starting...");
            var connectionFactoy = new ConnectionFactory
            {
                Uri = new Uri("amqp://localhost:5672/"),
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
                TopologyRecoveryEnabled = true
            };
            var connection = connectionFactoy.CreateConnection();

            #region Connection Events
            connection.CallbackException += (s, e) => Error(nameof(connection), nameof(connection.CallbackException), e.Exception);
            connection.ConnectionBlocked += (s, e) => Warn(nameof(connection), nameof(connection.ConnectionBlocked));
            connection.ConnectionRecoveryError += (s, e) => Warn(nameof(connection), nameof(connection.ConnectionRecoveryError), e.Exception);
            connection.ConnectionShutdown += (s, e) => Warn(nameof(connection), nameof(connection.ConnectionShutdown));
            connection.RecoverySucceeded += (s, e) => Info(nameof(connection), nameof(connection.RecoverySucceeded));
            #endregion

            var exchangeName = "theExchange";
            var queueName = "theQueue";
            var routingKey = "theRoutingKey";
            var consumerTag = "theConsumerTag";

            channel = connection.CreateModel();
            channel.BasicQos(0, 3, false);
            
            #region Channel Events
            channel.ModelShutdown += (s, e) => Warn(nameof(channel), nameof(channel.ModelShutdown));
            channel.CallbackException += (s, e) => Warn(nameof(channel), nameof(channel.CallbackException), e.Exception);
            #endregion

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true, false, null);
            channel.QueueDeclare(queueName, true, false, false, new Dictionary<string, object> {
                { "prop1", "value1" },
                { "prop2", "value2" }
            });
            channel.QueueBind(queueName, exchangeName, routingKey, new Dictionary<string, object> {
                { "prop3", "value3" },
                { "prop4", "value4" }
            });

            var consumer = new EventingBasicConsumer(channel);
            consumer.ConsumerTag = consumerTag;

            #region Receiver Events
            consumer.ConsumerCancelled += (s, e) => Warn(nameof(consumer), nameof(consumer.ConsumerCancelled));
            consumer.Shutdown += (s, e) => Warn(nameof(consumer), nameof(consumer.Shutdown));
            consumer.Received += (sender, e) => Info(nameof(consumer), nameof(consumer.Received), System.Text.Encoding.UTF8.GetString(e.Body));
            consumer.Registered += (s, e) => Info(nameof(consumer), nameof(consumer.Registered));
            consumer.Unregistered += (s, e) => Warn(nameof(consumer), nameof(consumer.Unregistered));
            #endregion

            channel.BasicConsume(queueName, true, consumerTag, consumer);
            Info(nameof(Program), "Listening...");
            var key = ConsoleKey.A;
            while (key != ConsoleKey.Q)
            {
                var keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;
            }
        }

        static void Info(string component, string eventName, string data = "")
        {
            Log.Information("{component}.{event}: {data}", component, eventName, data);
        }

        static void Warn(string component, string eventName, Exception exception = null)
        {
            if (exception == null)
                Log.Warning("{component}.{event}", component, eventName);
            else
                Log.Warning(exception, "{component}.{event}: {data}", component, eventName);
        }

        static void Error(string component, string eventName, Exception exception = null)
        {
            if (exception == null)
                Log.Error("{component}.{event}", component, eventName);
            else
                Log.Error(exception, "{component}.{event}: {data}", component, eventName);
        }
    }
}
