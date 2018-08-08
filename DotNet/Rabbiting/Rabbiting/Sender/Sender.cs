using RabbitMQ.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sender
{
    class Sender
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.ColoredConsole().CreateLogger();
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://localhost:5672/")
            };

            var exchangeName = "theExchange";
            var queueName = "theQueue";
            var routingKey = "theRoutingKey";

            var key = ConsoleKey.A;

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.CallbackException += (s, e) => Log.Error(e.Exception, "Error in channel");
                channel.BasicReturn += (s, e) => Log.Warning("Message of type {type} returned. {body}", e.BasicProperties.Type, Encoding.UTF8.GetString(e.Body));

                do
                {
                    try
                    {
                        Log.Information("Press any key to send message");
                        key = Console.ReadKey().Key;
                        if (key == ConsoleKey.Q) break;

                        string message = "Hello World!";
                        var body = Encoding.UTF8.GetBytes(message);

                        var props = channel.CreateBasicProperties();
                        props.Type = "TheType";
                        props.Headers = new Dictionary<string, object>();
                        props.Headers.Add("subject", routingKey);

                        channel.BasicPublish(exchange: exchangeName,
                                             routingKey: routingKey,
                                             mandatory: true,
                                             basicProperties: props,
                                             body: body);

                        Log.Information("Message {body} sent to {exchange} with {routingKey}", message, exchangeName, routingKey);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Error sending message");
                    }
                } while (key != ConsoleKey.Q);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
