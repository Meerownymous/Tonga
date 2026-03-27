using System;
using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Scalar;

public sealed class FirstOneTests
{
    [Fact]
    public void ThrowsCustomException()
    {
        Assert.Throws<InvalidOperationException>(() =>
            new Empty<string>()
                .FirstOne(new InvalidOperationException())
                .Value()
        );
    }

    [Fact]
    public void ReturnsFallBack()
    {
        Assert.Equal(
            "gotcha",
            new Empty<string>()
                .FirstOne("gotcha")
                .Value()
        );
    }

    [Fact]
    public void ReturnsFirstMatch()
    {
        Assert.Equal(
            "Max",
            ("hallo", "ich", "heisse", "Max")
                .AsEnumerable()
                .FirstOne(item => item.StartsWith("M"))
                .Value()
        );
    }

    [Fact]
    public void ReturnsFirstValue()
    {
        Assert.Equal(
            "hallo",
            ("hallo", "ich", "heisse", "Max")
                .AsEnumerable()
                .FirstOne()
                .Value()
        );
    }
}
