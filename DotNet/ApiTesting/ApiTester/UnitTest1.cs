using Xunit;
using FluentAssertions;

namespace ApiTester
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(1, 2)]
        public void Test1(int first, int second)
        {   
            first.Should().Be(second);
        }

        [Fact]
        public void Test2()
        {
            var expected = 2;
            var actual = 2;
            
            expected.Should().Be(actual);
        }        
    }
}
