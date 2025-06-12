

using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class RotatedTest
    {
        [Fact]
        public void RotateRightText()
        {
            Assert.Equal(
                "o!Hell",
                "Hello!".AsText().AsRotated(2).Str()
            );
        }

        [Fact]
        public void RotateLeftText()
        {
            Assert.Equal(
                "i!H",
                "Hi!".AsText().AsRotated(-1).Str()
            );
        }

        [Fact]
        public void NoRotateWhenShiftZero()
        {
            var nonrotate = "Atoms!";
            Assert.Equal(
                nonrotate,
                nonrotate.AsText().AsRotated(0).Str()
            );
        }

        [Fact]
        public void NoRotateWhenShiftModZero()
        {
            var nonrotate = "Rotate";
            Assert.Equal(
                nonrotate,
                nonrotate.AsText().AsRotated(nonrotate.Length).Str()
            );
        }

        [Fact]
        public void NoRotateWhenEmpty()
        {
            Assert.Equal(
                "",
                "".AsText().AsRotated(2).Str()
            );
        }
    }
}
