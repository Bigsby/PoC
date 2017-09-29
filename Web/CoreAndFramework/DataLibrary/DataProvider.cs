using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLibrary
{
    public static class DataProvider
    {
        public static IEnumerable<Item> GetItems()
        {
            return new DataContext().Set<Item>().ToList();
        }

        public static async Task AddItem(Item item)
        {
            using (var c = new DataContext())
            {
                c.Add(item);
                await c.SaveChangesAsync();
            }
        }
    }
}
