

using System;
using Xunit;
using Tonga.Enumerable;

namespace Tonga.Scalar.Tests
{
    public sealed class FirstOfTests
    {
        [Fact]
        public void ThrowsCustomException()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new First<string>(
                    new None(),
                    new InvalidOperationException()
                ).Value()
            );
        }

        [Fact]
        public void ReturnsFallBack()
        {
            Assert.Equal(
                "gotcha",
                new First<string>(
                    new None(), "gotcha").Value()
                );
        }

        [Fact]
        public void ReturnsFirstMatch()
        {
            var list = Enumerable.EnumerableOf.Pipe("hallo", "ich", "heisse", "Max");

            Assert.Equal(
                "Max",
                new First<string>(item => item.StartsWith("M"), list).Value()
            );
        }

        [Fact]
        public void ReturnsFirstValue()
        {
            var list = Enumerable.EnumerableOf.Pipe("hallo", "ich", "heisse", "Max");

            Assert.Equal(
                "hallo",
                new First<string>(list).Value()
            );
        }
    }
}
