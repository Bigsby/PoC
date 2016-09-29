using System;
using Autofac;

namespace LoaderLibrary
{
    public static class Loader
    {
        private static IContainer _container;

        public static void Start<TShowMessage>() where TShowMessage : IShowMessage
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MessageProvider>().As<IMessageProvider>().SingleInstance();
            builder.RegisterType<TShowMessage>().As<IShowMessage>();

            _container = builder.Build();

            _container.Resolve<IShowMessage>().ShowMessage();
        }

        public static void UpdateMessage()
        {
            _container.Resolve<IShowMessage>().ShowMessage();
        }

        public class MessageProvider : IMessageProvider
        {
            private int count = 0;
            public string Message
            {
                get
                {
                    return $"This is the message #{count++}";
                }
            }
        }
    }
}