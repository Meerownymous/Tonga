using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text;

public sealed class SubTextText
{
    [Fact]
    public void CutsString()
    {
        AssertText.Equal(
            "the_end",
            new SubText("this_is:the_end", 8)
        );
    }

    [Fact]
    public void CutStringwithLength()
    {
        AssertText.Equal(
            "the",
            new SubText("this_is:the_end",8,3)
        );
    }

    [Fact]
    public void CutIText()
    {
        AssertText.Equal(
            "the_end",
            new SubText("this_is:the_end",8)
        );
    }

    [Fact]
    public void CutITextwithLength()
    {
        AssertText.Equal(
            "the",
            new SubText("this_is:the_end",8, 3)
        );
    }
}
