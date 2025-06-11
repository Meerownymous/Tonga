using Tonga.Map;
using Xunit;

namespace Tonga.Tests.Map;

public sealed class StickyTests
{
    [Fact]
    public void RetrievesValueFromSource()
    {
        Assert.Equal(
            "one",
            (1, "one")
                .AsMap()
                .AsSticky()[1]
        );
    }

    [Fact]
    public void RemembersValue()
    {
        var map =
            1.AsOneTimePair(() => "one")
                .AsMap()
                .AsSticky();

        _ = map[1];

        Assert.Equal("one", map[1]);
    }
}
