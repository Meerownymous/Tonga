

using Xunit;

namespace Tonga.Text.Test
{
    public sealed class IsWhitespaceTest
    {
        [Fact]
        public void ConvertsString()
        {
            Assert.True(
                new IsWhitespace(
                    " "
                ).Value(),
                "Can't convert string"
            );
        }

        [Fact]
        public void DoesntMatchEmpty()
        {
            Assert.True(
                new IsWhitespace(
                    new LiveText("")
                ).Value(),
                "Can't determine an empty text");
        }

        [Fact]
        public void MatchesWhitespace()
        {
            Assert.True(
                new IsWhitespace(
                    new LiveText("  ")
                ).Value(),
                "Can't determine an empty text with spaces");
        }

        [Fact]
        public void DoesntMatchNotWhitespace()
        {
            Assert.False(
                new IsWhitespace(
                    new LiveText("not empty")
                ).Value(),
                "Can't detect a nonempty text");
        }
    }
}
