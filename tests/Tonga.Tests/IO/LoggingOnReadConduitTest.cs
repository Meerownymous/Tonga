

using Tonga.IO;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class LoggingOnReadConduitTest
    {
        [Fact]
        void ReadEmptyStream()
        {
            var input =
                new LoggingOnReadConduit(
                    new Tonga.IO.AsConduit(""),
                    ""
                );
            Assert.Equal(
                0,
                input.Stream().ReadByte()
            );
        }

        [Fact]
        void ReadByteByByte()
        {
            var input =
                new LoggingOnReadConduit(
                    new Tonga.IO.AsConduit(
                        new byte[] {
                            // @checkstyle MagicNumberCheck (2 lines)
                            (byte) 20,
                            (byte) 10,
                        }
                    ),
                    "ReadByteByByte"
                );

            var stream = input.Stream();
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
    }
}
