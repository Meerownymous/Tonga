

using System;
using System.Globalization;
using Xunit;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Tonga.Text.Test
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
                    new LiveText("{0}. Number as {1}"),
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
                    new LiveText("Formatted { {0} }"),
                    new string[] { "invalid" }
            ).AsString());
        }

        [Fact]
        public void FormatsTextWithCollection()
        {
            Assert.True(
                new Formatted(
                    new TextOf("{0}. Formatted as {1}"),
                    new String[] { "1", "txt" }
                ).AsString() == "1. Formatted as txt"
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
                    new LiveText("This"),
                    new LiveText("FormattedText")
                ).AsString()
            );

        }
    }
}
