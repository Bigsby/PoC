using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace ApiTester
{
    public class TypeTester
    {
        [Fact]
        public void TestSealed()
        {
            var mock = new Mock<Item>();

            var name = "name";
            mock.SetupProperty(i => i.Name, name);

            mock.Object.Name.Should().Be(name);

            
        }
    }

    class Item
    {
        public string Name { get; set; }
    }
}
