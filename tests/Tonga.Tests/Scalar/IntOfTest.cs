

using System;
using Xunit;
using Tonga.Text;

namespace Tonga.Scalar.Tests
{
    public sealed class IntOfTest
    {
        [Fact]
        public void NumberTest()
        {
            Assert.True(
            new IntOf("1867892354").Value() == 1867892354,
            "Can't parse integer number");
        }

        [Fact]
        public void FailsIfTextDoesNotRepresentAnInt()
        {
            Assert.Throws<FormatException>(
                () => new DoubleOf("abc").Value());

        }
    }
}
