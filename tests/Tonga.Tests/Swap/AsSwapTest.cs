

using Xunit;

namespace Tonga.Swap.Tests
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
