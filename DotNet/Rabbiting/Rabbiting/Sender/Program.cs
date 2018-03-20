using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<AClass>();

            var factory = new ConnectionFactory
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

            var exchangeName = "theExchange";
            var queueName = "theQueue";
            var routingKey = "theRoutingKey";

            var key = ConsoleKey.A;

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queueName, true, false, false, null);

                do
                {
                    Console.WriteLine("Press any key to send message");
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
                                         basicProperties: props,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                } while (key != ConsoleKey.Q);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        public class AClass
        {
            public string Name { get; set; }
        }
    }
}
