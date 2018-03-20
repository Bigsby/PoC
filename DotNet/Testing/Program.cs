using System;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    class StringOrInt
    {
        public static explicit operator EnumValues(StringOrInt me)
        {
            if (Enum.TryParse(typeof(EnumValues), me.StringValue, out object enumValue))
                return (EnumValues)enumValue;

            return EnumValues.Default;
        }

        public static implicit operator int(StringOrInt me)
        {
            return (int)(EnumValues)me;            
        }

        public string StringValue { get; set; }
    }

    enum EnumValues
    {
        Open,
        Closed,
        Default
    }
}
