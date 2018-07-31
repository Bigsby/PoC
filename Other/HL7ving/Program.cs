using System;
using NHapi.Base.Parser;

namespace HL7ving
{
    class Program
    {
        const string _theMessage = @"MSH|^~\&|EPIC|TISCH|||20171217230856|RAMOSJ10|ADT^A01|13ad6867-7b3e-45a2-ab8c-1ce9e7d3c616|T|2.3|||||||||||EVN|A01|20171109111017||ADT_EVENT|BERNARDOJ10^BERNARDO^BRUNO^^^^^^NYUSA^^^^^Tisch|20171109111014|PID|1|1099991107^^^NYU MRN^MRN|||Mater^Regression||19900101|M||W||60|||ENG|S|NONE||999999999|||N||||||||N||PV1|1|I|BFB^HL7|EL|||liug07^Liu^Grace|||PY||||Phys Ref|||||MAT0006|BC||||||||||||||||||||||||20171217230856||||||13653541102||||";

        static void Main(string[] args)
        {

            var parser = new PipeParser();
            var message = parser.Parse(_theMessage);
            Console.WriteLine("Hello World!");
        }
    }
}
