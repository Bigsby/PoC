using Newtonsoft.Json.Linq;
using System;

namespace JComparing
{
    class Program
    {
        static void Main(string[] args)
        {
            var one = JValue.FromObject("one");
            var two = JValue.FromObject("one");

            Console.WriteLine(1.CompareTo(2));
            Console.WriteLine(1.CompareTo(1));
            Console.WriteLine(1.CompareTo(0));
            Console.WriteLine(one == two);
            Console.ReadLine();
        }
    }
}
