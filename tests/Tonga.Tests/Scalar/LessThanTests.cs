

using Xunit;
using Tonga.Enumerable;

namespace Tonga.Scalar.Test
{
    public sealed class LessThanTests
    {
        [Fact]
        public void DetectsLess()
        {
            Assert.True(
                new LessThan(
                    3,
                    EnumerableOf.Pipe("a", "b")
                ).Value()
            );
        }

        [Fact]
        public void NoMatchOnMore()
        {
            Assert.False(
                new LessThan(
                    3,
                    EnumerableOf.Pipe("a", "b", "c", "d")
                ).Value()
            );
        }

        [Fact]
        public void NoMatchOnEqual()
        {
            Assert.False(
                new LessThan(
                    3,
                    EnumerableOf.Pipe("a", "b", "c")
                ).Value()
            );
        }
    }
}
