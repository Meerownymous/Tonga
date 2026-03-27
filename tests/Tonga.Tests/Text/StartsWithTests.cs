

using Tonga.Fact;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class StartsWithTests
    {
        [Fact]
        public void MatchesText()
        {
            AssertFact.True(
                new StartsWith("Im an text with a really good end!","Im a")
            );
        }

        [Fact]
        public void MatchesString()
        {
            AssertFact.True(
                new StartsWith("Im a text with a really good end!","Im a")
            );
        }

        [Fact]
        public void DoesntMatch()
        {
            AssertFact.False(
                new StartsWith("Im a text with a really good end!","m an")
            );
        }
    }
}
