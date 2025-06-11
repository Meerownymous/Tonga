using Tonga.Enumerable;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO;

public sealed class HeadTest
{
    [Fact]
    void ReadsHeadOfLongerInput()
    {
        Assert.Contains(
            "reads",
            new AsConduit("readsHeadOfLongInput")
                .AsHead(5)
                .AsText()
                .Str()
        );
    }

    [Fact]
    void ReadsOnlyLength()
    {
        Assert.Equal(
            5,
            new AsConduit("readsHeadOfLongInput")
                .AsHead(5)
                .AsText()
                .Str()
                .AsConduit()
                .Length()
                .Value()
        );
    }

    [Fact]
    void ReadsEmptyHeadOfInput()
    {
        Assert.Contains(
            "",
            new AsConduit("readsEmptyHeadOfInput")
                .AsHead(0)
                .AsText()
                .Str()
        );
    }

    [Fact]
    void ReadsHeadOfShorterInput()
    {
        var input = "readsHeadOfShorterInput";
        Assert.Contains(
            input,
            new AsConduit(input)
                .AsHead(35)
                .AsText()
                .Str()
        );
    }
}
