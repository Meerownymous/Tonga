using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text;

public sealed class SubTextText
{
    [Fact]
    public void CutString()
    {
        Assert.Equal(
            "the_end",
            "this_is:the_end"
                .AsSubText(8)
                .Str()
        );
    }

    [Fact]
    public void CutStringwithLength()
    {
        Assert.Equal(
            "the",
            "this_is:the_end"
                .AsSubText(8,3)
                .Str()
        );
    }

    [Fact]
    public void CutIText()
    {
        Assert.Equal(
            "the_end",
            "this_is:the_end".AsText()
                .AsSubText(8)
                .Str()
        );
    }

    [Fact]
    public void CutITextwithLength()
    {
        Assert.Equal(
            "the",
                "this_is:the_end".AsText()
                    .AsSubText(8, 3).Str()
        );
    }
}
