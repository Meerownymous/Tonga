using Xunit;
using Tonga.Func;
using Tonga.Scalar;
using Tonga.Swap;

namespace Tonga.Swap.Tests
{
    public sealed class SwapSwitchTests
    {
        [Fact]
        public void ChoosesCorrectFunc()
        {
            Assert.True(
                SwapSwitch._(
                    "true", AsSwap._<string,bool>((irrelevant) => true),
                    "false", AsSwap._<string,bool>((irrelevant) => false)
                ).Flip("true","irrelevant")
            );
        }
    }
}
