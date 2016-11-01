using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

namespace WebClientProxy
{
    public static class WebApiClient
    {
        private readonly static HttpClient _client = new HttpClient();
        private const string _apiBaseUrl = "http://localhost:64217/api/"; // to retrieve from configuration

        static WebApiClient()
        {
            _client.BaseAddress = new Uri(_apiBaseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task Invoke<TServiceInterface>(Expression<Action<TServiceInterface>> call)
        {
            var methodCall = call.Body as MethodCallExpression;

            var url = GetUrl<TServiceInterface>(methodCall);

            if (methodCall.Arguments.Count == 0)
            {
                var response = await _client.GetAsync(url);
            }
        }

        public static async Task<string> Invoke<TServiceInterface>(Expression<Func<TServiceInterface, string>> call)
        {
            var methodCall = call.Body as MethodCallExpression;

            var url = GetUrl<TServiceInterface>(methodCall);

            HttpResponseMessage response = null;

            if (methodCall.Arguments.Count == 0)
            {
                response = await _client.GetAsync(url);
            }
            else if (methodCall.Arguments.Count == 1)
            {
                response = await _client.PostAsJsonAsync(url, GetValueFromArgument(methodCall.Arguments[0]));
            }

            return await response?.Content.ReadAsStringAsync();
        }

        public static async Task<TResult> Invoke<TServiceInterface, TResult>(Expression<Func<TServiceInterface, TResult>> call)
            where TResult : class, new()
        {
            var methodCall = call.Body as MethodCallExpression;

            var url = GetUrl<TServiceInterface>(methodCall);

            if (methodCall.Arguments.Count == 0)
            {
                var response = await _client.GetAsync(url);

                if (typeof(TResult) == typeof(string))
                    return await (response.Content.ReadAsStringAsync() as Task<TResult>);

                return await response.Content.ReadAsAsync<TResult>();
            }

            throw new NotImplementedException();
        }

        private static object GetValueFromArgument(Expression expression)
        {
            var memberExpression = expression as MemberExpression;

            var constantExpression = memberExpression.Expression as ConstantExpression;
            var value = constantExpression.Value;
            var field = value.GetType().GetTypeInfo().DeclaredFields.FirstOrDefault(fi => fi.Name == memberExpression.Member.Name);
            return field.GetValue(value);
        }

        private static string GetUrl<TServiceInterface>(MethodCallExpression method)
        {
            return $"{typeof(TServiceInterface).Name.Substring(1)}/{method.Method.Name}";
        }
    }
}
