using System;
using System.ComponentModel;

namespace CoreGeneric
{
    class Program
    {
        static void Main(string[] args)
        {
            var converter = TypeDescriptor.GetConverter(typeof(int));

            if (converter.CanConvertFrom(typeof(string))){
                System.Console.WriteLine("I can convert");
                var value = converter.ConvertFrom("");
                System.Console.WriteLine("Converted: " + value);
            } else {
                System.Console.WriteLine("I CANNOT convert");
            }
            Console.WriteLine("Done!");
        }
    }
}
