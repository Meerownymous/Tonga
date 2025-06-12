using System.IO;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO;

public sealed class StreamHeadTest
{
    [Fact]
    void IsSkippingLessThanTotal()
    {
        var stream =
            "testSkippingLessThanTotal"
                .AsStream()
                .AsHead(5);

        var skipped = stream.Seek(3L, SeekOrigin.Begin);

        Assert.Equal(3L, skipped);

        Assert.Contains(
            "tS",
            stream.AsText().Str()
        );
    }

    [Fact]
    void IsSkippingMoreThanTotal()
    {
        var stream =
            "testSkippingMoreThanTotal"
                .AsStream()
                .AsHead(5);

        var skipped = stream.Seek(7L, SeekOrigin.Begin);

        Assert.Equal(5L, skipped);

        var input = stream.AsText().Str();

        Assert.Equal("", input);
    }
}
