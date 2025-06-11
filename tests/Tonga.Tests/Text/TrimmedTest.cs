using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text;

public sealed class TrimmedTest
{
    [Fact]
    public void TrimsWhitespaceEscapeSequences()
    {
        Assert.Equal(
            string.Empty,
            "   \b \f \n \r \t \v   "
                .AsText()
                .AsTrimmed()
                .Str()
        );
    }

    [Fact]
    public void TrimsString()
    {
        Assert.Equal(
            "Hello!",
            " \b   \t      Hello! \t \b  "
                .AsTrimmed()
                .Str()
        );
    }

    [Fact]
    public void TrimsText()
    {
        Assert.Equal(
            "Hello!",
            " \b   \t      Hello! \t \b  "
                .AsText()
                .AsTrimmed()
                .Str()
        );
    }

    [Fact]
    public void TrimsStringWithCharArray()
    {
        Assert.Equal(
            "ello!",
            " \b   \t      Hello! \t \b  "
                .AsTrimmed(['\b', '\t', ' ', 'H', 'o'])
                .Str()
        );
    }

    [Fact]
    public void TrimsTextWithCharArray()
    {
        Assert.Equal(
            "ello!",
            " \b   \t      Hello! \t \b  "
                .AsText()
                .AsTrimmed(['\b', '\t', ' ', 'H', 'o'])
                .Str()
        );
    }

    [Fact]
    public void TrimsTextByChars()
    {
        Assert.Equal(
            "ello!",
            " \b   \t      Hello! \t \b  ".AsText()
                .AsTrimmed(() => ['\b', '\t', ' ', 'H', 'o'])
            .Str()
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
        Assert.Equal(
            "ello! \t",
            " \b   \t      Hello! \t \b   \t      H"
                .AsText()
                .AsTrimmed(" \b   \t      H").Str()
        );
    }

    [Fact]
    public void RemovesStringFromText()
    {
        Assert.Equal(
            "ello! \t",
            " \b   \t      Hello! \t \b   \t      H"
                .AsTrimmed(" \b   \t      H".AsText())
                .Str()
        );
    }

    [Fact]
    public void RemovesTextFromText()
    {
        Assert.Equal(
            "ello! \t",
            new Trimmed(" \b   \t      Hello! \t \b   \t      H"
                .AsText()
                .AsTrimmed(" \b   \t      H".AsText()))
                .Str()
        );
    }

    [Fact]
    public void RemovesMultipleTextOccurenceFromText()
    {
        Assert.Equal(
            "World ",
            new Trimmed(
                "Hello Hello World Hello "
                    .AsText()
                    .AsTrimmed("Hello ".AsText())
            ).Str()
        );
    }
}
