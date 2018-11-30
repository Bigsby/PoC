using System;
using System.Reflection;

namespace enuming
{
    class Program
    {
        static void Main(string[] args)
        {
            var anumType = typeof(Anum);
            System.Console.WriteLine($"{anumType.Name}: {anumType.IsEnum}, {anumType.IsClass}, {anumType.IsValueType}");
            foreach (var member in anumType.GetMembers())
            {
                if (member is FieldInfo fi)
                {
                    System.Console.WriteLine($"{member.Name}: {fi.IsLiteral}, {fi.IsStatic}, {fi.FieldType}");
                }
                else
                {
                    System.Console.WriteLine($"{member.Name}: {member.MemberType}");
                }
            }

        }
    }

    enum Anum
    {
        Value1,
        Value2
    }
}
