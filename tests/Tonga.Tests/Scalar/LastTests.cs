using System;
using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Scalar
{
    public sealed class LastTests
    {
        [Fact]
        public void ThrowsCustomException()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new None<string>()
                    .Last(new InvalidOperationException())
                    .Value()
            );
        }

        [Fact]
        public void ReturnsFallback()
        {
            Assert.Equal(
                "gotcha",
                new None<string>()
                    .Last("gotcha")
                    .Value()

            );
        }

        [Fact]
        public void ReturnsLastValue()
        {
            Assert.Equal(
                "Max",
                ("hallo", "ich", "heisse", "Max")
                    .AsEnumerable()
                    .Last()
                    .Value()
            );
        }
    }
}
