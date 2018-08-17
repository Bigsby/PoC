using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using static System.Console;

namespace bazeries
{
    class Program
    {
        static Dictionary<char, char> keys = new Dictionary<char, char>(){
            {'Z', 'I'},
            {'R', 'F'},
            {'E', 'Y'},
            {'B', 'O'},
            {'Y', 'U'},
            {' ', 'C'}
            // {'G', 'M'},
            // {'U', 'O'},
            // {'N', 'C'},
            // {'A', 'A'},
            // {'X', 'N'},
            
        };
        static void Main(string[] args)
        {
            var cypheredText = File.ReadAllText(@"C:\Git\Bigsby\PoC\Other\bazeries\cyphered.txt");
            WriteLine(cypheredText);
            var binaries = new Regex("[0-1]+").Matches(cypheredText);
            foreach (Match binary in binaries)
            {
                var decimalValue = Convert.ToInt32(binary.Value, 2);
                var charValue = (char)decimalValue;
                keys.TryGetValue(charValue, out var value);
                WriteLine($"{binary.Value} - {decimalValue} - {charValue} - {value}");
            }
        }
    }
}
