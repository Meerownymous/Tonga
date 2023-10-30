

using System;
using Xunit;
using Tonga.Enumerable;

namespace Tonga.Scalar.Tests
{
    public sealed class LastTests
    {
        [Fact]
        public void ThrowsCustomException()
        {
            Assert.Throws<InvalidOperationException>(() =>
                Last._(
                    None._<string>(),
                    new InvalidOperationException()
                ).Value()
            );
        }

        [Fact]
        public void ReturnsFallback()
        {
            Assert.Equal(
                "gotcha",
                Last._(
                    None._<string>(),
                    "gotcha"
                ).Value()
            );
        }

        [Fact]
        public void ReturnsLastValue()
        {
            Assert.Equal(
                "Max",
                Last._(
                    AsEnumerable._("hallo", "ich", "heisse", "Max")
                ).Value()
            );
        }
    }
}
