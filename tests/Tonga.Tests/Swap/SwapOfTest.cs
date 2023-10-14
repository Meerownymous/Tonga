

using Xunit;
using Tonga.Swap;

namespace Tonga.Swap.Tests
{
    public sealed class SwapOfTest
    {
        [Fact]
        public void Swaps()
        {
            Assert.True(
            new SwapOf<int,int>(
                () => 1
            ).Flip(2) == 1,
            "cannot convert func into callable");
        }
    }
}
