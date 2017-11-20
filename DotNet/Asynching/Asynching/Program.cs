using System;
using System.Threading.Tasks;

namespace Asynching
{
    class Program
    {
        private static TaskCompletionSource<bool> _tcs;
        static void Main(string[] args)
        {
            var date = DateTime.Now;
            Console.WriteLine($"{date:s}");
            DoAsyncStuff().Wait();
            Console.WriteLine("Done waiting");
        }

        static async Task DoAsyncStuff()
        {
            _tcs = new TaskCompletionSource<bool>();

            var doOne = DoOne();
            var timeout = Task.Delay(2000);

            var allTasks = Task.WhenAll(doOne, _tcs.Task);

            if (allTasks != await Task.WhenAny(allTasks, timeout))
            {
                Console.WriteLine("Timeout finished first!");
            }
            else
            {
                Console.WriteLine("All finished first!");
            }

            Console.WriteLine("Done all!");
            Console.ReadLine();
        }

        static async Task DoOne()
        {
            Console.WriteLine("Started one...");
            await Task.Delay(1000);
            Console.WriteLine("Done one!");
            await Task.Delay(500);
            Console.WriteLine("Setting flag...");
            _tcs.TrySetResult(true);
        }
    }
}
