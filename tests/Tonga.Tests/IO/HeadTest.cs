

using Xunit;
using Tonga.Text;
using Tonga.Scalar;

namespace Tonga.IO.Tests
{
    public sealed class HeadTest
    {
        [Fact]
        void ReadsHeadOfLongerInput()
        {
            Assert.Contains(
                "reads",
                AsText._(
                    new Head(
                        new AsInput("readsHeadOfLongInput"),
                        5
                    )
                ).AsString()
            );
        }

        [Fact]
        void ReadsOnlyLength()
        {
            var res =
                AsText._(
                    new Head(
                        new AsInput("readsHeadOfLongInput"),
                        5
                    )
                ).AsString();

            Assert.Equal(
                5,
                Length._(
                    new AsInput(
                        res
                    )
                ).Value()
            );
        }

        [Fact]
        void ReadsEmptyHeadOfInput()
        {
            Assert.Contains(
                "",
                AsText._(
                    new Head(
                        new AsInput("readsEmptyHeadOfInput"),
                        0
                    )
                ).AsString()
            );
        }

        [Fact]
        void ReadsHeadOfShorterInput()
        {
            var input = "readsHeadOfShorterInput";
            Assert.Contains(
                input,
                AsText._(
                    new Head(
                        new AsInput(input),
                        35
                    )
                ).AsString()
            );
        }
    }
}
