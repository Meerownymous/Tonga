using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class LinesTest
    {
        [Fact]
        public void SplitsLines()
        {
            Assert.Equal(
                ["1", "2", "3"],
                new Lines("1\r\n2\r\n3")
            );
        }

        [Fact]
        public void SkipsEmpty()
        {
            Assert.Equal(
                ["1", "2", "4"],
                new Lines("1\r\n2\r\n\r\n4\r\n", skipEmpty: true)
            );
        }

        [Fact]
        public void IncludesEmpty()
        {
            Assert.Equal(
                ["1", "2", "", "4"],
                new Lines("1\r\n2\r\n\r\n4\r\n", skipEmpty: false)
            );
        }
    }
}

