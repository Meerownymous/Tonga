using Xunit;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.Enumerable.Test
{
    public sealed class ExactAmountTests
    {
        [Fact]
        public void DetectsMatch()
        {
            Assert.True(
                new ExactAmount(3,
                    AsEnumerable._("a", "b", "c")
                ).Value()
            );
        }

        [Fact]
        public void NoMatchOnLess()
        {
            Assert.False(
                new ExactAmount(3,
                    AsEnumerable._("a", "b")
                ).Value()
            );
        }

        [Fact]
        public void NoMatchOnMore()
        {
            Assert.False(
                new ExactAmount(3,
                    AsEnumerable._("a", "b", "c", "d")
                ).Value()
            );
        }
    }
}
