

using Xunit;
using Tonga.Text;

namespace Tonga.Bytes.Tests
{
    public sealed class HexBytesTests
    {
        [Theory]
        [InlineData("foobar", "666f6f626172")]
        public void BytesFromHex(string expected, string hex)
        {
            Assert.Equal(
                    expected,
                    new LiveText(new HexBytes(new LiveText(hex))).AsString()
                );
        }
    }
}
