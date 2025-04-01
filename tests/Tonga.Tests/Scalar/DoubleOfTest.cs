using System;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Scalar
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
