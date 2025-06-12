using System;
using Tonga.Fact;
using Tonga.Tap;
using Xunit;

namespace Tonga.Tests.Tap;

public sealed class IfYesTests
{
    [Fact]
    public void ActsIfYes()
    {
        bool acted = false;
        new IfYes(
            () => true,
            () => { acted = true; }
        ).Trigger();

        Assert.True(acted);
    }

    [Fact]
    public void NoActIfNo()
    {
        bool acted = false;
        new IfYes(
            () => false,
            () => { acted = true; }
        ).Trigger();

        Assert.False(acted);
    }
}
