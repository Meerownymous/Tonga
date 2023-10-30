

using Xunit;

namespace Tonga.Bytes.Tests
{
    public sealed class Base64BytesTest
    {
        [Fact]
        public void EncodesBase64()
        {
            Assert.True(
                new BytesEqual(
                    new Base64Decoded(
                        new AsBytes(
                            "SGVsbG8h")
                    ),
                    new AsBytes(
                        "Hello!")
                ).Value()
            );
        }
    }
}
