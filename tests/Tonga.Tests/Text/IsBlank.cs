using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class IsBlank
    {
        [Fact]
        public void ConvertsString()
        {
            Assert.True(
                new Tonga.Text.IsBlank(" ").IsTrue()
            );
        }

        [Fact]
        public void DoesntMatchEmpty()
        {
            Assert.True(
                new Tonga.Text.IsBlank(
                    "".AsText()
                ).IsTrue()
            );
        }

        [Fact]
        public void MatchesWhitespace()
        {
            Assert.True(
                new Tonga.Text.IsBlank("  ").IsTrue()
            );
        }

        [Fact]
        public void DoesntMatchNotWhitespace()
        {
            Assert.False(
                new Tonga.Text.IsBlank("not empty").IsTrue()
            );
        }
    }
}
