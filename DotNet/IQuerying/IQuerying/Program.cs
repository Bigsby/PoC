using IQuerying.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace IQuerying
{
    class Program
    {
        static void Main(string[] args)
        {
            var query = new QueryableTerraServerData<Place>();

            foreach (var place in query.Where(p => p.State == "NY"))
            {
                WriteLine($"- {place.Name} - {place.PlaceType}");
            }
            WriteLine("Done!");
            ReadLine();
        }
    }
}
