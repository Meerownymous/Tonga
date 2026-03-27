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
                new Comparable("Hallo Welt").CompareTo("Tschüss Welt") <= -1
            );
        }

        [Fact]
        public void SeesDifferences()
        {
            Assert.False(
                // ReSharper disable once SuspiciousTypeConversion.Global
                new Comparable("Timm").Equals("Jan-Peter")
            );
        }

        [Fact]
        public void MatchesString()
        {
            Assert.Equal(
                "Timm",
                new Comparable("Timm").Str()
            );
        }
    }
}
