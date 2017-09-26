using System;
using System.Diagnostics;
using static System.Console;

namespace Curlies
{
    class Program
    {
        static void Main(string[] args)
        {
            const int count = 1000000;
            var value = 0;
            var sw = new Stopwatch();


            WriteLine("With {");
            sw.Start();
            for (int i = 0; i < count; i++)
            {
                if (i % 2 == 0)
                {
                    value = count;
                }
            }
            sw.Stop();
            WriteLine(sw.ElapsedTicks);

            sw.Reset();

            WriteLine("Without {");
            sw.Start();
            for (int i = 0; i < count; i++)
                if (i % 2 == 0)
                    value = count;
            sw.Stop();
            WriteLine(sw.ElapsedTicks);

            ReadLine();
        }
    }
}
