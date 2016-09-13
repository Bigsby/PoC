using System;
using System.Linq.Expressions;
using System.ServiceModel;
using System.Threading.Tasks;

namespace GenericWcfClient
{
    public static class Caller<TService>
    {
        public static void Call(Expression<Action<TService>> func)
        {
            CallInt(func);
        }

        public static async Task CallAsync(Expression<Action<TService>> func)
        {
            await Task.Run(() => Call(func));
        }

        public static TResult Call<TResult>(Expression<Func<TService, TResult>> func)
        {
            return CallInt(func);
        }

        public static async Task<TResult> CallAsync<TResult>(Expression<Func<TService, TResult>> func)
        {
            return await Task.Run(() => Call(func));
        }

        private static void CallInt(Expression<Action<TService>> func)
        {
            var factory = new ChannelFactory<TService>("*");

            var channel = factory.CreateChannel(factory.Endpoint.Address);

            func.Compile()(channel);

            factory.Close();
        }

        private static TResult CallInt<TResult>(Expression<Func<TService, TResult>> func)
        {
            var factory = new ChannelFactory<TService>(new BasicHttpBinding(), new EndpointAddress("http://localhost:54264/Service.svc"));

            var channel = factory.CreateChannel(factory.Endpoint.Address);

            var result = func.Compile()(channel);

            factory.Close();

            return result;
        }
    }
}