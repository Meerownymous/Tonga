using Tonga.Enumerable;
using Tonga.List;
using Xunit;

namespace Tonga.Tests.List;

public sealed class EconomicTest
{
    [Fact]
    public void AdvancesOnlyWhenAsked()
    {
        var advances = 0;
        var sticky =
            ("one", "two", "three").AsEnumerable()
                .OnEach(() => advances++)
                .AsList()
                .AsSticky();
        sticky.GetEnumerator().MoveNext();

        Assert.Equal(1, advances);
    }

    [Fact]
    public void AdvancesOnlyToIndex()
    {
        var advances = 0;
        var sticky =
            ("one", "two", "three")
                .AsEnumerable()
                .OnEach(() => advances++)
                .AsList()
                .AsSticky();
        _ = sticky[1];

        Assert.Equal(2, advances);
    }

    [Fact]
    public void AdvancesOnlyToItemForContains()
    {
        var advances = 0;
        var sticky =
            ("one", "two", "three")
                .AsEnumerable()
                .OnEach(() => advances++)
                .AsList()
                .AsSticky();
        _ = sticky.Contains("two");

        Assert.Equal(2, advances);
    }

    [Fact]
    public void AdvancesAllForCount()
    {
        var advances = 0;
        var sticky =
            ("one", "two", "three")
                .AsEnumerable()
                .OnEach(() => advances++)
                .AsList()
                .AsSticky();
        _ = sticky.Count;
        Assert.Equal(3, advances);
    }

    [Fact]
    public void MemoizesSeenItems()
    {
        var advances = 0;
        var sticky =
            ("one", "two", "three")
                .AsEnumerable()
                .OnEach(() => advances++)
                .AsList()
                .AsSticky();
        _ = sticky.Count;
        _ = sticky[2];

        Assert.Equal(3, advances);
    }
}
