using System.IO;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class HeadInputStreamTest
    {
        [Fact]
        void TestSkippingLessThanTotal()
        {
            var stream =
                new HeadInputStream(
                    new AsInput("testSkippingLessThanTotal").Stream(),
                    5
                );

            var skipped = stream.Seek(3L, SeekOrigin.Begin);

            Assert.Equal(
                3L,
                skipped
            );

            Assert.Contains(
                "tS",
                AsText._(new AsInput(stream)).AsString()
            );
        }

        [Fact]
        void TestSkippingMoreThanTotal()
        {
            var stream =
                new HeadInputStream(
                    new AsInput("testSkippingMoreThanTotal").Stream(),
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
