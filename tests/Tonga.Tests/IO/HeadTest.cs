using Tonga.IO;
using Tonga.Scalar;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
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
                        new AsConduit("readsHeadOfLongInput"),
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
                        new Tonga.IO.AsConduit("readsHeadOfLongInput"),
                        5
                    )
                ).AsString();

            Assert.Equal(
                5,
                Length._(
                    new Tonga.IO.AsConduit(
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
                        new AsConduit("readsEmptyHeadOfInput"),
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
                        new Tonga.IO.AsConduit(input),
                        35
                    )
                ).AsString()
            );
        }
    }
}
