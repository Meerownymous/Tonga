

using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class StartsWithTests
    {
        [Fact]
        public void MatchesText()
        {
            var x =
                new StartsWith(
                    AsText._("Im an text with a really good end!"),
                    AsText._("Im a")
                );
            Assert.True(x.Value());
        }

        [Fact]
        public void MatchesString()
        {
            var x =
                new StartsWith(
                    AsText._("Im a text with a really good end!"),
                    "Im a"
                );
            Assert.True(x.Value());
        }

        [Fact]
        public void DoesntMatch()
        {
            var x =
                new StartsWith(
                    AsText._("Im a text with a really good end!"),
                    AsText._("m an")
                );
            Assert.False(x.Value());
        }
    }
}
