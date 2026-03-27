

using Tonga.IO;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class LoggingOnReadConduitTest
    {
        [Fact]
        void ReadsEmptyStream()
        {
            var input = new LoggingOnReadConduit("","");
            Assert.Equal(
                0,
                input.Stream().ReadByte()
            );
        }

        [Fact]
        void ReadsByteByByte()
        {
            var input =
                new LoggingOnReadConduit(
                    new byte[]
                        {
                            20,
                            10
                        },
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
