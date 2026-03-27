using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO;

public sealed class HeadSliceTest
{
    [Fact]
    void ReadsHeadOfLongerInput()
    {
        AssertText.Equal(
            "reads",
            new HeadSlice("readsHeadOfLongInput", 5)
        );
    }



    [Fact]
    void ReadsEmptyHeadOfInput()
    {
        AssertText.Contains(
            "",
            new HeadSlice("readsEmptyHeadOfInput", 0)
        );
    }

    [Fact]
    void ReadsHeadOfShorterInput()
    {
        var input = "readsHeadOfShorterInput";
        AssertText.Contains(
            input,
            new HeadSlice(input, 35)
        );
    }
}
