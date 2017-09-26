using Autofac;
using System;
using System.Collections.Generic;

namespace Tupling
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<HandleString>().As<IHandler<string>>().SingleInstance();
            builder.RegisterType<HandleInt>().As<IHandler<int>>().SingleInstance();
            var dic = new Dictionary<string, Tuple<Type, Func<object>>>();
            dic.Add("adfas", Tuple.Create (typeof(object), (Func<object>)null));
            Console.WriteLine("Hello World!");
        }
    }

    class HandleString : IHandler<string>
    {
        public void Handle(string parameter)
        {
            Console.WriteLine("Handling string");
        }
    }

    class HandleInt : IHandler<int>
    {
        public void Handle(int parameter)
        {
            Console.WriteLine("Handling int");
        }
    }

    interface IHandler<T>
    {
        void Handle(T parameter);
    }
}
