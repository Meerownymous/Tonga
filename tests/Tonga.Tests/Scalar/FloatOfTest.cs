using System;
using Tonga.Primitives;
using Xunit;

namespace Tonga.Tests.Scalar
{
    public sealed class FloatOfTest
    {
        [Fact]
        public void NumberTest()
        {
            Assert.Equal(
                1656.894F,
                new FloatOf("1656.894").Value()
            );
        }

        [Fact]
        public void FailsIfTextDoesNotRepresentAFloat()
        {
            Assert.Throws<FormatException>(
                () => new FloatOf("abc").Value()
            );
        }
    }
}
