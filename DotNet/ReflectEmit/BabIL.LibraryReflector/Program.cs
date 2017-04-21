using System;
using System.IO;
using System.Linq;
using System.Reflection;
using static System.Console;

namespace BabIL.LibraryReflector
{
    class Program
    {
        static int currentIndent = 0;
        static readonly BindingFlags allBidningFlags = (BindingFlags)Enum.GetValues(typeof(BindingFlags)).Cast<int>().Sum();

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                WriteLine("No library file provided!");
                return;
            }

            var libraryPath = args[0];
            if (!File.Exists(libraryPath))
            {
                WriteLine("Path provided cannot be found!");
                return;
            }

            try
            {
                var assembly = Assembly.ReflectionOnlyLoadFrom(libraryPath);

                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += (s, e) => {
                    try
                    {
                        var byName = Assembly.ReflectionOnlyLoad(e.Name);
                        if (null != byName)
                            return byName;
                    }
                    catch
                    { }

                    foreach (var file in Directory.GetFiles(Path.GetDirectoryName(libraryPath), "*.dll"))
                    {
                        var test = Assembly.ReflectionOnlyLoadFrom(file);
                        if (test?.FullName == e.Name)
                            return test;
                    }

                    return null;
                };
                DisplayValue("Name", assembly.FullName);

                Header("Referenced Assemblies", assembly.GetReferencedAssemblies().Length);
                currentIndent++;
                foreach (var referencedAssembly in assembly.GetReferencedAssemblies())
                {
                    WriteItem(referencedAssembly.FullName);

                }
                currentIndent--;

                DisplayValue("EntryPoint", null == assembly.EntryPoint ? "None" : $"{assembly.EntryPoint.DeclaringType?.FullName}.{assembly.EntryPoint.Name} : {GetMethodDescription(assembly.EntryPoint)}");

                Header("Modules", assembly.Modules.Count());
                foreach (var module in assembly.Modules)
                {
                    currentIndent++;
                    WriteItem(module.Name);
                    currentIndent++;

                    DisplayFields(module.GetFields(allBidningFlags));
                    DisplayMethods(module.GetMethods(allBidningFlags));

                    Header("Types", module.GetTypes().Count(), true);
                    currentIndent++;
                    foreach (var type in module.GetTypes())
                    {
                        WriteItem(type.FullName);
                        currentIndent++;

                        DisplayFields(type.GetFields(allBidningFlags));
                        DisplayMethods(type.GetMethods(allBidningFlags));

                        Header("Properties", type.GetProperties(allBidningFlags).Length, true);
                        currentIndent++;
                        foreach (var property in type.GetProperties(allBidningFlags))
                            WriteItem($"{property.Name} : {property.PropertyType.FullName}" + (property.CanRead ? ", get;" : "") + (property.CanWrite ? ", set;" : ""));
                        currentIndent--;

                        Header("Events", type.GetEvents(allBidningFlags).Length, true);
                        currentIndent++;
                        foreach (var @event in type.GetEvents(allBidningFlags))
                            WriteItem($"{@event.Name} : {@event.EventHandlerType.FullName}");
                        currentIndent--;

                        currentIndent--;
                    }
                    currentIndent--;
                }
            }
            catch (Exception ex)
            {
                WriteLine("Error reading library!");
                WriteLine(ex.Message);
            }
        }

        static void DisplayFields(FieldInfo[] fields)
        {
            Header("Fields", fields.Count(), true);
            currentIndent++;
            foreach (var field in fields)
                WriteItem($"{field.Name}: {field.FieldType.FullName}");
            currentIndent--;
        }

        static void DisplayMethods(MethodInfo[] methods)
        {
            Header("Methods", methods.Count(), true);
            currentIndent++;
            foreach (var method in methods)
                WriteItem($"{method.Name}: {GetMethodDescription(method)}");
            currentIndent--;
        }

        static string GetMethodDescription(MethodInfo method)
        {
            return $"({string.Join(", ", method.GetParameters().Select(p => p.Name + ": " + p.ParameterType.FullName).ToArray())}) -> {method.ReturnType.FullName}";
        }

        static void Header(string name, int count, bool noNewLine = false)
        {
            if (!noNewLine)
                WriteLine();
            WriteLine($"{new string(' ', currentIndent * 2)}{name} [{count}]:");
        }

        static void WriteItem(string value)
        {
            WriteLine($"{new string(' ', currentIndent * 2)}- {value}");
        }

        static void DisplayValue(string name, string value)
        {
            WriteLine($"{new string(' ', currentIndent * 2)}{name}: {value}");
        }
    }
}