

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
                    AsText._("   \b \f \n \r \t \v   ")
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
                new Trimmed(AsText._(" \b   \t      Hello! \t \b  ")).AsString() == "Hello!"
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
                new Trimmed(AsText._(" \b   \t      Hello! \t \b  "), new char[] { '\b', '\t', ' ', 'H', 'o' }).AsString() == "ello!"
            );
        }

        [Fact]
        public void TrimsTextByChars()
        {
            Assert.Equal(
                "ello!",
                new Trimmed(
                    AsText._(" \b   \t      Hello! \t \b  "),
                    () => new char[] { '\b', '\t', ' ', 'H', 'o' }
                ).AsString()
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
                new Trimmed(AsText._(" \b   \t      Hello! \t \b   \t      H"), " \b   \t      H").AsString() == "ello! \t"
            );
        }

        [Fact]
        public void RemovesStringFromText()
        {
            Assert.True(
                new Trimmed(" \b   \t      Hello! \t \b   \t      H", AsText._(" \b   \t      H")).AsString() == "ello! \t"
            );
        }

        [Fact]
        public void RemovesTextFromText()
        {
            Assert.True(
                new Trimmed(AsText._(" \b   \t      Hello! \t \b   \t      H"), AsText._(" \b   \t      H")).AsString() == "ello! \t"
            );
        }

        [Fact]
        public void RemovesMultipleTextOccurenceFromText()
        {
            Assert.Equal(
                "World ",
                new Trimmed(
                    AsText._("Hello Hello World Hello "),
                    AsText._("Hello ")
                ).AsString()
            );
        }
    }
}
