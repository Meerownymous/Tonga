using Xunit;
using Tonga.Scalar;
using Tonga.Enumerable;

namespace Tonga.Text.Test
{
    public sealed class SplitTest
    {
        [Fact]
        public void SplitText()
        {
            Assert.Equal(
                2,
                new LengthOf(
                    Filtered._(
                        s => s == "Hello" || s == "world!",
                        new Split(
                            "Hello world!", "\\s+"
                        )
                    )
                ).Value()
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
            Assert.Equal(
                2,
                new LengthOf(
                    Filtered._(
                        s => s == "Atoms" || s == "OOP!",
                        new Split(
                            "Atoms OOP!",
                            AsText._("\\s")
                        )
                    )
                ).Value()
            );
        }

        [Fact]
        public void SplitTextWithStringRegex()
        {
            Assert.Equal(
                2,
                new LengthOf(
                    Filtered._(
                        s => s == "Atoms" || s == "Primitives!",
                        new Split(
                            AsText._("Atoms4Primitives!"), "\\d+")
                    )
                ).Value()
            );
        }

        [Fact]
        public void SplitTextWithTextRegex()
        {
            Assert.Equal(
                2,
                new LengthOf(
                    Filtered._(
                        s => s == "Split" || s == "OOP",
                        new Split(AsText._("Split#OOP!"), "\\W+")
                    )
                ).Value()
            );
        }

        [Fact]
        public void SplitTextRemoveEmptyStrings()
        {
            Assert.True(
                new LengthOf(
                    new Split(
                        AsText._("Split##OOP!"),
                        "\\W+")).Value() == 2,
                "Can't remove empty strings");
        }

        [Fact]
        public void SplitTextContainsEmptyStrings()
        {
            Assert.True(
                new LengthOf(
                    new Split(
                        AsText._("Split##OOP!"),
                        "\\W+",
                        false
                    )
                ).Value() == 3
            );
        }
    }
}
