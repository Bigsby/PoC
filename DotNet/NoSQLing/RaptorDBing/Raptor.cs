using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace RaptorDBing
{
    class Raptor
    {
        static void Main(string[] args)
        {
            var db = RaptorDB.RaptorDB.Open("data");

            var student = new Student
            {
                Id = Guid.NewGuid().ToString(),
                Name = "John Cleese"
            };
            
            db.Save(Guid.Parse(student.Id), student);

            foreach (var item in 
                db.Query<Student>("").Rows)
                WriteLine("Student: " + item.Name);

            db.Query<Student>("");

        }
    }

    //public class ComparableStudent : Student, IComparable<Student>
    //{
    //    public int CompareTo(Student other)
    //    {
    //        return Id.CompareTo(other.Id);
    //    }
    //}
}
