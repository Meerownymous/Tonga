

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
                    new ManyOf(),
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
                    new ManyOf(),
                    "gotcha"
                ).Value()
            );
        }

        [Fact]
        public void ReturnsLastValue()
        {
            var list = new ManyOf("hallo", "ich", "heisse", "Max");

            Assert.Equal(
                "Max",
                new LastOf<string>(list).Value()
            );
        }
    }
}
