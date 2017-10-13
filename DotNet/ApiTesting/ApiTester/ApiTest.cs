using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using System.Net;

namespace ApiTester
{
    public class ApiTest
    {
        [Theory(Skip = "Out of scope")]
        [InlineData("TestOne")]
        public async Task ApiTest1(string testFile)
        {
            var basePath = @"C:\Git\Bigsby\PoC\DotNet\ApiTesting\ApiTester\TestData";
            var filePath = Path.Combine(basePath, testFile + ".json");
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Test file not found! {filePath}");

            var testData = JsonConvert.DeserializeObject<TestData>(File.ReadAllText(filePath));

            if (testData.Tests?.Length < 1)
                return;

            var client = new HttpClient();

            foreach (var test in testData.Tests)
            {
                var request = new HttpRequestMessage(new HttpMethod(test.Method ?? "GET"), test.Url);

                if (null != test.Payload)
                    request.Content = new StringContent(JsonConvert.SerializeObject(test.Payload), Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request);

                response.StatusCode.Should().Be(test.Code);

                var content = await response.Content.ReadAsStringAsync();
                var contentValue = JToken.Parse(content);

                foreach (var assertion in test.Assertions)
                {
                    var tokens = contentValue.SelectTokens(assertion.JPath);
                    tokens.Count().Should().Be(assertion.Length);
                }
            }
        }
    }

    class TestData
    {
        public TestRequest[] Tests { get; set; }
    }

    class TestRequest
    {
        public string Url { get; set; }
        public string Method { get; set; }
        public JToken Payload { get; set; }
        public HttpStatusCode Code { get; set; }
        public Assertion[] Assertions { get; set; }
    }

    class Assertion
    {
        public string JPath { get; set; }
        public int Length { get; set; }
    }
}
