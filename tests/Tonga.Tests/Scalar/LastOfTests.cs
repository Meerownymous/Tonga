

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
                Last.From(
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
                Last.From(
                    new None(),
                    "gotcha"
                ).Value()
            );
        }

        [Fact]
        public void ReturnsLastValue()
        {
            Assert.Equal(
                "Max",
                Last.From(
                    AsEnumerable._("hallo", "ich", "heisse", "Max")
                ).Value()
            );
        }
    }
}
