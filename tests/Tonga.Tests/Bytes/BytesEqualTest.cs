

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
                    new BytesOf(3.2d),
                    new BytesOf(3.2d)
                ).Value()
            );
        }

        [Fact]
        public void FalseDifferentLenght()
        {
            Assert.False(
                new BytesEqual(
                    new BytesOf(1),
                    new BytesOf(3.2d)
                ).Value()
            );
        }

        [Fact]
        public void FalseDifferentBytes()
        {
            Assert.False(
                new BytesEqual(
                    new BytesOf(1),
                    new BytesOf(3.2d)
                ).Value()
            );
        }
    }
}
