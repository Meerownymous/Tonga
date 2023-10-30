

using System.IO;
using Xunit;
using Tonga.Text;

namespace Tonga.IO.Tests
{
    public sealed class HeadInputStreamTest
    {
        [Fact]
        void TestSkippingLessThanTotal()
        {
            var stream =
                new HeadInputStream(
                    new InputOf("testSkippingLessThanTotal").Stream(),
                    5
                );

            var skipped = stream.Seek(3L, SeekOrigin.Begin);

            Assert.Equal(
                3L,
                skipped
            );

            Assert.Contains(
                "tS",
                AsText._(new InputOf(stream)).AsString()
            );
        }

        [Fact]
        void TestSkippingMoreThanTotal()
        {
            var stream =
                new HeadInputStream(
                    new InputOf("testSkippingMoreThanTotal").Stream(),
                    5
                );
            var skipped = stream.Seek(7L, SeekOrigin.Begin);

            Assert.Equal(
                5L,
                skipped
            );

            var input = AsText._(stream).AsString();
            Assert.Equal(
                "",
                input
            );
        }
    }
}
