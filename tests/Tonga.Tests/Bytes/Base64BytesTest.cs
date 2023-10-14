

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
                    new Base64Bytes(
                        new BytesOf(
                            "SGVsbG8h")
                    ),
                    new BytesOf(
                        "Hello!")
                ).Value()
            );
        }
    }
}
