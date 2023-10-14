

using Xunit;

namespace Tonga.Text.Test
{
    public sealed class RotatedTest
    {
        [Fact]
        public void RotateRightText()
        {
            Assert.True(
                new Rotated(
                    new LiveText("Hello!"), 2
                ).AsString() == "o!Hell"
            );
        }

        [Fact]
        public void RotateLeftText()
        {
            Assert.True(
                new Rotated(
                    new LiveText("Hi!"), -1
                ).AsString() == "i!H"
            );
        }

        [Fact]
        public void NoRotateWhenShiftZero()
        {
            var nonrotate = "Atoms!";
            Assert.True(
                new Rotated(
                    new LiveText(nonrotate), 0
                ).AsString() == nonrotate
            );
        }

        [Fact]
        public void NoRotateWhenShiftModZero()
        {
            var nonrotate = "Rotate";
            Assert.True(
                new Rotated(
                    new LiveText(nonrotate), nonrotate.Length
                ).AsString() == nonrotate,
                "Can't rotate text shift mod zero");
        }

        [Fact]
        public void NoRotateWhenEmpty()
        {
            Assert.True(
                new Rotated(
                    new LiveText(""), 2
                ).AsString() == ""
            );
        }
    }
}
