using System;
using System.Globalization;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class FormattedTest
    {
        [Fact]
        public void FormatsText()
        {
            Assert.Equal(
                "1 Formatted text",
                new Formatted(
                    "{0} Formatted {1}", 1.ToString(), "text"
                ).Str()
            );
        }

        [Fact]
        public void FailsForInvalidPattern()
        {
            Assert.Throws<FormatException>(() =>
                new Formatted(
                    "Formatted { {0} }", "invalid").Str()
                );
        }

        [Fact]
        public void FormatsTextWithCollection()
        {
            Assert.Equal(
                "1. Formatted as txt",
                new Formatted(
                    "{0}. Formatted as {1}".AsText(), "1", "txt"
                ).Str()
            );
        }

        [Fact]
        public void FormatsWithLocale()
        {
            Assert.Equal(
                "1234567890,0",
                new Formatted(
                    "{0:0.0}",
                    new CultureInfo("de-DE"),
                    1234567890
                ).Str()
            );
        }

        [Fact]
        public void FormatsWithstringAndText()
        {
            Assert.Equal(
                "This is a FormattedText test",
                new Formatted(
                    "{0} is a {1} test",
                    "This".AsText(),
                    "FormattedText".AsText()
                ).Str()
            );

        }
    }
}
