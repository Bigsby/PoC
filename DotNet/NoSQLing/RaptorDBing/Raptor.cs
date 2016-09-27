using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaptorDBing
{
    class Raptor
    {
        static void Main(string[] args)
        {
            var db = new RaptorDB.RaptorDB<ComparableStudent>(@"C:\Git\Bigsby\PoC\DotNet\NoSQLing\RaptorDBing\data", false);
            
        }
    }

    public class ComparableStudent : Student, IComparable<Student>
    {
        public int CompareTo(Student other)
        {
            return Id.CompareTo(other.Id);
        }
    }
}
