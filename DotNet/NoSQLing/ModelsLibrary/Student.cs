using System.Collections.Generic;

namespace ModelsLibrary
{
    public class Student : BaseClass
    {
        public IEnumerable<Class> Classes { get; set; }
    }
}
