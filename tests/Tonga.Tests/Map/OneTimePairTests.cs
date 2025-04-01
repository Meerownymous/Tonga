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
            OneTimePair._(
                AsPair._(1, () => "one")
            ).Value()
        );
    }

    [Fact]
    public void RejectsSecondRetrieval()
    {
        var pair =
            OneTimePair._(
                AsPair._(1, () => "one")
            );

        pair.Value();

        Assert.Throws<InvalidOperationException>(pair.Value);
    }
}
