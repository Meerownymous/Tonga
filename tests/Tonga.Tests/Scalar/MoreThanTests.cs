

using Xunit;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.Enumerable.Test
{
    public sealed class MoreThanTests
    {
        [Fact]
        public void DetectsMore()
        {
            Assert.True(
                MoreThan._(
                    3,
                    AsEnumerable._("a", "b", "c", "d")
                ).Value()
            );
        }

        [Fact]
        public void NoMatchOnLess()
        {
            Assert.False(
                MoreThan._(3,
                    AsEnumerable._("a", "b")
                ).Value()
            );
        }

        [Fact]
        public void NoMatchOnEqual()
        {
            Assert.False(
                MoreThan._(3,
                    AsEnumerable._("a", "b", "c")
                ).Value()
            );
        }
    }
}
