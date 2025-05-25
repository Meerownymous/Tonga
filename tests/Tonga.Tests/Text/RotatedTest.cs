

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
                ).Str() == "o!Hell"
            );
        }

        [Fact]
        public void RotateLeftText()
        {
            Assert.True(
                new Rotated(
                    AsText._("Hi!"), -1
                ).Str() == "i!H"
            );
        }

        [Fact]
        public void NoRotateWhenShiftZero()
        {
            var nonrotate = "Atoms!";
            Assert.True(
                new Rotated(
                    AsText._(nonrotate), 0
                ).Str() == nonrotate
            );
        }

        [Fact]
        public void NoRotateWhenShiftModZero()
        {
            var nonrotate = "Rotate";
            Assert.True(
                new Rotated(
                    AsText._(nonrotate), nonrotate.Length
                ).Str() == nonrotate,
                "Can't rotate text shift mod zero");
        }

        [Fact]
        public void NoRotateWhenEmpty()
        {
            Assert.True(
                new Rotated(
                    AsText._(""), 2
                ).Str() == ""
            );
        }
    }
}
