

using Tonga.Swap;
using Xunit;

namespace Tonga.Tests.Swap
{
    public sealed class AsSwapTest
    {
        [Fact]
        public void Swaps()
        {
            Assert.Equal(
                1,
                AsSwap._<int,int>(
                    (input) => 1
                ).Flip(2)
            );
        }
    }
}
