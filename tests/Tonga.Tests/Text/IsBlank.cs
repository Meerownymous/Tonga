using Tonga.Fact;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class IsBlank
    {
        [Fact]
        public void ConvertsString()
        {
            AssertFact.True(
                new Tonga.Text.IsBlank(" ")
            );
        }

        [Fact]
        public void DoesntMatchEmpty()
        {
            AssertFact.True(
                new Tonga.Text.IsBlank("")
            );
        }

        [Fact]
        public void MatchesWhitespace()
        {
            AssertFact.True(
                new Tonga.Text.IsBlank("  ")
            );
        }

        [Fact]
        public void DoesntMatchNotWhitespace()
        {
            AssertFact.False(
                new Tonga.Text.IsBlank("not empty")
            );
        }
    }
}
