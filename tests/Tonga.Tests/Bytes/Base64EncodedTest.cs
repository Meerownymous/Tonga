

using Tonga.Bytes;
using Xunit;

namespace Tonga.Tests.Bytes
{
    public sealed class BytesBase64Test
    {
        [Fact]
        public void EncodesBase64()
        {
            Assert.True(
                new BytesEqual(
                    new Base64Encoded(
                        new AsBytes(
                            "Hello!"
                        )
                    ),
                    new AsBytes(
                        "SGVsbG8h"
                    )
                ).Value()
            );
        }
    }
}
