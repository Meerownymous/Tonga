using Tonga.Scalar;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text;

public sealed class TrimmedRightTest
{
    [Fact]
    public void TrimsWhitespaceEscapeSequences()
    {
        Assert.True(
            new TrimmedRight(
                AsText._("   \b \f \n \r \t \v   ")
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
                AsText._(" \b   \t      Hello! \t \b  ")
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
            new TrimmedRight(AsText._(" \b   \t      Hello! \t \b  "), new char[] { '\b', '\t', ' ', 'H', '!', 'o' }).AsString() == " \b   \t      Hell"
        );
    }

    [Fact]
    public void TrimsTextWithScalar()
    {
        Assert.True(
            new TrimmedRight(
                AsText._(" \b   \t      Hello! \t \b  "),
                AsScalar._(() => new char[] { '\b', '\t', ' ', 'H', '!', 'o' })
            ).AsString() == " \b   \t      Hell"
        );
    }

    [Fact]
    public void RemovesStringFromString()
    {
        Assert.Equal(
            " \b   \t      Hello! \t",
            new TrimmedRight(
                " \b   \t      Hello! \t \b   \t      H", " \b   \t      H"
            ).AsString()
        );
    }

    [Fact]
    public void RemovesTextFromString()
    {
        Assert.True(
            new TrimmedRight(
                AsText._(" \b   \t      Hello! \t \b   \t      H"), " \b   \t      H").AsString() == " \b   \t      Hello! \t"
        );
    }

    [Fact]
    public void RemovesStringFromText()
    {
        Assert.True(
            new TrimmedRight(" \b   \t      Hello! \t \b   \t      H",
                AsText._(" \b   \t      H")
            ).AsString() == " \b   \t      Hello! \t"
        );
    }

    [Fact]
    public void RemovesTextFromText()
    {
        Assert.True(
            new TrimmedRight(
                AsText._(" \b   \t      Hello! \t \b   \t      H"),
                AsText._(" \b   \t      H")
            ).AsString() == " \b   \t      Hello! \t"
        );
    }
}
