using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class GZipDecompressingTests
    {
        [Fact]
        public void Decompresses()
        {
            byte[] bytes = {
                0x1F, //GZIP Header ID1
                0x8b, //GZIP Header ID2
                0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //Header End
                0x0B, 0xF2, 0x48, 0xCD, 0xC9, 0xC9, 0x57,
                0x04, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x03,
                0x00, 0x56, 0xCC, 0x2A, 0x9D, 0x06, 0x00,
                0x00, 0x00
            };

            Assert.Equal(
                "Hello!",
                AsText._(
                    new GZipDecompressing(
                        new Tonga.IO.AsConduit(bytes)
                    )
                ).AsString()
            );
        }
    }
}
