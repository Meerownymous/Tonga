

using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class StartsWithTests
    {
        [Fact]
        public void MatchesText()
        {
            Assert.True(
                "Im an text with a really good end!".AsText()
                    .AsStartsWith("Im a")
                    .IsTrue()
            );
        }

        [Fact]
        public void MatchesString()
        {
            Assert.True(
                "Im a text with a really good end!".AsText()
                    .AsStartsWith("Im a")
                    .IsTrue()
            );
        }

        [Fact]
        public void DoesntMatch()
        {
            Assert.False("Im a text with a really good end!".AsText()
                .AsStartsWith("m an")
                .IsTrue());
        }
    }
}
