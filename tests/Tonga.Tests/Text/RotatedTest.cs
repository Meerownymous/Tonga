

using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class RotatedTest
    {
        [Fact]
        public void RotateRightText()
        {
            AssertText.Equal(
                "o!Hell",
                new Rotated("Hello!", 2)
            );
        }

        [Fact]
        public void RotateLeftText()
        {
            AssertText.Equal(
                "i!H",
                new Rotated("Hi!", -1)
            );
        }

        [Fact]
        public void NoRotateWhenShiftZero()
        {
            var nonrotate = "Atoms!";
            AssertText.Equal(
                nonrotate,
                new Rotated(nonrotate,0)
            );
        }

        [Fact]
        public void NoRotateWhenShiftModZero()
        {
            var nonrotate = "Rotate";
            AssertText.Equal(
                nonrotate,
                new Rotated(nonrotate, nonrotate.Length)
            );
        }

        [Fact]
        public void NoRotateWhenEmpty()
        {
            AssertText.Equal(
                "",
                new Rotated("", 2)
            );
        }
    }
}
