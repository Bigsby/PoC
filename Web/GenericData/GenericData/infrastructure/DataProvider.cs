using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;

namespace GenericData.infrastructure
{
    public static class DataProvider
    {
        public static IEnumerable<JToken> GetItems(string set)
        {
            //return new JArray().AsQueryable();
            return Directory.GetFiles(GetFolderPath(set)).Select(file => JToken.Parse(File.ReadAllText(file)));
        }

        private static string GetFolderPath(string set)
        {
            return HostingEnvironment.MapPath("~/App_Data/" + set);
        }
    }
}