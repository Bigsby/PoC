using HtmlAgilityPack;
using System.Net.Http;
using System.Web;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using static System.Console;

namespace HTMLCrawling
{
    class Program
    {
        private const string _baseUrl = "https://channel9.msdn.com/";
        private const string _pagesUrl = _baseUrl + "Events/Connect/2016?sort=status&direction=desc&page=";
        private const int pageCount = 11;
        private const string _resultOutput = @"C:\Git\Bigsby\PoC\DotNet\HTMLCrawling\HTMLCrawling\Connect2016Sessions.json";

        static void Main(string[] args)
        {
            var client = new HttpClient();

            var result = new List<Session>();
            for (int page = 1; page < pageCount + 1; page++)
            {
                WriteLine($"Processing page {page}");
                var response = client.GetStringAsync(_pagesUrl + page).Result;
                var html = new HtmlDocument();
                html.LoadHtml(response);
                var navigator = html.CreateNavigator();

                foreach (HtmlNodeNavigator article in navigator.Select("/html/body/main/section/div/article"))
                    result.Add(new Session
                    {
                        Title = HttpUtility.HtmlDecode(article.SelectSingleNode("header/h4/a").Value),
                        Url = _baseUrl + article.SelectSingleNode("a").GetAttribute("href", null),
                        Duration = TimeSpan.Parse("00:" + article.SelectSingleNode("a/time").Value)
                    });
            }

            WriteLine("Serializing");
            var json = JsonConvert.SerializeObject(result);
            WriteLine("Saving to file");
            File.WriteAllText(_resultOutput, json);
            WriteLine("Done!!!");
            ReadLine();
        }
    }

    public class Session
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public TimeSpan Duration { get; set; }
        public int Relevance { get; set; }
    }
}
