using Tonga.Scalar;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text;

public sealed class TrimmedRightTest
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
            " \b   \t      Hello!",
            " \b   \t      Hello! \t \b  ".AsTrimmedRight()
        );
    }

    [Fact]
    public void TrimsText()
    {
        AssertText.Equal(
            " \b   \t      Hello!",
            " \b   \t      Hello! \t \b  ".AsTrimmedRight()
        );
    }

    [Fact]
    public void TrimsStringWithCharArray()
    {
        AssertText.Equal(
            " \b   \t      Hell",
            " \b   \t      Hello! \t \b  "
                .AsTrimmedRight(['\b', '\t', ' ', 'H', '!', 'o'])
        );
    }

    [Fact]
    public void TrimsTextWithCharArray()
    {
        AssertText.Equal(
            " \b   \t      Hell",
            " \b   \t      Hello! \t \b  "
                .AsTrimmedRight(['\b', '\t', ' ', 'H', '!', 'o'])
        );
    }

    [Fact]
    public void RemovesStringFromString()
    {
        AssertText.Equal(
            " \b   \t      Hello! \t",
            " \b   \t      Hello! \t \b   \t      H"
                .AsTrimmedRight(" \b   \t      H")
        );
    }

    [Fact]
    public void RemovesTextFromString()
    {
        AssertText.Equal(
            " \b   \t      Hello! \t",
            " \b   \t      Hello! \t \b   \t      H"
                .AsTrimmedRight(" \b   \t      H")
        );
    }

    [Fact]
    public void RemovesStringFromText()
    {
        AssertText.Equal(
            " \b   \t      Hello! \t",
            " \b   \t      Hello! \t \b   \t      H"
                .AsTrimmedRight(" \b   \t      H")
        );
    }

    [Fact]
    public void RemovesTextFromText()
    {
        AssertText.Equal(
            " \b   \t      Hello! \t",
            " \b   \t      Hello! \t \b   \t      H"
                .AsTrimmedRight(" \b   \t      H")
        );
    }
}
