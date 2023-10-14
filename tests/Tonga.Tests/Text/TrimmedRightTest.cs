

using Xunit;
using Tonga.Scalar;

namespace Tonga.Text.Test
{
    public sealed class TrimmedRightTest
    {
        [Fact]
        public void TrimsWhitespaceEscapeSequences()
        {
            Assert.True(
                new TrimmedRight(
                    new LiveText("   \b \f \n \r \t \v   ")
                ).AsString() == string.Empty
            );
        }

        [Fact]
        public void TrimsString()
        {
            Assert.True(
                new TrimmedRight(" \b   \t      Hello! \t \b  ").AsString() == " \b   \t      Hello!"
            );
        }

        [Fact]
        public void TrimsText()
        {
            Assert.True(
                new TrimmedRight(
                    new LiveText(" \b   \t      Hello! \t \b  ")
                ).AsString() == " \b   \t      Hello!"
            );
        }

        [Fact]
        public void TrimsStringWithCharArray()
        {
            Assert.True(
                new TrimmedRight(
                    " \b   \t      Hello! \t \b  ",
                    new char[] { '\b', '\t', ' ', 'H', '!', 'o' }
                ).AsString() == " \b   \t      Hell"
            );
        }

        [Fact]
        public void TrimsTextWithCharArray()
        {
            Assert.True(
                new TrimmedRight(new LiveText(" \b   \t      Hello! \t \b  "), new char[] { '\b', '\t', ' ', 'H', '!', 'o' }).AsString() == " \b   \t      Hell"
            );
        }

        [Fact]
        public void TrimsTextWithScalar()
        {
            Assert.True(
                new TrimmedRight(
                    new LiveText(" \b   \t      Hello! \t \b  "),
                    new Live<char[]>(() => new char[] { '\b', '\t', ' ', 'H', '!', 'o' })
                ).AsString() == " \b   \t      Hell"
            );
        }

        [Fact]
        public void RemovesStringFromString()
        {
            Assert.True(
                new TrimmedRight(" \b   \t      Hello! \t \b   \t      H", " \b   \t      H").AsString() == " \b   \t      Hello! \t"
            );
        }

        [Fact]
        public void RemovesTextFromString()
        {
            Assert.True(
                new TrimmedRight(
                    new LiveText(" \b   \t      Hello! \t \b   \t      H"), " \b   \t      H").AsString() == " \b   \t      Hello! \t"
            );
        }

        [Fact]
        public void RemovesStringFromText()
        {
            Assert.True(
                new TrimmedRight(" \b   \t      Hello! \t \b   \t      H",
                    new LiveText(" \b   \t      H")
                ).AsString() == " \b   \t      Hello! \t"
            );
        }

        [Fact]
        public void RemovesTextFromText()
        {
            Assert.True(
                new TrimmedRight(
                    new LiveText(" \b   \t      Hello! \t \b   \t      H"),
                    new LiveText(" \b   \t      H")
                ).AsString() == " \b   \t      Hello! \t"
            );
        }
    }
}
