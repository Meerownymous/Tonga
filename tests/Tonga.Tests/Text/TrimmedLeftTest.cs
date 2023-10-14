

using Xunit;
using Tonga.Scalar;

namespace Tonga.Text.Test
{
    public sealed class TrimmedLeftTest
    {
        [Fact]
        public void TrimsWhitespaceEscapeSequences()
        {
            Assert.True(
                new TrimmedLeft(new LiveText("   \b \f \n \r \t \v   ")).AsString() == string.Empty
            );
        }

        [Fact]
        public void TrimsString()
        {
            Assert.Equal(
                "Hello! \t \b  ",
                new TrimmedLeft(
                    " \b   \t      Hello! \t \b  "
                ).AsString()
            );
        }

        [Fact]
        public void TrimsText()
        {
            Assert.Equal(
                "Hello! \t \b  ",
                new TrimmedLeft(
                    new LiveText(" \b   \t      Hello! \t \b  ")
                ).AsString()
            );
        }

        [Fact]
        public void TrimsStringWithCharArray()
        {
            Assert.Equal(
                "ello! \t \b  ",
                new TrimmedLeft(
                    " \b   \t      Hello! \t \b  ",
                    new char[] { '\b', '\t', ' ', 'H', 'o' }
                ).AsString()
            );
        }

        [Fact]
        public void TrimsTextWithCharArray()
        {
            Assert.Equal(
                "ello! \t \b  ",
                new TrimmedLeft(
                    new LiveText(" \b   \t      Hello! \t \b  "),
                    new char[] { '\b', '\t', ' ', 'H', 'o' }
                ).AsString()
            );
        }

        [Fact]
        public void TrimsTextWithScalar()
        {
            Assert.Equal(
                "ello! \t \b  ",
                new TrimmedLeft(
                    new LiveText(" \b   \t      Hello! \t \b  "),
                    new Live<char[]>(() => new char[] { '\b', '\t', ' ', 'H', 'o' })
                ).AsString()
            );
        }

        [Fact]
        public void RemovesStringFromString()
        {
            Assert.Equal(
                "ello! \t \b   \t      H",
                new TrimmedLeft(
                    " \b   \t      Hello! \t \b   \t      H", " \b   \t      H"
                ).AsString()
            );
        }

        [Fact]
        public void RemovesTextFromString()
        {
            Assert.Equal(
                "ello! \t \b   \t      H",
                new TrimmedLeft(new LiveText(" \b   \t      Hello! \t \b   \t      H"), " \b   \t      H").AsString()
            );
        }

        [Fact]
        public void RemovesStringFromText()
        {
            Assert.Equal(
                "ello! \t \b   \t      H",
                new TrimmedLeft(" \b   \t      Hello! \t \b   \t      H", new LiveText(" \b   \t      H")).AsString()
            );
        }

        [Fact]
        public void RemovesTextFromText()
        {
            Assert.Equal(
                "ello! \t \b   \t      H",
                new TrimmedLeft(
                    new LiveText(" \b   \t      Hello! \t \b   \t      H"),
                    new LiveText(" \b   \t      H")
                ).AsString()
            );
        }
    }
}
