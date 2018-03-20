using Microsoft.Azure.Amqp;
using Microsoft.Azure.Amqp.Framing;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AmqpClient
{
    class AmqpClient : IAmqpClient
    {
        public event EventHandler<QueueMessageArgs> MessageArrived;
        int _tagCount = 0;
        private const string QueueName = "amqpQueue";

        public Task<bool> ConnectAsync()
        {
            try
            {
                StartReceiver();

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
        }

        private void StartReceiver()
        {
            Task.Run(async () =>
            {
                var tfactory = new AmqpConnectionFactory();
                var connection = await tfactory.OpenConnectionAsync("amqp://localhost:5672", TimeSpan.FromSeconds(10));

                var session = connection.CreateSession(CreateSessionSettings());

                var receicerSettings = new AmqpLinkSettings
                {
                    LinkName = $"receiver-{DateTime.UtcNow.Ticks}",
                    Role = true,
                    Source = new Source
                    {
                        Address = QueueName,
                        //DynamicNodeProperties = CreateProperties(),
                        //Value = "sourceValue",
                        Durable = 1
                    },
                    Properties = CreateProperties()
                };

                var receiver = new ReceivingAmqpLink(session, receicerSettings);
                await Task.WhenAll(
                session.OpenAsync(session.DefaultOpenTimeout),
                receiver.OpenAsync(receiver.DefaultOpenTimeout));

                while (true)
                {

                    AmqpMessage message = await receiver.ReceiveMessageAsync(TimeSpan.FromSeconds(20));

                    if (message != null)
                    {
                        receiver.DisposeDelivery(message, true, AmqpConstants.AcceptedOutcome);
                        ReceiveMessage(message);
                    }
                }
            });
        }

        public async Task<bool> SendAsync(string body)
        {
            try
            {
                var tfactory = new AmqpConnectionFactory();
                var connection = await tfactory.OpenConnectionAsync(new Uri("amqp://guest:guest@localhost:5672"), TimeSpan.FromSeconds(30));

                var session = connection.CreateSession(CreateSessionSettings());
                var sender = CreateSender(session);
                await Task.WhenAll(
                    session.OpenAsync(session.DefaultOpenTimeout),
                    sender.OpenAsync(sender.DefaultOpenTimeout));

                var message = AmqpMessage.Create(new AmqpValue { Value = body });
                //message.Header.Value = "amqpHeader";
                //message.Footer.Value = "amqpFooter";
                message.Properties.AbsoluteExpiryTime = new DateTime(2025, 10, 27);
                message.Properties.Subject = "propertiesSubject";
                message.Properties.To = "propertiesTo";
                message.Properties.ReplyTo = "propertiesReplyTo";
                message.Properties.Value = "thePropertiesValue";
                message.Properties.ContentType = new Microsoft.Azure.Amqp.Encoding.AmqpSymbol("appliciton/json");
                
                //message.ApplicationProperties.Value = CreateProperties();

                var tag = new ArraySegment<byte>(BitConverter.GetBytes(_tagCount++));
                var outcome = await sender.SendMessageAsync(message, tag, AmqpConstants.NullBinary, TimeSpan.FromSeconds(5));
                if (outcome.DescriptorCode == Accepted.Code)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private AmqpSessionSettings CreateSessionSettings()
        {
            return new AmqpSessionSettings
            {
                //Properties = CreateProperties()
            };
        }

        private Fields CreateProperties()
        {
            var properties = new Fields();
            properties.Add(Properties.SubjectName, "amqpExchange");
            return properties;
        }

        private SendingAmqpLink CreateSender(AmqpSession session)
        {
            AmqpLinkSettings settings = new AmqpLinkSettings();
            settings.LinkName = $"sender-{DateTime.UtcNow.Ticks}";
            settings.Role = false;
            settings.Target = new Target()
            {
                Address = QueueName
            };
            settings.InitialDeliveryCount = 0;
            settings.Properties = CreateProperties();
            return new SendingAmqpLink(session, settings);
        }

        private void ReceiveMessage(AmqpMessage message)
        {
            var result = new StringBuilder();
            result.AppendLine("AbsoluteExpiryTime: " + message.Properties.AbsoluteExpiryTime);
            result.AppendLine("MessageAnnotations: " + message.MessageAnnotations);
            result.AppendLine("DeliveryAnnotations: " + message.DeliveryAnnotations);
            result.AppendLine("BodyType: " + message.BodyType);
            result.AppendLine("Link: " + message.Link);
            result.AppendLine("MessageFormat: " + message.MessageFormat);
            result.AppendLine("Sections: " + message.Sections);
            result.AppendLine("Header: " + message.Header.Value);
            result.AppendLine("Footer: " + message.Footer.Value);
            result.AppendLine("ApplicationProperties: " + message.ApplicationProperties.Value);
            result.AppendLine("Properties:");
            result.AppendLine(message.Properties.ToString());
            result.AppendLine("Subject:" + message.Properties.Subject);
            result.AppendLine("Value:" + message.Properties.Value);
            result.AppendLine();
            result.AppendLine("Body:");
            result.AppendLine(message.ValueBody.Value as string);

            MessageArrived?.Invoke(this, new QueueMessageArgs(result.ToString()));
        }
    }
}
