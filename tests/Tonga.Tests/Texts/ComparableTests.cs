


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
                    AsText._("Hallo Welt")
                ).CompareTo(
                    AsText._("Tschüss Welt")
                ) <= -1
            );
        }

        [Fact]
        public void SeesDifferences()
        {
            Assert.True(
                new Comparable(
                    AsText._("Timm")
                ).Equals(
                    AsText._("Jan-Peter")
                ) == false
            );
        }

        [Fact]
        public void MatchesString()
        {
            Assert.True(
                new Comparable(
                    AsText._("Timm")
                ).AsString()
                == "Timm"
            );
        }
    }
}
