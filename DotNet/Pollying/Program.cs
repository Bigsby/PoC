using System;
using System.Threading.Tasks;
using Polly;

namespace Pollying
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine(DateTime.Now);

            var attempts = 0;

            var policy = Policy.Handle<Exception>()
                .WaitAndRetryForeverAsync(
                    attempt => TimeSpan.FromSeconds(2),
                    onRetry: (exception, waitPeriod) => System.Console.WriteLine($"Error: {exception.Message}. Retrying in {waitPeriod}")
                );

            System.Console.WriteLine("Starting...");

            policy.ExecuteAsync(async () =>
            {
                System.Console.WriteLine(DateTime.Now);
                if (attempts > 3) return;
                await Task.Delay(100);

                attempts++;
                throw new Exception($"Failed {attempts} time(s)");
            }).Wait();

            System.Console.WriteLine("2 Starting...");
            attempts = 0;

            policy.ExecuteAsync(async () =>
            {
                System.Console.WriteLine("2 " + DateTime.Now);
                if (attempts > 3) return;
                await Task.Delay(100);

                attempts++;
                throw new Exception($"2 Failed {attempts} time(s)");
            }).Wait();

            System.Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
