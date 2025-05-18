using Tonga.Bytes;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Bytes
{
    public sealed class HexBytesTests
    {
        [Theory]
        [InlineData("foobar", "666f6f626172")]
        public void BytesFromHex(string expected, string hex)
        {
            Assert.Equal(
                    expected,
                    AsText._(
                        new HexBytes(
                            AsText._(hex)
                        )
                    ).AsString()
                );
        }
    }
}
