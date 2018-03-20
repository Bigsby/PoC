using Microsoft.Azure.Amqp;
using Microsoft.Azure.Amqp.Framing;

namespace PCL.Client
{
    public static class TheClient
    {
        public static void Start()
        {
            try
            {
                var connection = AmqpConnection.Factory.OpenConnectionAsync("amqp://localhost:5672/").Result;

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
                    Source = new Source
                    {
                        Address = "theQueue",
                        Durable = 1
                    }
                };
                var receiver = new ReceivingAmqpLink(session, linkSettings);

                receiver.RegisterMessageListener(message =>
                {
                    var stop = "here";
                });
            }
            catch (System.Exception ex)
            {
                var s = ex.Message;
            }
        }
    }
}
