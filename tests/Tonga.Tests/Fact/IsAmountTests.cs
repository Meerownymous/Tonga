using Tonga.Enumerable;
using Tonga.Fact;
using Xunit;

namespace Tonga.Tests.Fact
{
    public sealed class IsAmountTests
    {
        [Fact]
        public void DetectsMatch()
        {
            Assert.True(
                new IsAmount(3,
                    AsEnumerable._("a", "b", "c")
                ).IsTrue()
            );
        }

        [Fact]
        public void NoMatchOnLess()
        {
            Assert.False(
                new IsAmount(3,
                    AsEnumerable._("a", "b")
                ).IsTrue()
            );
        }

        [Fact]
        public void NoMatchOnMore()
        {
            Assert.False(
                new IsAmount(3,
                    AsEnumerable._("a", "b", "c", "d")
                ).IsTrue()
            );
        }
    }
}
