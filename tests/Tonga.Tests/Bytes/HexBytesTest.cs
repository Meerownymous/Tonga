using Tonga.Bytes;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Bytes
{
    public sealed class HexBytesTests
    {
        [Fact]
        public void BytesFromHex()
        {
            Assert.Equal(
                "foobar",
                new TextMorph(
                    new TextMorph("666f6f626172").AsHexBytes()
                ).Str()
            );
        }
    }
}
