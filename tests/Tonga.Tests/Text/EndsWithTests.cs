

using Xunit;

namespace Tonga.Text.Test
{
    public sealed class EndsWithTests
    {
        [Fact]
        public void MatchesText()
        {
            var x =
                new EndsWith(
                    new LiveText("Im a text with a really good end!"),
                    new LiveText("od end!")
                );
            Assert.True(x.Value());
        }

        [Fact]
        public void MatchesString()
        {
            var x =
                new EndsWith(
                    new LiveText("Im a text with a really good end!"),
                    "od end!"
                );
            Assert.True(x.Value());
        }

        [Fact]
        public void DoesntMatch()
        {
            var x =
                new EndsWith(
                    new LiveText("Im a text with a really good end!"),
                    new LiveText("od end")
                );
            Assert.False(x.Value());
        }
    }
}
