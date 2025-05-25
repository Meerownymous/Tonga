using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text;

public sealed class TrimmedTest
{
    [Fact]
    public void TrimsWhitespaceEscapeSequences()
    {
        Assert.True(
            new Trimmed(
                AsText._("   \b \f \n \r \t \v   ")
            ).Str() == string.Empty
        );
    }

    [Fact]
    public void TrimsString()
    {
        Assert.True(
            new Trimmed(" \b   \t      Hello! \t \b  ").Str() == "Hello!"
        );
    }

    [Fact]
    public void TrimsText()
    {
        Assert.True(
            new Trimmed(AsText._(" \b   \t      Hello! \t \b  ")).Str() == "Hello!"
        );
    }

    [Fact]
    public void TrimsStringWithCharArray()
    {
        Assert.True(
            new Trimmed(" \b   \t      Hello! \t \b  ", new char[] { '\b', '\t', ' ', 'H', 'o' }).Str() == "ello!"
        );
    }

    [Fact]
    public void TrimsTextWithCharArray()
    {
        Assert.True(
            new Trimmed(AsText._(" \b   \t      Hello! \t \b  "), new char[] { '\b', '\t', ' ', 'H', 'o' }).Str() == "ello!"
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
            ).Str()
        );
    }

    [Fact]
    public void RemovesStringFromString()
    {
        Assert.True(
            new Trimmed(" \b   \t      Hello! \t \b   \t      H", " \b   \t      H").Str() == "ello! \t"
        );
    }

    [Fact]
    public void RemovesTextFromString()
    {
        Assert.True(
            new Trimmed(AsText._(" \b   \t      Hello! \t \b   \t      H"), " \b   \t      H").Str() == "ello! \t"
        );
    }

    [Fact]
    public void RemovesStringFromText()
    {
        Assert.True(
            new Trimmed(" \b   \t      Hello! \t \b   \t      H", AsText._(" \b   \t      H")).Str() == "ello! \t"
        );
    }

    [Fact]
    public void RemovesTextFromText()
    {
        Assert.True(
            new Trimmed(AsText._(" \b   \t      Hello! \t \b   \t      H"), AsText._(" \b   \t      H")).Str() == "ello! \t"
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
            ).Str()
        );
    }
}
