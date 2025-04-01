

using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class RotatedTest
    {
        [Fact]
        public void RotateRightText()
        {
            Assert.True(
                new Rotated(
                    AsText._("Hello!"), 2
                ).AsString() == "o!Hell"
            );
        }

        [Fact]
        public void RotateLeftText()
        {
            Assert.True(
                new Rotated(
                    AsText._("Hi!"), -1
                ).AsString() == "i!H"
            );
        }

        [Fact]
        public void NoRotateWhenShiftZero()
        {
            var nonrotate = "Atoms!";
            Assert.True(
                new Rotated(
                    AsText._(nonrotate), 0
                ).AsString() == nonrotate
            );
        }

        [Fact]
        public void NoRotateWhenShiftModZero()
        {
            var nonrotate = "Rotate";
            Assert.True(
                new Rotated(
                    AsText._(nonrotate), nonrotate.Length
                ).AsString() == nonrotate,
                "Can't rotate text shift mod zero");
        }

        [Fact]
        public void NoRotateWhenEmpty()
        {
            Assert.True(
                new Rotated(
                    AsText._(""), 2
                ).AsString() == ""
            );
        }
    }
}
