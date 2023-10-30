

using Xunit;
using Tonga.Text;
using Tonga.Scalar;

namespace Tonga.IO.Tests
{
    public sealed class HeadOfTest
    {
        [Fact]
        void ReadsHeadOfLongerInput()
        {
            Assert.Contains(
                "reads",
                AsText._(
                    new HeadOf(
                        new InputOf("readsHeadOfLongInput"),
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
                    new HeadOf(
                        new InputOf("readsHeadOfLongInput"),
                        5
                    )
                ).AsString();

            Assert.Equal(
                5,
                Length._(
                    new InputOf(
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
                    new HeadOf(
                        new InputOf("readsEmptyHeadOfInput"),
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
                    new HeadOf(
                        new InputOf(input),
                        35
                    )
                ).AsString()
            );
        }
    }
}
