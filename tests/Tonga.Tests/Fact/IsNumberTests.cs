using Tonga.Fact;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Fact
{
    public sealed class IsNumberTests
    {
        [Fact]
        public void DetectsNumber()
        {
            Assert.True(
                new IsNumber(
                    "1,234.56"
                ).IsTrue(),
                "Can't read number from string"
            );
        }

        [Fact]
        public void DetectsCustomCultureNumber()
        {
            Assert.True(
                new IsNumber(
                    "1234,56",
                    new System.Globalization.NumberFormatInfo
                    {
                        NumberDecimalSeparator = ","
                    }
                ).IsTrue(),
                "Can't read number from string using custom format provider"
            );
        }

        [Fact]
        public void DetectsNumberFromText()
        {
            Assert.True(
                new IsNumber(
                    AsText._(
                        "1,234.56"
                    )
                ).IsTrue(),
                "Can't read number from text"
            );
        }

        [Fact]
        public void DetectsCustomCultureNumberFromText()
        {
            Assert.True(
                new IsNumber(
                    AsText._(
                        "1234,56"
                    ),
                    new System.Globalization.NumberFormatInfo
                    {
                        NumberDecimalSeparator = ","
                    }
                ).IsTrue(),
                "Can't read number from text using custom format provider"
            );
        }

        [Fact]
        public void DetectsNoNumber()
        {
            Assert.False(
                new IsNumber(
                    "not a number"
                ).IsTrue(),
                "Falsely recognized a string as a number"
            );
        }

        [Fact]
        public void DetectsNoNumberFromText()
        {
            Assert.False(
                new IsNumber(
                    AsText._(
                        "not a number"
                    )
                ).IsTrue(),
                "Falsely recognized a text as a number"
            );
        }
    }
}
