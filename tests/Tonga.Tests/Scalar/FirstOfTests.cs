

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
                new FirstOf<string>(
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
                new FirstOf<string>(
                    new None(), "gotcha").Value()
                );
        }

        [Fact]
        public void ReturnsFirstMatch()
        {
            var list = Params.Of("hallo", "ich", "heisse", "Max");

            Assert.Equal(
                "Max",
                new FirstOf<string>(item => item.StartsWith("M"), list).Value()
            );
        }

        [Fact]
        public void ReturnsFirstValue()
        {
            var list = Params.Of("hallo", "ich", "heisse", "Max");

            Assert.Equal(
                "hallo",
                new FirstOf<string>(list).Value()
            );
        }
    }
}
