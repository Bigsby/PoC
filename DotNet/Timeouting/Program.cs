using System;
using System.Threading;
using System.Threading.Tasks;

namespace Timeouting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting");

            var tcs = new TaskCompletionSource<bool>();
            

            
            Console.ReadKey(true);
        }
    }
}
