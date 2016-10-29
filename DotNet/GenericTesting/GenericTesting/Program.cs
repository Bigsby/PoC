using static System.Console;
using System.Linq;

namespace GenericTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = new[] { 1, 2, 3, 4, 5 };

            foreach (var number in numbers.Take(10))
                WriteLine($"- {number}");

            ReadLine();
        }
    }
}
