using System;
using Tonga.Map;
using Xunit;

namespace Tonga.Tests.Map;

public sealed class OneTimePairTests
{
    [Fact]
    public void AllowsFirstRetrieval()
    {
        Assert.Equal(
            "one",
            1.AsPair(() => "one")
            .AsOneTimePair()
            .Value()
        );
    }

    [Fact]
    public void RejectsSecondRetrieval()
    {
        var pair =
            1.AsPair(() => "one").AsOneTimePair();

        pair.Value();

        Assert.Throws<InvalidOperationException>(pair.Value);
    }
}
