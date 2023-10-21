

using Xunit;

namespace Tonga.Bytes.Tests
{
    public sealed class BytesEqualTest
    {
        [Fact]
        public void TrueCorrectBytes()
        {
            Assert.True(
                new BytesEqual(
                    new AsBytes(3.2d),
                    new AsBytes(3.2d)
                ).Value()
            );
        }

        [Fact]
        public void FalseDifferentLenght()
        {
            Assert.False(
                new BytesEqual(
                    new AsBytes(1),
                    new AsBytes(3.2d)
                ).Value()
            );
        }

        [Fact]
        public void FalseDifferentBytes()
        {
            Assert.False(
                new BytesEqual(
                    new AsBytes(1),
                    new AsBytes(3.2d)
                ).Value()
            );
        }
    }
}
