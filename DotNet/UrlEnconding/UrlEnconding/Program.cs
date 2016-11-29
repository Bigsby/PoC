using PCLibrary;
using System;
using System.Linq;
using System.Web;
using static System.Console;

namespace UrlEnconding
{
    class Program
    {
        static void Main(string[] args)
        {
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12,13 };
            var httpUtil = HttpServerUtility.UrlTokenEncode(bytes);
            WriteLine($"HTTP Util: {httpUtil}_");
            var convert = UrlEncoder.Encoder(bytes);
            WriteLine($"Convert  : {convert}_");
            WriteLine("Done!");
            ReadLine();
        }
    }
}
