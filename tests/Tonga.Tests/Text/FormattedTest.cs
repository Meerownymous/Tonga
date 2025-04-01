using System;
using System.Globalization;
using Tonga.Text;
using Xunit;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Tonga.Tests.Text
{
    public sealed class FormattedTest
    {
        [Fact]
        public void FormatsText()
        {
            Assert.True(
                new Formatted(
                    "{0} Formatted {1}", 1, "text"
                ).AsString().Contains("1 Formatted text"),
                "Can't format a text");
        }

        [Fact]
        public void FormatsTextWithObjects()
        {
            Assert.True(
                new Formatted(
                    AsText._("{0}. Number as {1}"),
                    1,
                    "string"
                ).AsString().Contains("1. Number as string"),
                "Can't format a text with objects");
        }

        [Fact]
        public void FailsForInvalidPattern()
        {
            Assert.Throws<FormatException>(
                () => new Formatted(
                    AsText._("Formatted { {0} }"),
                    new string[] { "invalid" }
            ).AsString());
        }

        [Fact]
        public void FormatsTextWithCollection()
        {
            Assert.Equal(
                "1. Formatted as txt",
                new Formatted(
                    AsText._("{0}. Formatted as {1}"),
                    new String[] { "1", "txt" }
                ).AsString()
            );
        }

        [Fact]
        public void FormatsWithLocale()
        {
            Assert.True(
                new Formatted(
                    "{0:0.0}", new CultureInfo("de-DE"), 1234567890
                ).AsString() == "1234567890,0",
                "Can't format a text with Locale");
        }

        [Fact]
        public void FormatsWithstringAndText()
        {
            Assert.Equal(
                "This is a FormattedText test",
                new Formatted(
                    "{0} is a {1} test",
                    AsText._("This"),
                    AsText._("FormattedText")
                ).AsString()
            );

        }
    }
}
