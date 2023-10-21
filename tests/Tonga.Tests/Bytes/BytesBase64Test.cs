

using Xunit;

namespace Tonga.Bytes.Tests
{
    public sealed class BytesBase64Test
    {
        [Fact]
        public void EncodesBase64()
        {
            Assert.True(
                new BytesEqual(
                    new BytesAsBase64(
                        new AsBytes(
                            "Hello!")
                    ),
                    new AsBytes(
                        "SGVsbG8h")
                ).Value()
            );
        }
    }
}
