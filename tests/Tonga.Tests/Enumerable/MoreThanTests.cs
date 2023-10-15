

using Xunit;
using Tonga.Enumerable;

namespace Tonga.Enumerable.Test
{
    public sealed class MoreThanTests
    {
        [Fact]
        public void DetectsMore()
        {
            Assert.True(
                new MoreThan(
                    3,
                    EnumerableOf.Pipe("a", "b", "c", "d")
                ).Value()
            );
        }

        [Fact]
        public void NoMatchOnLess()
        {
            Assert.False(
                new MoreThan(
                    3,
                    EnumerableOf.Pipe("a", "b")
                ).Value()
            );
        }

        [Fact]
        public void NoMatchOnEqual()
        {
            Assert.False(
                new MoreThan(
                    3,
                    EnumerableOf.Pipe("a", "b", "c")
                ).Value()
            );
        }
    }
}
