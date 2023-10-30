

using Xunit;

namespace Tonga.IO.Tests
{
    public sealed class LoggingInputTest
    {
        [Fact]
        void ReadEmptyStream()
        {
            var input =
                new LoggingInput(
                    new AsInput(""),
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
                new LoggingInput(
                    new AsInput(
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
