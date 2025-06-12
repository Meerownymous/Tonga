using Tonga.Bytes;
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
                "666f6f626172"
                    .AsText()
                    .AsHexBytes()
                    .AsText()
                    .Str()
            );
        }
    }
}
