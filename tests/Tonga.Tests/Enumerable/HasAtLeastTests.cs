using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class HasAtLeastTests
    {
        [Fact]
        public void DetectsMatch()
        {
            Assert.True(
                ("a", "b", "c").AsEnumerable()
                .HasAtLeast(3)
                .IsTrue()
            );
        }

        [Fact]
        public void NoMatchOnLess()
        {
            Assert.False(
                ("a", "b")
                    .AsEnumerable()
                    .HasAtLeast(3)
                    .IsTrue()
            );
        }

        [Fact]
        public void NoMatchOnMore()
        {
            Assert.False(
                ("a", "b", "c", "d")
                    .AsEnumerable()
                    .HasAtLeast(3)
                    .IsTrue()
            );
        }
    }
}
