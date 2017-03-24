using System;

namespace CSLibrary
{
    public class CSharpLibrary
    {
        public static string Name = "C# Library";

        public static void WriteToConsole()
        {
            Console.WriteLine($"Written from {Name}");
        }

        public static void WriteThisToConsole(string what)
        {
            Console.WriteLine($"You told {Name} to write: {what}");
        }

        public static string WhoAmI() => Name;
    }
}
