using CSLibrary;
using FSLibrary;
using VBLibrary;
using static System.Console;

namespace CSConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("C# Console App here...");
            WriteLine("Calling libraries...");
            WriteLine();

            WriteLine($"Name: {CSharpLibrary.Name}");
            CSharpLibrary.WriteToConsole();
            CSharpLibrary.WriteThisToConsole("C# Console App here!");
            WriteLine("Who are you? " + CSharpLibrary.WhoAmI());
            WriteLine();

            WriteLine($"Name: {FSharpLibrary.Name}");
            FSharpLibrary.WriteToConsole();
            FSharpLibrary.WriteThisToConsole("C# Console App here!");
            WriteLine("Who are you? " + FSharpLibrary.WhoAmI());
            WriteLine();

            WriteLine($"Name: {VBNetLibrary.Name}");
            VBNetLibrary.WriteToConsole();
            VBNetLibrary.WriteThisToConsole("C# Console App here!");
            WriteLine("Who are you? " + VBNetLibrary.WhoAmI());


            WriteLine("Done!");
            ReadLine();
        }
    }
}
