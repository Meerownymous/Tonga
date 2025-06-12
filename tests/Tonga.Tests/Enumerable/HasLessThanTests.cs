using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class HasLessThanTests
    {
        [Fact]
        public void DetectsLess()
        {
            Assert.True(
                ("a", "b")
                    .AsEnumerable()
                    .HasLessThan(3)
                    .IsTrue()
            );
        }

        [Fact]
        public void NoMatchOnMore()
        {
            Assert.False(
                ("a", "b", "c", "d")
                    .AsEnumerable()
                    .HasLessThan(3)
                    .IsTrue()
            );
        }

        [Fact]
        public void NoMatchOnEqual()
        {
            Assert.False(
                ("a", "b", "c")
                    .AsEnumerable()
                    .HasLessThan(3)
                    .IsTrue()
            );
        }
    }
}
