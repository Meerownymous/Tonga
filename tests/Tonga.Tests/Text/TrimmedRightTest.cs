using Tonga.Scalar;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text;

public sealed class TrimmedRightTest
{
    [Fact]
    public void TrimsWhitespaceEscapeSequences()
    {
        Assert.Equal(
            string.Empty,
            "   \b \f \n \r \t \v   ".AsText()
                .AsTrimmedRight()
                .Str()
        );
    }

    [Fact]
    public void TrimsString()
    {
        Assert.Equal(
            " \b   \t      Hello!",
            " \b   \t      Hello! \t \b  ".AsTrimmedRight().Str()
        );
    }

    [Fact]
    public void TrimsText()
    {
        Assert.Equal(
            " \b   \t      Hello!",
            " \b   \t      Hello! \t \b  ".AsText().AsTrimmedRight().Str()
        );
    }

    [Fact]
    public void TrimsStringWithCharArray()
    {
        Assert.Equal(
            " \b   \t      Hell",
            " \b   \t      Hello! \t \b  "
                .AsTrimmedRight(['\b', '\t', ' ', 'H', '!', 'o'])
                .Str()
        );
    }

    [Fact]
    public void TrimsTextWithCharArray()
    {
        Assert.Equal(
            " \b   \t      Hell",
            " \b   \t      Hello! \t \b  ".AsText()
                .AsTrimmedRight(['\b', '\t', ' ', 'H', '!', 'o'])
                .Str()
        );
    }

    [Fact]
    public void TrimsTextWithScalar()
    {
        Assert.Equal(
            " \b   \t      Hell",
            " \b   \t      Hello! \t \b  ".AsText()
                .AsTrimmedRight(() => ['\b', '\t', ' ', 'H', '!', 'o'])
                .Str()
        );
    }

    [Fact]
    public void RemovesStringFromString()
    {
        Assert.Equal(
            " \b   \t      Hello! \t",
            " \b   \t      Hello! \t \b   \t      H"
                .AsTrimmedRight(" \b   \t      H")
                .Str()
        );
    }

    [Fact]
    public void RemovesTextFromString()
    {
        Assert.Equal(
            " \b   \t      Hello! \t",
            " \b   \t      Hello! \t \b   \t      H"
                .AsText()
                .AsTrimmedRight(" \b   \t      H").Str()
        );
    }

    [Fact]
    public void RemovesStringFromText()
    {
        Assert.Equal(
            " \b   \t      Hello! \t",
            " \b   \t      Hello! \t \b   \t      H"
                .AsTrimmedRight(" \b   \t      H".AsText())
                .Str()
        );
    }

    [Fact]
    public void RemovesTextFromText()
    {
        Assert.Equal(
            " \b   \t      Hello! \t",
            " \b   \t      Hello! \t \b   \t      H"
                .AsTrimmedRight(" \b   \t      H".AsText())
                .Str()
        );
    }
}
