

using System;
using Xunit;
using Tonga.Text;

namespace Tonga.Scalar.Tests
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
