

using System;
using Xunit;
using Tonga.Enumerable;

namespace Tonga.Scalar.Tests
{
    public sealed class FirstTests
    {
        [Fact]
        public void ThrowsCustomException()
        {
            Assert.Throws<InvalidOperationException>(() =>
                First._(
                    None._<string>(),
                    new InvalidOperationException()
                ).Value()
            );
        }

        [Fact]
        public void ReturnsFallBack()
        {
            Assert.Equal(
                "gotcha",
                First._(
                    None._<string>(),
                    "gotcha"
                ).Value()
            );
        }

        [Fact]
        public void ReturnsFirstMatch()
        {
            Assert.Equal(
                "Max",
                First._(
                    item => item.StartsWith("M"),
                    AsEnumerable._("hallo", "ich", "heisse", "Max")
                ).Value()
            );
        }

        [Fact]
        public void ReturnsFirstValue()
        {
            Assert.Equal(
                "hallo",
                First._(
                    AsEnumerable._("hallo", "ich", "heisse", "Max")
                ).Value()
            );
        }
    }
}
