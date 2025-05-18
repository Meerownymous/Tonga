using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class IsWhitespaceTest
    {
        [Fact]
        public void ConvertsString()
        {
            Assert.True(
                new IsBlank(
                    " "
                ).Value(),
                "Can't convert string"
            );
        }

        [Fact]
        public void DoesntMatchEmpty()
        {
            Assert.True(
                new IsBlank(
                    AsText._("")
                ).Value(),
                "Can't determine an empty text");
        }

        [Fact]
        public void MatchesWhitespace()
        {
            Assert.True(
                new IsBlank(
                    AsText._("  ")
                ).Value(),
                "Can't determine an empty text with spaces");
        }

        [Fact]
        public void DoesntMatchNotWhitespace()
        {
            Assert.False(
                new IsBlank(
                    AsText._("not empty")
                ).Value(),
                "Can't detect a nonempty text");
        }
    }
}
