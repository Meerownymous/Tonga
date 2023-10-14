


using Xunit;

namespace Tonga.Text.Test
{
    public sealed class ComparableTests
    {
        [Fact]
        public void Compares()
        {
            Assert.True(
                new Comparable(
                    new LiveText("Hallo Welt")
                ).CompareTo(
                    new LiveText("Tschüss Welt")
                ) <= -1
            );
        }

        [Fact]
        public void SeesDifferences()
        {
            Assert.True(
                new Comparable(
                    new LiveText("Timm")
                ).Equals(
                    new LiveText("Jan-Peter")
                ) == false
            );
        }

        [Fact]
        public void MatchesString()
        {
            Assert.True(
                new Comparable(
                    new LiveText("Timm")
                ).AsString()
                == "Timm"
            );
        }
    }
}
