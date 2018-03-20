using Microsoft.Azure.Amqp;
using Microsoft.Azure.Amqp.Framing;
using System;

namespace AMQP1Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var connection = AmqpConnection.Factory.OpenConnectionAsync("amqp://localhost:5672").Result;

                var sessionSettings = new AmqpSessionSettings
                {
                };
                //AmqpSessionSettings.Create(new Begin())
                var session = connection.CreateSession(sessionSettings);

                var linkSettings = new AmqpLinkSettings
                {
                    LinkName = "theExchange",
                    Handle = 1234,

                    Role = false,
                    Target = new Target
                    {
                        Address = "theQueue",
                        Durable = 1
                    }
                };

                var sender = new SendingAmqpLink(session, linkSettings);
                session.Open();
                sender.Open();
                var key = ConsoleKey.A;
                var i = 0;
                do
                {
                    Console.WriteLine("Press any key to send message");

                    var body = "Hello, AMQP1.0!";
                    var message = AmqpMessage.Create(new AmqpValue() { Value = body });
                    var tag = new ArraySegment<byte>(BitConverter.GetBytes(i++));
                    var outcome = sender.SendMessageAsync(message, tag, AmqpConstants.NullBinary, TimeSpan.FromSeconds(3)).Result;

                    if (outcome.DescriptorCode == Accepted.Code)
                        Console.WriteLine(" [x] Sent {0}", body);
                    else
                        Console.WriteLine("Error sending message!");

                } while (key != ConsoleKey.Q);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
