using Xunit;
using Tonga.Fact;

namespace Tonga.Enumerable.Test
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
