using FirstLevelLibrary;
using static System.Console;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine(FirstLevel.GetContent());
            ReadLine();
        }
    }
}
