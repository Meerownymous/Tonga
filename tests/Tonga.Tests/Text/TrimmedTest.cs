using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text;

public sealed class TrimmedTest
{
    [Fact]
    public void TrimsWhitespaceEscapeSequences()
    {
        AssertText.Equal(
            string.Empty,
            "   \b \f \n \r \t \v   "
                .AsTrimmed()
        );
    }

    [Fact]
    public void TrimsString()
    {
        AssertText.Equal(
            "Hello!",
            " \b   \t      Hello! \t \b  "
                .AsTrimmed()
        );
    }

    [Fact]
    public void TrimsText()
    {
        AssertText.Equal(
            "Hello!",
            " \b   \t      Hello! \t \b  "
                .AsTrimmed()
        );
    }

    [Fact]
    public void TrimsStringWithCharArray()
    {
        AssertText.Equal(
            "ello!",
            " \b   \t      Hello! \t \b  "
                .AsTrimmed(['\b', '\t', ' ', 'H', 'o'])
        );
    }

    [Fact]
    public void TrimsTextWithCharArray()
    {
        AssertText.Equal(
            "ello!",
            " \b   \t      Hello! \t \b  "
                .AsTrimmed(['\b', '\t', ' ', 'H', 'o'])
        );
    }

    [Fact]
    public void TrimsTextByChars()
    {
        AssertText.Equal(
            "ello!",
            " \b   \t      Hello! \t \b  "
                .AsTrimmed(['\b', '\t', ' ', 'H', 'o'])
        );
    }

    [Fact]
    public void RemovesStringFromString()
    {
        Assert.Equal(
            "ello! \t",
            " \b   \t      Hello! \t \b   \t      H"
                .AsTrimmed(" \b   \t      H")
                .Str()
        );
    }

    [Fact]
    public void RemovesTextFromString()
    {
        AssertText.Equal(
            "ello! \t",
            " \b   \t      Hello! \t \b   \t      H"
                .AsTrimmed(" \b   \t      H").Str()
        );
    }

    [Fact]
    public void RemovesStringFromText()
    {
        AssertText.Equal(
            "ello! \t",
            " \b   \t      Hello! \t \b   \t      H"
                .AsTrimmed(" \b   \t      H")
        );
    }

    [Fact]
    public void RemovesTextFromText()
    {
        AssertText.Equal(
            "ello! \t",
            " \b   \t      Hello! \t \b   \t      H"
                .AsTrimmed(" \b   \t      H")
        );
    }

    [Fact]
    public void RemovesMultipleTextOccurenceFromText()
    {
        AssertText.Equal(
            "World ",
            "Hello Hello World Hello "
                .AsTrimmed("Hello ")
        );
    }
}
