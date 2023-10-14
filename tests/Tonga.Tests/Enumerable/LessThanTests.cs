

using Xunit;
using Tonga.Enumerable;

namespace Tonga.Enumerable.Test
{
    public sealed class LessThanTests
    {
        [Fact]
        public void DetectsLess()
        {
            Assert.True(
                new LessThan(
                    3,
                    Params.Of("a", "b")
                ).Value()
            );
        }

        [Fact]
        public void NoMatchOnMore()
        {
            Assert.False(
                new LessThan(
                    3,
                    Params.Of("a", "b", "c", "d")
                ).Value()
            );
        }

        [Fact]
        public void NoMatchOnEqual()
        {
            Assert.False(
                new LessThan(
                    3,
                    Params.Of("a", "b", "c")
                ).Value()
            );
        }
    }
}
