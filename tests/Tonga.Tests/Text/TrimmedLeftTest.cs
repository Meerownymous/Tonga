using Tonga.Scalar;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text;

public sealed class TrimmedLeftTest
{
    [Fact]
    public void TrimsWhitespaceEscapeSequences()
    {
        Assert.True(
            new TrimmedLeft(AsText._("   \b \f \n \r \t \v   ")).Str() == string.Empty
        );
    }

    [Fact]
    public void TrimsString()
    {
        Assert.Equal(
            "Hello! \t \b  ",
            new TrimmedLeft(
                " \b   \t      Hello! \t \b  "
            ).Str()
        );
    }

    [Fact]
    public void TrimsText()
    {
        Assert.Equal(
            "Hello! \t \b  ",
            new TrimmedLeft(
                AsText._(" \b   \t      Hello! \t \b  ")
            ).Str()
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
            ).Str()
        );
    }

    [Fact]
    public void TrimsTextWithCharArray()
    {
        Assert.Equal(
            "ello! \t \b  ",
            new TrimmedLeft(
                AsText._(" \b   \t      Hello! \t \b  "),
                new char[] { '\b', '\t', ' ', 'H', 'o' }
            ).Str()
        );
    }

    [Fact]
    public void TrimsTextWithScalar()
    {
        Assert.Equal(
            "ello! \t \b  ",
            new TrimmedLeft(
                AsText._(" \b   \t      Hello! \t \b  "),
                AsScalar._(() => new char[] { '\b', '\t', ' ', 'H', 'o' })
            ).Str()
        );
    }

    [Fact]
    public void RemovesStringFromString()
    {
        Assert.Equal(
            "ello! \t \b   \t      H",
            new TrimmedLeft(
                " \b   \t      Hello! \t \b   \t      H", " \b   \t      H"
            ).Str()
        );
    }

    [Fact]
    public void RemovesTextFromString()
    {
        Assert.Equal(
            "ello! \t \b   \t      H",
            new TrimmedLeft(AsText._(" \b   \t      Hello! \t \b   \t      H"), " \b   \t      H").Str()
        );
    }

    [Fact]
    public void RemovesStringFromText()
    {
        Assert.Equal(
            "ello! \t \b   \t      H",
            new TrimmedLeft(" \b   \t      Hello! \t \b   \t      H", AsText._(" \b   \t      H")).Str()
        );
    }

    [Fact]
    public void RemovesTextFromText()
    {
        Assert.Equal(
            "ello! \t \b   \t      H",
            new TrimmedLeft(
                AsText._(" \b   \t      Hello! \t \b   \t      H"),
                AsText._(" \b   \t      H")
            ).Str()
        );
    }
}
