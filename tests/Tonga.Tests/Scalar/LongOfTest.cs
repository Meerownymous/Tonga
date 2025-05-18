using System;
using Tonga.Primitives;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Scalar
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
