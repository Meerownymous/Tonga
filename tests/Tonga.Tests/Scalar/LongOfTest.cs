

using System;
using Xunit;
using Tonga.Text;

namespace Tonga.Scalar.Tests
{
    public sealed class LongOfTest
    {
        [Fact]
        public void NumberTest()
        {
            Assert.True(
                new LongOf("186789235425346").Value() == 186789235425346L);
        }

        [Fact]
        public void FailsIfTextDoesNotRepresentALong()
        {
            Assert.Throws<FormatException>(
                () => new LongOf("abc").Value());
        }
    }
}
