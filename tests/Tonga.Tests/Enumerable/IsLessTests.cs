using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class IsLessTests
    {
        [Fact]
        public void DetectsLess()
        {
            Assert.True(
                IsLess._(
                    3,
                    AsEnumerable._("a", "b")
                ).IsTrue()
            );
        }

        [Fact]
        public void NoMatchOnMore()
        {
            Assert.False(
                IsLess._(
                    3,
                    AsEnumerable._("a", "b", "c", "d")
                ).IsTrue()
            );
        }

        [Fact]
        public void NoMatchOnEqual()
        {
            Assert.False(
                IsLess._(
                    3,
                    AsEnumerable._("a", "b", "c")
                ).IsTrue()
            );
        }
    }
}
