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
                Length._(
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
            Assert.Equal(
                0,
                Length._(
                    new Split("", "\n")).Value()
            );
        }

        [Fact]
        public void SplitStringWithTextRegex()
        {
            Assert.Equal(
                2,
                Length._(
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
                Length._(
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
                Length._(
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
            Assert.Equal(
                2,
                Length._(
                    new Split(
                        AsText._("Split##OOP!"),
                        "\\W+")
                    ).Value()
                );
        }

        [Fact]
        public void SplitTextContainsEmptyStrings()
        {
            Assert.Equal(
                3,
                Length._(
                    new Split(
                        AsText._("Split##OOP!"),
                        "\\W+",
                        false
                    )
                ).Value()
            );
        }
    }
}
