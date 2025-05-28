using Tonga.Map;
using Xunit;

namespace Tonga.Tests.Map;

public sealed class EconomicTests
{
    [Fact]
    public void RetrievesValueFromSource()
    {
        Assert.Equal(
            "one",
            Compiled<,>._(
                AsMap._(
                    AsPair._(1, "one")
                )
            )[1]
        );
    }

    [Fact]
    public void RemembersValue()
    {
        var map =
            Compiled<,>._(
                AsMap._(
                    OneTimePair._(
                        AsPair._(1, "one")
                    )
                )
            );

        _ = map[1];

        Assert.Equal(
            "one",
            map[1]
        );
    }
}
