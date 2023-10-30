

using System.IO;
using Xunit;
using Tonga.Bytes;

namespace Tonga.IO.Tests
{
    public sealed class LoggingInputStreamTest
    {
        [Fact]
        void ReadEmptyStream()
        {
            var stream =
                new LoggingInputStream(
                    new MemoryStream(
                        new AsBytes("").Bytes()
                    ),
                    ""
                );
            Assert.Equal(
                0,
                stream.ReadByte()
            );
        }

        [Fact]
        void ReadByteByByte()
        {
            var stream = new LoggingInputStream(
                new MemoryStream(
                    new byte[] {
                            // @checkstyle MagicNumberCheck (2 lines)
                            (byte) 20,
                            (byte) 10,
                    }
                ),
                "ReadByteByByte"
            );

            Assert.Equal(
                20,
                stream.ReadByte()
            );

            Assert.Equal(
                10,
                stream.ReadByte()
            );
            Assert.Equal(
                0,
                stream.ReadByte()
            );
        }

        [Fact]
        void SkipFirstByte()
        {
            var stream = new LoggingInputStream(
                new MemoryStream(
                    new byte[] {
                            // @checkstyle MagicNumberCheck (2 lines)
                            (byte) 20,
                            (byte) 10,
                    }
                ),
                "ReadByteByByte"
            );

            stream.Seek(1, SeekOrigin.Begin);

            Assert.Equal(
                10,
                stream.ReadByte()
            );
            Assert.Equal(
                0,
                stream.ReadByte()
            );
        }
    }
}
