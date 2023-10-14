

using System;
using Xunit;
using Tonga.Text;

namespace Tonga.Scalar.Tests
{
    public sealed class DoubleOfTest
    {
        [Fact]
        public void NumberTest()
        {
            Assert.True(
                new DoubleOf("185.65156465123").Value() == 185.65156465123,
                "Can't parse double number");
        }

        [Fact]
        public void FailsIfTextDoesNotRepresentADouble()
        {
            Assert.Throws<FormatException>(() => new DoubleOf("abc").Value());
        }
    }
}
