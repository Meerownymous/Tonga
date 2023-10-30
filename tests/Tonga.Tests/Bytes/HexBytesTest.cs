

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
                    AsText._(
                        new HexBytes(
                            AsText._(hex)
                        )
                    ).AsString()
                );
        }
    }
}
