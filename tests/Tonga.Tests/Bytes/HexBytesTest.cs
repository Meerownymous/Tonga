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
                "666f6f626172",
                "foobar"
                    .AsText()
                    .AsHexBytes()
                    .AsText()
                    .Str()
            );
        }
    }
}
