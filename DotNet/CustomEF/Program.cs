using System;

namespace CustomEF
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...!");

            try
            {
                var context = new EFCoreContext();

                //context.Set<Item>().Add(new Item
                //{
                //    Name = "New Item"
                //});
                //context.SaveChanges();

                foreach (var item in context.Set<Item>())
                {
                    Console.WriteLine($"Item: {item.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error!\n{ex.Message}");
            }


            Console.WriteLine("Ended!");
            Console.ReadLine();
        }
    }
}
