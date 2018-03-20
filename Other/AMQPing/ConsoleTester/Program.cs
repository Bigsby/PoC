using Microsoft.Azure.Amqp;
using Microsoft.Azure.Amqp.Framing;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleTester
{
    class Program
    {
        static int count = 0;

        static void Main(string[] args)
        {
            //TestLocalReceiver();
            TestPCL();
            //TestLocal();


            Console.WriteLine("Terminated!");
            Console.ReadLine();
        }

        private static void DisplayMessage(string body)
        {
            Console.WriteLine("Message received:");
            Console.WriteLine(body);
        }

        static int _tagCount = 0;
        private const string QueueName = "amqpQueue";

        private static void TestLocal()
        {
            StartReceiver();

            var keyPressed = ConsoleKey.A;

            do
            {
                Console.WriteLine("Press Q to quit, any other key to send message");

                keyPressed = Console.ReadKey().Key;
                if (keyPressed != ConsoleKey.Q)
                {

                    Task.Run(async () => { 
                    var tfactory = new AmqpConnectionFactory();
                    var connection = await tfactory.OpenConnectionAsync(new Uri("amqp://localhost:5672"), TimeSpan.FromSeconds(30));
                    var session = connection.CreateSession(new AmqpSessionSettings());
                    var sender = CreateSender(session);
                    await Task.WhenAll(
                        session.OpenAsync(session.DefaultOpenTimeout),
                        sender.OpenAsync(sender.DefaultOpenTimeout));

                    var message = AmqpMessage.Create(new AmqpValue() { Value = $"Message number {++count}" });
                    var tag = new ArraySegment<byte>(BitConverter.GetBytes(_tagCount++));
                    var outcome = await sender.SendMessageAsync(message, tag, AmqpConstants.NullBinary, TimeSpan.FromSeconds(5));

                    var sent = outcome.DescriptorCode == Accepted.Code;

                    if (sent)
                        Console.WriteLine("Message Sent!");
                    else
                        Console.WriteLine("Message NOT Sent!");
                    });
                }
            } while (keyPressed != ConsoleKey.Q);
        }

        private static SendingAmqpLink CreateSender(AmqpSession session)
        {
            AmqpLinkSettings settings = new AmqpLinkSettings();
            settings.LinkName = $"sender-{DateTime.UtcNow.Ticks}";
            settings.Role = false;
            settings.Source = new Source(); ;
            settings.Target = new Target() { Address = QueueName }; ;
            settings.InitialDeliveryCount = 0;
            return new SendingAmqpLink(session, settings);
        }

        private static void StartReceiver()
        {
            Task.Run(async () => {
                var tfactory = new AmqpConnectionFactory();
                var connection = await tfactory.OpenConnectionAsync("amqp://localhost:5672", TimeSpan.FromSeconds(10));

                var session = connection.CreateSession(new AmqpSessionSettings());

                var receicerSettings = new AmqpLinkSettings
                {
                    LinkName = $"receiver-{DateTime.UtcNow.Ticks}",
                    Role = true,
                    TotalLinkCredit = 300,
                    Source = new Source
                    {
                        Address = QueueName,
                    },
                    Target = new Target()
                };

                var receiver = new ReceivingAmqpLink(session, receicerSettings);

                while (true)
                {

                    AmqpMessage message = await receiver.ReceiveMessageAsync(TimeSpan.FromSeconds(20));

                    if (message != null)
                    {
                        receiver.DisposeDelivery(message, true, AmqpConstants.AcceptedOutcome);
                        DisplayMessage(new StreamReader(message.BodyStream).ReadToEnd());
                    }
                }
            });
        }

        private static void TestLocalReceiver()
        {
            var tfactory = new AmqpConnectionFactory();
            var connection = tfactory.OpenConnectionAsync("amqp://localhost:5672", TimeSpan.FromSeconds(10)).Result;

            var session = connection.CreateSession(new AmqpSessionSettings());

            var receicerSettings = new AmqpLinkSettings
            {
                LinkName = $"receiver-{DateTime.UtcNow.Ticks}",
                Role = true,
                TotalLinkCredit = 300,
                Source = new Source
                {
                    Address = QueueName
                },
                Target = new Target()
            };

            var receiver = new ReceivingAmqpLink(session, receicerSettings);
            Task.WhenAll(
                session.OpenAsync(TimeSpan.FromSeconds(30)),
                receiver.OpenAsync(TimeSpan.FromSeconds(30))).Wait();

            Console.WriteLine("Listening");
            while (true)
            {

                AmqpMessage message = receiver.ReceiveMessageAsync(receiver.DefaultOpenTimeout).Result;

                if (message != null)
                {
                    receiver.DisposeDelivery(message, true, AmqpConstants.AcceptedOutcome);
                    DisplayMessage(new StreamReader(message.BodyStream).ReadToEnd());
                }
            }
        }

        private static void TestPCL()
        {
            var client = AmqpClient.AmqpClientFactory.GetClient();

            var connected = client.ConnectAsync().Result;

            client.MessageArrived += (s, e) => DisplayMessage(e.Body);

            if (connected)
            {
                var keyPressed = ConsoleKey.A;

                do
                {
                    Console.WriteLine("Press Q to quit, any other key to send message");

                    keyPressed = Console.ReadKey().Key;
                    if (keyPressed != ConsoleKey.Q)
                    {
                        var sent = client.SendAsync($"Message number {++count}").Result;

                        if (sent)
                            Console.WriteLine("Message Sent!");
                        else
                            Console.WriteLine("Message NOT Sent!");
                    }
                } while (keyPressed != ConsoleKey.Q);
            }
        }
    }
}
