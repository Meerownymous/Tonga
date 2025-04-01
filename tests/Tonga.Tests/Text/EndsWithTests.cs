

using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class EndsWithTests
    {
        [Fact]
        public void MatchesText()
        {
            var x =
                new EndsWith(
                    AsText._("Im a text with a really good end!"),
                    AsText._("od end!")
                );
            Assert.True(x.Value());
        }

        [Fact]
        public void MatchesString()
        {
            var x =
                new EndsWith(
                    AsText._("Im a text with a really good end!"),
                    "od end!"
                );
            Assert.True(x.Value());
        }

        [Fact]
        public void DoesntMatch()
        {
            var x =
                new EndsWith(
                    AsText._("Im a text with a really good end!"),
                    AsText._("od end")
                );
            Assert.False(x.Value());
        }
    }
}
