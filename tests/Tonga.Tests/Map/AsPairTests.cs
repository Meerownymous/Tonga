using System;
using Tonga.Map;
using Xunit;

namespace Tonga.Tests.Map;

public sealed class AsPairTests
{
    [Fact]
    public void SensesChanges()
    {
        var pair = 1.AsPair(() => Guid.NewGuid().ToString());
        Assert.NotEqual(pair.Value(), pair.Value());
    }

    [Fact]
    public void BuildsValue()
    {
        Assert.Equal(
            "value",
            "key".AsPair(() => "value")
                .Value()
        );
    }

    [Fact]
    public void BuildsValueLazily()
    {
        Assert.Equal(
            "key",
            new AsPair<string, int>("key", () => throw new ApplicationException()).Key()
        );
    }

    [Fact]
    public void KnowsAboutBeingLazy()
    {
        Assert.True("2".AsPair(() => "4").IsLazy());
    }

    [Fact]
    public void KnowsAboutBeingNotLazy()
    {
        Assert.False(
            ("2","4")
                .AsPair()
                .IsLazy()
        );
    }
}
