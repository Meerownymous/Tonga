using System;
using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Scalar
{
    public sealed class LastOneTests
    {
        [Fact]
        public void ThrowsCustomException()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new Empty<string>()
                    .LastOne(new InvalidOperationException())
                    .Value()
            );
        }

        [Fact]
        public void ReturnsFallback()
        {
            Assert.Equal(
                "gotcha",
                new Empty<string>()
                    .LastOne("gotcha")
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
                    .LastOne()
                    .Value()
            );
        }
    }
}
