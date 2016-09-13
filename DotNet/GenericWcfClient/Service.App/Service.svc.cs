using System;
using Service.Contract;

namespace Service.App
{
    public class Service1 : IServiceContract
    {
        public int GetInt()
        {
            return 7;
        }

        public Item GetItem(string name, int value)
        {
            return new Item
            {
                Name = name,
                Value = value
            };
        }
    }
}
