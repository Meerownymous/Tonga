using System;
using Tonga.Optional;
using Xunit;

namespace Tonga.Optional.Tests;

public sealed class OptEmptyTests
{
    [Fact]
    public void RejectsValueRequest()
    {
        Assert.Throws<InvalidOperationException>(
            () => new OptEmpty<int>().Value()
        );
    }

    [Fact]
    public void PerformsNotAction()
    {
        bool performed = false;
        new OptEmpty<int>().IfNot(() => performed = true);
        Assert.True(performed);
    }

    [Fact]
    public void DoesNotPerformHasAction()
    {
        bool performed = false;
        new OptEmpty<int>().IfHas((v) => performed = true);
        Assert.False(performed);
    }
}
