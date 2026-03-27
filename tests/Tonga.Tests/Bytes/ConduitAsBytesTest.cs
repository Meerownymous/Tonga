using System;
using System.Text;
using Tonga.Bytes;
using Tonga.Enumerable;
using Tonga.IO;
using Tonga.Tests.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Bytes;

public sealed class ConduitAsBytesTest
{

    [Fact]
    public void ReadsLargeInMemoryContent()
    {
        int multiplier = 5_000;
        String text = "1234567890";
        Assert.Equal(
            text.Length * multiplier,
            new ConduitAsBytes(
                new Joined(" ", text.AsEndless()
                    .AsHead(multiplier)).Str()
            ).Raw().Length
        );
    }

    [Fact]
    public void ReadsLargeContent()
    {
        int size = 100_000;
        using var slow = new SlowInputStream(size);
        Assert.Equal(
            size,
            new ConduitAsBytes(slow).Raw().Length
        );
    }

    [Fact]
    public void ReadsInputIntoBytes()
    {
        ConduitMorph content = "Hello, друг!";
        AssertText.Equal(
            content,
            Encoding.UTF8.GetString(
                new ConduitAsBytes(content).Raw()
            )
        );
    }

    [Fact]
    public void ReadsInputIntoBytesWithSmallBuffer()
    {
        var content = "Hello, товарищ!";
        Assert.Equal(
            content,
            Encoding.UTF8.GetString(
                new ConduitAsBytes(content, 2).Raw()
            )
        );
    }
}
