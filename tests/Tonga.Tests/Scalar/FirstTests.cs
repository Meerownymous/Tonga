using System;
using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Scalar;

public sealed class FirstTests
{
    [Fact]
    public void ThrowsCustomException()
    {
        Assert.Throws<InvalidOperationException>(() =>
            new None<string>()
                .First(new InvalidOperationException())
                .Value()
        );
    }

    [Fact]
    public void ReturnsFallBack()
    {
        Assert.Equal(
            "gotcha",
            new None<string>()
                .First("gotcha")
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
                .First(item => item.StartsWith("M"))
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
                .First()
                .Value()
        );
    }
}
