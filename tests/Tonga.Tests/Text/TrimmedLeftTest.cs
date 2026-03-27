using Tonga.Scalar;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text;

public sealed class TrimmedLeftTest
{
    [Fact]
    public void TrimsWhitespaceEscapeSequences()
    {
        AssertText.Equal(
            string.Empty,
            new TrimmedLeft("   \b \f \n \r \t \v   ")
        );
    }

    [Fact]
    public void TrimsString()
    {
        AssertText.Equal(
            "Hello! \t \b  ",
            new TrimmedLeft(" \b   \t      Hello! \t \b  ")
        );
    }

    [Fact]
    public void TrimsText()
    {
        AssertText.Equal(
            "Hello! \t \b  ",
            new TrimmedLeft(" \b   \t      Hello! \t \b  ")
        );
    }

    [Fact]
    public void TrimsStringWithCharArray()
    {
        AssertText.Equal(
            "ello! \t \b  ",
            new TrimmedLeft(" \b   \t      Hello! \t \b  ", ['\b', '\t', ' ', 'H', 'o'])
        );
    }

    [Fact]
    public void TrimsTextWithCharArray()
    {
        AssertText.Equal(
            "ello! \t \b  ",
            new TrimmedLeft(" \b   \t      Hello! \t \b  ",['\b', '\t', ' ', 'H', 'o'])
        );
    }

    [Fact]
    public void RemovesStringFromString()
    {
        AssertText.Equal(
            "ello! \t \b   \t      H",
            new TrimmedLeft(" \b   \t      Hello! \t \b   \t      H", " \b   \t      H")
        );
    }

    [Fact]
    public void RemovesTextFromString()
    {
        AssertText.Equal(
            "ello! \t \b   \t      H",
            new TrimmedLeft(" \b   \t      Hello! \t \b   \t      H"," \b   \t      H")
        );
    }

    [Fact]
    public void RemovesStringFromText()
    {
        AssertText.Equal(
            "ello! \t \b   \t      H",
            new TrimmedLeft(" \b   \t      Hello! \t \b   \t      H"," \b   \t      H")
        );
    }
}
