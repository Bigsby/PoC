using System.Collections.Generic;

namespace ModelsLibrary
{
    public class Class : BaseClass
    {
        public Teacher Teacher { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}
