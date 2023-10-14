

using System.Linq;
using Xunit;
using Tonga.Enumerable;

namespace Tonga.Text.Test
{
    public sealed class SplitTest
    {
        [Fact]
        public void SplitText()
        {
            Assert.True(
        new Split(
            "Hello world!", "\\s+"
        ).Select(s => s == "Hello" || s == "world!").Count() == 2
            );
        }

        [Fact]
        public void SplitEmptyText()
        {
            Assert.True(
                new LengthOf(
                    new Split("", "\n")).Value() == 0,
                    "Can't split an empty text");
        }

        [Fact]
        public void SplitStringWithTextRegex()
        {
            Assert.True(
                new Split(
                    "Atoms OOP!",
                    new LiveText("\\s")
                ).Select(s => s == "Atoms" || s == "OOP!").Count() == 2,
                "Can't split an string with text regex");
        }

        [Fact]
        public void SplitTextWithStringRegex()
        {
            Assert.True(
            new Split(
                new LiveText("Atoms4Primitives!"), "\\d+")
                .Select(s => s == "Atoms" || s == "Primitives!").Count() == 2,
            "Can't split an text with string regex");
        }

        [Fact]
        public void SplitTextWithTextRegex()
        {
            Assert.True(
                new Split(new LiveText("Split#OOP!"), "\\W+")
                .Select(s => s == "Split" || s == "OOP").Count() == 2,
                "Can't split an text with text regex");
        }

        [Fact]
        public void SplitTextRemoveEmptyStrings()
        {
            Assert.True(
                new LengthOf(
                    new Split(
                        new LiveText("Split##OOP!"),
                        "\\W+")).Value() == 2,
                "Can't remove empty strings");
        }

        [Fact]
        public void SplitTextContainsEmptyStrings()
        {
            Assert.True(
                new LengthOf(
                    new Split(
                        new LiveText("Split##OOP!"),
                        "\\W+",
                        false
                    )
                ).Value() == 3
            );
        }
    }
}
