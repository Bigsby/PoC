using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace mongoing
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "mongodb://localhost";
            var mongoUrl = new MongoUrl(connectionString);
            var settings = MongoClientSettings.FromUrl(mongoUrl);
            var mongoClient = new MongoClient(settings);

            BsonClassMap.RegisterClassMap<BaseClass>(config => {
                config.AddKnownType(typeof(Item));
                config.MapIdField(i => i._id);
                config.MapProperty(i => i.Id).SetElementName("id").SetIsRequired(true);
            });

            BsonClassMap.RegisterClassMap<Item>(config =>
                config.MapProperty(i => i.Name).SetElementName("name").SetIsRequired(true)
            );

            var db = mongoClient.GetDatabase("test");

            var col = db.GetCollection<Item>("collectionOne");
            
            foreach (var item in col.AsQueryable())
                Console.WriteLine($"{item.Id} - {item.Name}");

            Console.WriteLine("Done!");
        }
    }

    public abstract class BaseClass
    {
        internal ObjectId _id;
        public string Id { get; set; }

    }

    public class Item : BaseClass
    {
        public string Name { get; set; }
    }
}
