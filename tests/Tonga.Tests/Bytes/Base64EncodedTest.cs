

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
                new IsEqual(
                    new Base64Encoded("Hello!"),
                    "SGVsbG8h"
                ).IsTrue()
            );
        }
    }
}
