using System.IO;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class HeadInputStreamTest
    {
        [Fact]
        void IsSkippingLessThanTotal()
        {
            var stream =
                new HeadInputStream(
                    new Tonga.IO.AsConduit("testSkippingLessThanTotal").Stream(),
                    5
                );

            var skipped = stream.Seek(3L, SeekOrigin.Begin);

            Assert.Equal(
                3L,
                skipped
            );

            Assert.Contains(
                "tS",
                AsText._(new Tonga.IO.AsConduit(stream)).AsString()
            );
        }

        [Fact]
        void IsSkippingMoreThanTotal()
        {
            var stream =
                new HeadInputStream(
                    new Tonga.IO.AsConduit("testSkippingMoreThanTotal").Stream(),
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
