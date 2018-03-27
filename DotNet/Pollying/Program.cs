using System;
using System.Threading.Tasks;
using Polly;

namespace Pollying
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var attempts = 0;

            var policy = Policy.Handle<Exception>()
                .WaitAndRetryForeverAsync(
                    attempt => TimeSpan.FromSeconds(2),
                    onRetry: (exception, waitPeriod) => System.Console.WriteLine($"Error: {exception.Message}. Retrying in {waitPeriod}")
                );

            System.Console.WriteLine("Starting...");

            policy.ExecuteAsync(async () => { 
                if (attempts > 3) return;
                await Task.Delay(100);

                attempts++;
                throw new Exception($"Failed {attempts} time(s)");
            });

            System.Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
