using Tonga.Swap;
using Xunit;

namespace Tonga.Tests.Swap
{
    public sealed class SwapSwitchTests
    {
        [Fact]
        public void ChoosesCorrectFunc()
        {
            Assert.True(
                SwapSwitch._(
                    "true", AsSwap._<string,bool>(_ => true),
                    "false", AsSwap._<string,bool>(_ => false)
                ).Flip("true","irrelevant")
            );
        }
    }
}
