using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Serializing
{
    class Program
    {
        static void Main(string[] args)
        {



            var item = new Item
            {
                Name = "This is the item",
                Value = Value.Two
            };

            var json1 = JsonConvert.SerializeObject(item);

            var deserializded1 = Deserialize<Item>(json1);

            JsonConvert.DefaultSettings = (() =>
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new StringEnumConverter());
                return settings;
            });

            var json2 = JsonConvert.SerializeObject(item);

            var deserializded2 = Deserialize<Item>(json2);

            var stopt = "here";
        }

        static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public Value Value { get; set; }
    }

    public enum Value
    {
        One, Two
    }
}
