

using Xunit;
using Tonga.Scalar;

namespace Tonga.Text.Test
{
    public sealed class TrimmedTest
    {
        [Fact]
        public void TrimsWhitespaceEscapeSequences()
        {
            Assert.True(
                new Trimmed(
                    new TextOf("   \b \f \n \r \t \v   ")
                ).AsString() == string.Empty
            );
        }

        [Fact]
        public void TrimsString()
        {
            Assert.True(
                new Trimmed(" \b   \t      Hello! \t \b  ").AsString() == "Hello!"
            );
        }

        [Fact]
        public void TrimsText()
        {
            Assert.True(
                new Trimmed(new LiveText(" \b   \t      Hello! \t \b  ")).AsString() == "Hello!"
            );
        }

        [Fact]
        public void TrimsStringWithCharArray()
        {
            Assert.True(
                new Trimmed(" \b   \t      Hello! \t \b  ", new char[] { '\b', '\t', ' ', 'H', 'o' }).AsString() == "ello!"
            );
        }

        [Fact]
        public void TrimsTextWithCharArray()
        {
            Assert.True(
                new Trimmed(new LiveText(" \b   \t      Hello! \t \b  "), new char[] { '\b', '\t', ' ', 'H', 'o' }).AsString() == "ello!"
            );
        }

        [Fact]
        public void TrimsTextWithScalar()
        {
            Assert.True(
                new Trimmed(new LiveText(" \b   \t      Hello! \t \b  "), new Live<char[]>(() => new char[] { '\b', '\t', ' ', 'H', 'o' })).AsString() == "ello!"
            );
        }

        [Fact]
        public void RemovesStringFromString()
        {
            Assert.True(
                new Trimmed(" \b   \t      Hello! \t \b   \t      H", " \b   \t      H").AsString() == "ello! \t"
            );
        }

        [Fact]
        public void RemovesTextFromString()
        {
            Assert.True(
                new Trimmed(new LiveText(" \b   \t      Hello! \t \b   \t      H"), " \b   \t      H").AsString() == "ello! \t"
            );
        }

        [Fact]
        public void RemovesStringFromText()
        {
            Assert.True(
                new Trimmed(" \b   \t      Hello! \t \b   \t      H", new LiveText(" \b   \t      H")).AsString() == "ello! \t"
            );
        }

        [Fact]
        public void RemovesTextFromText()
        {
            Assert.True(
                new Trimmed(new LiveText(" \b   \t      Hello! \t \b   \t      H"), new LiveText(" \b   \t      H")).AsString() == "ello! \t"
            );
        }

        [Fact]
        public void RemovesMultipleTextOccurenceFromText()
        {
            Assert.Equal(
                "World ",
                new Trimmed(
                    new LiveText("Hello Hello World Hello "),
                    new LiveText("Hello ")
                ).AsString()
            );
        }
    }
}
