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
                new SwapSwitch<string, bool>(
                    "true", new SwapOf<string,bool>(()=>true),
                    "false", new SwapOf<string, bool>(() => false)
                ).Flip("true","")
            );
        }
    }
}
