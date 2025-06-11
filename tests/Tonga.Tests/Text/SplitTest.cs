using Tonga.Enumerable;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class SplitTest
    {
        [Fact]
        public void SplitText()
        {
            Assert.Equal(
                ["Hello", "world!"],
                "Hello world!"
                    .AsSplit("\\s+")
            );
        }

        [Fact]
        public void SplitEmptyText()
        {
            Assert.Equal(
                [],
                "".AsSplit("\n")
            );
        }

        [Fact]
        public void SplitStringWithTextRegex()
        {
            Assert.Equal(
                ["Tonga", "OOP!"],
                "Tonga OOP!".AsSplit("\\s")
            );
        }

        [Fact]
        public void SplitTextWithStringRegex()
        {
            Assert.Equal(
                ["Atoms", "Primitives!"],
                "Atoms4Primitives!".AsSplit("\\d+")
            );
        }

        [Fact]
        public void SplitTextWithTextRegex()
        {
            Assert.Equal(
                ["Split", "OOP"],
                "Split#OOP!".AsSplit("\\W+")
            );
        }

        [Fact]
        public void SplitTextRemoveEmptyStrings()
        {
            Assert.Equal(
                2,
                "Split##OOP!".AsSplit("\\W+").Length().Value()
            );
        }

        [Fact]
        public void SplitTextContainsEmptyStrings()
        {
            Assert.Equal(
                3,
                "Split##OOP!".AsSplit("\\W+",false).Length().Value()
            );
        }
    }
}
