

using Xunit;
using Tonga.Enumerable;

namespace Tonga.Scalar.Test
{
    public sealed class IsLessTests
    {
        [Fact]
        public void DetectsLess()
        {
            Assert.True(
                new IsLess(
                    3,
                    AsEnumerable._("a", "b")
                ).Value()
            );
        }

        [Fact]
        public void NoMatchOnMore()
        {
            Assert.False(
                new IsLess(
                    3,
                    AsEnumerable._("a", "b", "c", "d")
                ).Value()
            );
        }

        [Fact]
        public void NoMatchOnEqual()
        {
            Assert.False(
                new IsLess(
                    3,
                    AsEnumerable._("a", "b", "c")
                ).Value()
            );
        }
    }
}
