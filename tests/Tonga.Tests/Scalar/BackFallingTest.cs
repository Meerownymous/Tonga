using System;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Scalar;

public class BackFallingTest
{
    [Fact]
    public void GivesFallback()
    {
        var fbk = "strong string";

        Assert.True(
            new AsScalar<string>(
                () => throw new Exception("NO STRINGS ATTACHED HAHAHA")
            ).AsBackFalling(fbk)
            .Value() == fbk
        );
    }

    [Fact]
    public void GivesFallbackByFunc()
    {
        var fbk = "strong string";

        Assert.Equal(
            fbk,
            new AsScalar<string>(
                () => throw new Exception("NO STRINGS ATTACHED HAHAHA")
            ).AsBackFalling(() => fbk)
            .Value()
        );
    }

    [Fact]
    public void InjectsException()
    {
        var notAmused = new Exception("All is broken :(");

        Assert.Equal(
            notAmused.Message,
            new AsScalar<string>(() => throw notAmused)
                .AsBackFalling(ex => ex.Message)
                .Value()
        );
    }
}
