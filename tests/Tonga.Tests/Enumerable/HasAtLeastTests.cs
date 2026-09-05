using System;
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
                ("a", "b", "c")
                    .AsEnumerable()
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
        public void MatchesOnMore()
        {
            Assert.True(
                ("a", "b", "c", "d")
                    .AsEnumerable()
                    .HasAtLeast(3)
                    .IsTrue()
            );
        }

        [Fact]
        public void MatchesZeroOnEmpty()
        {
            Assert.True(
                new Empty<string>()
                    .HasAtLeast(0)
                    .IsTrue()
            );
        }

        [Fact]
        public void NoMatchOnEmpty()
        {
            Assert.False(
                new Empty<string>()
                    .HasAtLeast(1)
                    .IsTrue()
            );
        }

        [Fact]
        public void StopsAtAmount()
        {
            var advanced = 0;
            Assert.True(
                ("a", "b", "c", "d", "e", "f")
                    .AsEnumerable()
                    .OnEach(_ => advanced++)
                    .HasAtLeast(3)
                    .IsTrue()
            );
            Assert.Equal(3, advanced);
        }

        [Fact]
        public void RejectsNegativeAmount()
        {
            Assert.Throws<ArgumentException>(() =>
                ("a", "b")
                    .AsEnumerable()
                    .HasAtLeast(-1)
                    .IsTrue()
            );
        }
    }
}
