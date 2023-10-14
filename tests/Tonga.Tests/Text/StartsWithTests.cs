

using Xunit;

namespace Tonga.Text.Test
{
    public sealed class StartsWithTests
    {
        [Fact]
        public void MatchesText()
        {
            var x =
                new StartsWith(
                    new LiveText("Im an text with a really good end!"),
                    new LiveText("Im a")
                );
            Assert.True(x.Value());
        }

        [Fact]
        public void MatchesString()
        {
            var x =
                new StartsWith(
                    new LiveText("Im a text with a really good end!"),
                    "Im a"
                );
            Assert.True(x.Value());
        }

        [Fact]
        public void DoesntMatch()
        {
            var x =
                new StartsWith(
                    new LiveText("Im a text with a really good end!"),
                    new LiveText("m an")
                );
            Assert.False(x.Value());
        }
    }
}
