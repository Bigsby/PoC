using static System.Console;
using System.Linq;
using System.Collections.Generic;

namespace GenericTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = DivideOrZero(12, 12);


            double x = 3;
            double y = 0;
            WriteLine("Y==0 = " + (y == 0));
            WriteLine("x/y=" + (x / y));
            WriteLine("DivindeOrZero=" + DivideOrZero(x, y));
            ReadLine();
        }

        static IEnumerable<Item> _items;

        private static double DivideOrZero(double top, double bottom)
        {
            return bottom == 0 ? 0 : top / bottom;
        }
    }

    public class Item
    { }
}
