using System.Configuration;
using static System.Console;

namespace LocalSettings
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string key in ConfigurationManager.AppSettings.Keys)
            {
                WriteLine($"Configuration for \"{key}\" is \"{ConfigurationManager.AppSettings[key]}\"");
            }
            
            ReadLine();
        }
    }
}