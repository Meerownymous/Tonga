

using System;
using Xunit;
using Tonga.Enumerable;

namespace Tonga.Scalar.Tests
{
    public sealed class LastOfTests
    {
        [Fact]
        public void ThrowsCustomException()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new LastOf<string>(
                    new None(),
                    new InvalidOperationException()
                ).Value()
            );
        }

        [Fact]
        public void ReturnsFallback()
        {
            Assert.Equal(
                "gotcha",
                new LastOf<string>(
                    new None(),
                    "gotcha"
                ).Value()
            );
        }

        [Fact]
        public void ReturnsLastValue()
        {
            var list = Enumerable.EnumerableOf.Pipe("hallo", "ich", "heisse", "Max");

            Assert.Equal(
                "Max",
                new LastOf<string>(list).Value()
            );
        }
    }
}
