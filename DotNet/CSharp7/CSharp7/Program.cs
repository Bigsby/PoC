using System;
using static System.Console;

namespace CSharp7
{
    class Program
    {
        //https://msdn.microsoft.com/en-us/magazine/mt790184.aspx
        static void Main(string[] args)
        {
            //Tuples
            // requires System.ValueTuple NuGet package
            var triplet = ("John", "Cleese", new DateTime(1939, 10, 27));
            WriteLine($"Triplet Item1: {triplet.Item1}, Item2: {triplet.Item2}, Item3: {triplet.Item3}");
            WriteLine(triplet);

            var namedTriplet = (firstName: "John", lastName: "Cleese", birthday: new DateTime(1939, 10, 27));
            WriteLine($"Named Triplet firstName: {namedTriplet.firstName}, lastName: {namedTriplet.lastName}, birthday: {namedTriplet.birthday}");
            WriteLine(namedTriplet);

            var (fn, ln, bd) = namedTriplet;
            WriteLine($"Deconstructed Triplet fn:{fn}, ln:{ln}, bd:{bd}");

            WriteLine();
            // Deconstructors
            // requires System.ValueTuple NuGet package
            var person = new Person("John", "Cleese");
            // Calls person.Deconstruct method
            var (firstName, lastName) = person;
            WriteLine($"Person is {firstName} {lastName}.");

            WriteLine();
            //Pattern Matching
            object v = 3;
            switch (v)
            {
                case null:
                    WriteLine("value is null");
                    break;
                case int i when i < 2:
                    WriteLine($"value is an integer less than 2: {i}");
                    break;
                case int i when i >= 2:
                    WriteLine($"value is an integer greater than or equal to 2: {i}");
                    break;
                case string s:
                    WriteLine("value is a string: " + s);
                    break;
                default:
                    break;
            }

            WriteLine();
            //Local functions
            person.Birthday = new DateTime(1939, 10, 27);
            WriteLine($"{person.FirstName} is {PersonAge()}");
            int PersonAge()
            {
                var today = DateTime.Today;
                var age = today.Year - person.Birthday.Year;
                if (person.Birthday > today.AddYears(-age)) age--;
                return age;
            };

            WriteLine();
            //Reference Returns
            var numbers = new[] { 1, 2, 3 };
            foreach (var n in numbers)
                Write(n + ",");
            WriteLine();
            ref int GetSecondNumber()
            {
                return ref numbers[1];
            }
            ref var second = ref GetSecondNumber();
            second = 20;
            foreach (var n in numbers)
                Write(n + ",");
            WriteLine();

            WriteLine();
            //Out variables
            if (int.TryParse("2234", out int parsedInt))
                WriteLine("integer parsed: " + parsedInt);
            else
                WriteLine("integer not parsed! " + parsedInt);

            if (int.TryParse("asdf", out int notParsedInt))
                WriteLine("integer parsed: " + notParsedInt);
            else
                WriteLine("integer not parsed! " + notParsedInt);

            WriteLine();
            //Exception throwing within statements
            int StringLenght(string value) => value.Length;
            try
            {
                string v = null;
                var len = StringLenght(v ?? throw new Exception("null string"));
            }
            catch (Exception ex)
            {
                WriteLine("Error: " + ex.Message);
            }

            ReadLine();
        }
    }

    public class Person
    {
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public void Deconstruct(out string firstName, out string lastName)
        {
            firstName = FirstName;
            lastName = LastName;
        }
    }

    //Body-expressions in constructors destructors and accessors
    public class BodyClass : IDisposable
    {
        private string _value;
        public BodyClass(string value) => Value = value;
        ~BodyClass() => Dispose();

        public string Value
        {
            get => _value;
            private set => _value = value;
        }

        public void Dispose() => WriteLine("Disposing...");
    }
}
