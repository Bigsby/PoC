using System;

namespace Extending
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello".PlusDot());
            try
            {
                Console.Write(((string)null).PlusDot());
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            Console.ReadLine();
        }
    }

    public static class Extensions
    {
        public static string PlusDot(this string value)
        {
            if (null == value)
                throw new ArgumentNullException(nameof(value));

            return value + ".";
        }
    }
}
