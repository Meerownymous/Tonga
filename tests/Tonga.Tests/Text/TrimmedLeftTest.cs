using Tonga.Scalar;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text;

public sealed class TrimmedLeftTest
{
    [Fact]
    public void TrimsWhitespaceEscapeSequences()
    {
        Assert.Equal(
            string.Empty,
            "   \b \f \n \r \t \v   ".AsTrimmedLeft().Str()
        );
    }

    [Fact]
    public void TrimsString()
    {
        Assert.Equal(
            "Hello! \t \b  ",
            " \b   \t      Hello! \t \b  ".AsTrimmedLeft().Str()
        );
    }

    [Fact]
    public void TrimsText()
    {
        Assert.Equal(
            "Hello! \t \b  ",
            " \b   \t      Hello! \t \b  ".AsTrimmedLeft().Str()
        );
    }

    [Fact]
    public void TrimsStringWithCharArray()
    {
        Assert.Equal(
            "ello! \t \b  ",
            " \b   \t      Hello! \t \b  "
                .AsTrimmedLeft(['\b', '\t', ' ', 'H', 'o'])
                .Str()
        );
    }

    [Fact]
    public void TrimsTextWithCharArray()
    {
        Assert.Equal(
            "ello! \t \b  ",
            " \b   \t      Hello! \t \b  "
                .AsTrimmedLeft(['\b', '\t', ' ', 'H', 'o'])
                .Str()
        );
    }

    [Fact]
    public void RemovesStringFromString()
    {
        Assert.Equal(
            "ello! \t \b   \t      H",
            " \b   \t      Hello! \t \b   \t      H"
                .AsTrimmedLeft(" \b   \t      H")
                .Str()
        );
    }

    [Fact]
    public void RemovesTextFromString()
    {
        Assert.Equal(
            "ello! \t \b   \t      H",
            " \b   \t      Hello! \t \b   \t      H"
                .AsTrimmedLeft(" \b   \t      H")
                .Str()
        );
    }

    [Fact]
    public void RemovesStringFromText()
    {
        Assert.Equal(
            "ello! \t \b   \t      H",
            " \b   \t      Hello! \t \b   \t      H"
                .AsTrimmedLeft(" \b   \t      H")
                .Str()
        );
    }

    [Fact]
    public void RemovesTextFromText()
    {
        Assert.Equal(
            "ello! \t \b   \t      H",
            " \b   \t      Hello! \t \b   \t      H".AsText()
                .AsTrimmedLeft(" \b   \t      H".AsText())
                .Str()
        );
    }
}
