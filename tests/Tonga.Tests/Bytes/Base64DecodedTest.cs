

using Tonga.Bytes;
using Xunit;

namespace Tonga.Tests.Bytes
{
    public sealed class Base64BytesTest
    {
        [Fact]
        public void EncodesBase64()
        {
            Assert.True(
                new IsEqual(
                    new Base64Decoded(
                        new AsBytes(
                            "SGVsbG8h")
                    ),
                    new AsBytes(
                        "Hello!")
                ).IsTrue()
            );
        }
    }
}
