using Tonga.Enumerable;
using Tonga.Fact;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class IsMoreTests
    {
        [Fact]
        public void DetectsMore()
        {
            Assert.True(
                MoreThan._(
                    3,
                    AsEnumerable._("a", "b", "c", "d")
                ).IsTrue()
            );
        }

        [Fact]
        public void NoMatchOnLess()
        {
            Assert.False(
                MoreThan._(3,
                    AsEnumerable._("a", "b")
                ).IsTrue()
            );
        }

        [Fact]
        public void NoMatchOnEqual()
        {
            Assert.False(
                MoreThan._(3,
                    AsEnumerable._("a", "b", "c")
                ).IsTrue()
            );
        }
    }
}
