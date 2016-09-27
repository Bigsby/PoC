using ModelsLibrary;
using Raven.Client.Embedded;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace RavenDBing
{
    class Raven
    {
        static void Main(string[] args)
        {
            var store = new EmbeddableDocumentStore
            {
                DataDirectory = @"C:\Git\Bigsby\PoC\DotNet\NoSQLing\RavenDBing\Data",
                DefaultDatabase = "db1"
            };

            store.Initialize();

            var session = store.OpenSession();

            session.Store(new Teacher
            {
                Name = "Eric Idle"
            });

            session.SaveChanges();

            var students = session.Query<Teacher>();

            foreach (var student in students)
                WriteLine($"{student.Id}. {student.Name}");

            WriteLine("Done!");
            ReadLine();
        }
    }
}
