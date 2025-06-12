using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class ComparableTests
    {
        [Fact]
        public void Compares()
        {
            Assert.True(
                new Comparable(
                    "Hallo Welt".AsText()
                ).CompareTo(
                    "Tschüss Welt".AsText()
                ) <= -1
            );
        }

        [Fact]
        public void SeesDifferences()
        {
            Assert.False(
                // ReSharper disable once SuspiciousTypeConversion.Global
                new Comparable(
                    "Timm".AsText()
                ).Equals(
                    "Jan-Peter".AsText()
                )
            );
        }

        [Fact]
        public void MatchesString()
        {
            Assert.Equal(
                "Timm",
                new Comparable(
                    "Timm".AsText()
                ).Str()
            );
        }
    }
}
