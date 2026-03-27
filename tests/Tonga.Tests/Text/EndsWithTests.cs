

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
                new EndsWith("Im a text with a really good end!", "od end!").IsTrue()
            );
        }

        [Fact]
        public void MatchesString()
        {
            Assert.True(
                new EndsWith("Im a text with a really good end!", "od end!")
                    .IsTrue()
            );
        }

        [Fact]
        public void DoesntMatch()
        {
            Assert.False(
                new EndsWith("Im a text with a really good end!", "od end")
                    .IsTrue()
            );
        }
    }
}
