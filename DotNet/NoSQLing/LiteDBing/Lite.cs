using LiteDB;
using ModelsLibrary;
using static System.Console;

namespace LiteDBing
{
    class Lite
    {
        static void Main(string[] args)
        {
            var db = new LiteDatabase(@"C:\Git\Bigsby\PoC\DotNet\NoSQLing\LiteDBing\lite.db");
            
            var students = db.GetCollection<Student>("students");
            
            students.Insert(new Student
            {
                Id = "2",
                Name = "Eric Idle"
            });

          

            foreach (var student in students.FindAll())
                WriteLine($"{student.Id}. {student.Name}");



            WriteLine("Done!");
            ReadLine();
        }
    }
}
