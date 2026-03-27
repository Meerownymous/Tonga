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
                new Split("Hello world!", "\\s+")
            );
        }

        [Fact]
        public void SplitEmptyText()
        {
            Assert.Equal(
                [],
                "".SplitBy("\n")
            );
        }

        [Fact]
        public void SplitStringWithTextRegex()
        {
            Assert.Equal(
                ["Tonga", "OOP!"],
                "Tonga OOP!".SplitBy("\\s")
            );
        }

        [Fact]
        public void SplitTextWithStringRegex()
        {
            Assert.Equal(
                ["Atoms", "Primitives!"],
                "Atoms4Primitives!".SplitBy("\\d+")
            );
        }

        [Fact]
        public void SplitTextWithTextRegex()
        {
            Assert.Equal(
                ["Split", "OOP"],
                "Split#OOP!".SplitBy("\\W+")
            );
        }

        [Fact]
        public void SplitTextRemoveEmptyStrings()
        {
            Assert.Equal(
                2,
                "Split##OOP!".SplitBy("\\W+").Length().Value()
            );
        }

        [Fact]
        public void SplitTextContainsEmptyStrings()
        {
            Assert.Equal(
                3,
                "Split##OOP!".SplitBy("\\W+",false).Length().Value()
            );
        }
    }
}
