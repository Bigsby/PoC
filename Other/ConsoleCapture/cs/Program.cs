using System;

namespace cs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Window: {Console.WindowWidth}x{Console.WindowHeight}");
            Console.CursorVisible = false;
            var startTop = Console.CursorTop;
            var startLeft = Console.CursorLeft;
            
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            while (key.Key != ConsoleKey.Q)
            {
                key = Console.ReadKey(true);
                ClearLine();
                Console.SetCursorPosition(startLeft, startTop);

                Console.Write("You clicked: ");
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Black;

                if (key.Modifiers.HasFlag(ConsoleModifiers.Control))
                    Console.Write("Ctrl + ");
                if (key.Modifiers.HasFlag(ConsoleModifiers.Shift))
                    Console.Write("Shift + ");
                if (key.Modifiers.HasFlag(ConsoleModifiers.Alt))
                    Console.Write("Alt + ");
                Console.Write(key.Key);

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static void ClearLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
