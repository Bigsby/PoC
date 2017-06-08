using Biggy.Core;
using Biggy.Data.Json;
using System.Linq;
using static System.Console;

namespace BiggyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var store = new JsonStore<Item>("dbDirectory", "dbName", "itemTable");
            
            var items = new BiggyList<Item>(store);
            //items.Add(new Item { Id = 1, Name = "item one" });
            //items.Add(new Item { Id = 2, Name = "item two" });

            //items.Contains()

            foreach (var item in items)
            {
                WriteLine($"{item.Id} - {item.Name}");
            }

            var contains = items.Contains(new Item { Id = 1, Name = "item one" });
            WriteLine("Contains: " + contains);

            var firstOrDefault = items.FirstOrDefault(i => i.Id == 1);
            WriteLine("FirstOrDefault: " + firstOrDefault.Name);

            ReadLine();
        }
    }

    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
