

using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class EndsWithTests
    {
        [Fact]
        public void MatchesText()
        {
            Assert.True(
                "Im a text with a really good end!".AsText()
                    .EndsWith("od end!".AsText())
                    .IsTrue()
            );
        }

        [Fact]
        public void MatchesString()
        {
            Assert.True(
                "Im a text with a really good end!".AsText()
                    .EndsWith("od end!")
                    .IsTrue()
            );
        }

        [Fact]
        public void DoesntMatch()
        {
            Assert.False(
                "Im a text with a really good end!"
                    .AsText()
                    .EndsWith("od end".AsText())
                    .IsTrue()
            );
        }
    }
}
