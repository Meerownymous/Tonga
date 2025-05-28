

using Tonga.Bytes;
using Xunit;

namespace Tonga.Tests.Bytes
{
    public sealed class IsEqualTest
    {
        [Fact]
        public void TrueCorrectBytes()
        {
            Assert.True(
                new IsEqual(
                    new AsBytes(3.2d),
                    new AsBytes(3.2d)
                ).Value()
            );
        }

        [Fact]
        public void FalseDifferentLenght()
        {
            Assert.False(
                new IsEqual(
                    new AsBytes(1),
                    new AsBytes(3.2d)
                ).Value()
            );
        }

        [Fact]
        public void FalseDifferentBytes()
        {
            Assert.False(
                new IsEqual(
                    new AsBytes(1),
                    new AsBytes(3.2d)
                ).Value()
            );
        }
    }
}
