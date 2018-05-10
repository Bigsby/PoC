using System;
using System.Xml;
using static System.Console;

namespace Xmling
{
    class Program
    {
        static void Main(string[] args)
        {
            var doc = new XmlDocument();
            doc.Load("file.xml");
            var elements = doc.GetElementsByTagName("element");
            foreach (XmlNode node in elements)
            {
                WriteLine(node.InnerText);
            }

            var attrs = doc.GetElementsByTagName("attr");
            foreach (XmlNode attr in attrs)
            {
                WriteLine(attr.Attributes["value"].Value);
            }
            ReadLine();
            WriteLine("Done!");
        }
    }
}
