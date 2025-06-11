using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class HasMoreThanTests
    {
        [Fact]
        public void DetectsMore()
        {
            Assert.True(
                ("a", "b", "c", "d").AsEnumerable()
                    .HasMoreThan(3)
                    .IsTrue()
            );
        }

        [Fact]
        public void NoMatchOnLess()
        {
            Assert.False(
                ("a", "b")
                    .AsEnumerable()
                    .HasMoreThan(3)
                    .IsTrue()
            );
        }

        [Fact]
        public void NoMatchOnEqual()
        {
            Assert.False(
                ("a", "b", "c")
                    .AsEnumerable()
                    .HasMoreThan(3)
                    .IsTrue()
            );
        }
    }
}
