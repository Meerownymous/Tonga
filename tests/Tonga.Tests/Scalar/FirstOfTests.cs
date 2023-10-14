

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
                    new ManyOf<string>(),
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
                    new ManyOf(), "gotcha").Value()
                );
        }

        [Fact]
        public void ReturnsFirstMatch()
        {
            var list = new ManyOf<string>("hallo", "ich", "heisse", "Max");

            Assert.Equal(
                "Max",
                new FirstOf<string>(item => item.StartsWith("M"), list).Value()
            );
        }

        [Fact]
        public void ReturnsFirstValue()
        {
            var list = new ManyOf<string>("hallo", "ich", "heisse", "Max");

            Assert.Equal(
                "hallo",
                new FirstOf<string>(list).Value()
            );
        }
    }
}
