using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tonga.AsyncEnumerable;
using Xunit;

namespace Tonga.Tests.AsyncEnumerable;

public sealed class DecoratorTests
{
    [Fact]
    public async Task SkippedDropsTheFirstItems() =>
        Assert.Equal(
            [3, 4],
            await (1, 2, 3, 4).AsAsyncEnumerable().AsSkipped(2).AsList().Value()
        );

    [Fact]
    public async Task JoinedConcatenates() =>
        Assert.Equal(
            [1, 2, 3, 4],
            await (1, 2).AsAsyncEnumerable().AsJoined((3, 4).AsAsyncEnumerable()).AsList().Value()
        );

    [Fact]
    public async Task JoinedAppendsSingleItems() =>
        Assert.Equal(
            [1, 2, 3],
            await (1, 2).AsAsyncEnumerable().AsJoined(3).AsList().Value()
        );

    [Fact]
    public async Task JoinedReadsTheSecondSourceOnlyWhenNeeded()
    {
        var read = 0;
        Assert.Equal(
            [1],
            await (1, 2)
                .AsAsyncEnumerable()
                .AsJoined((3, 4).AsAsyncEnumerable().OnEach((int _) => read++))
                .AsHead(1)
                .AsList()
                .Value()
        );
        Assert.Equal(0, read);
    }

    [Fact]
    public async Task ReversedTurnsTheOrderAround() =>
        Assert.Equal(
            [3, 2, 1],
            await (1, 2, 3).AsAsyncEnumerable().AsReversed().AsList().Value()
        );

    [Fact]
    public async Task SortedOrdersByDefault() =>
        Assert.Equal(
            [1, 2, 3],
            await (3, 1, 2).AsAsyncEnumerable().AsSorted().AsList().Value()
        );

    [Fact]
    public async Task SortedByOrdersByKey() =>
        Assert.Equal(
            ["a", "bb", "ccc"],
            await ("ccc", "a", "bb").AsAsyncEnumerable().AsSortedBy(item => item.Length).AsList().Value()
        );

    [Fact]
    public async Task DistinctRemovesDuplicates() =>
        Assert.Equal(
            [1, 2, 3],
            await (1, 2, 1, 3, 2).AsAsyncEnumerable().AsDistinct().AsList().Value()
        );

    [Fact]
    public async Task UnionMergesWithoutDuplicates() =>
        Assert.Equal(
            [1, 2, 3],
            await (1, 2).AsAsyncEnumerable().AsUnion((2, 3).AsAsyncEnumerable()).AsList().Value()
        );

    [Fact]
    public async Task IntersectionKeepsWhatIsInBoth() =>
        Assert.Equal(
            [2, 3],
            await (1, 2, 3).AsAsyncEnumerable().AsIntersection((2, 3, 4).AsAsyncEnumerable()).AsList().Value()
        );

    [Fact]
    public async Task IntersectionYieldsEachItemOnce() =>
        Assert.Equal(
            [2],
            await (2, 2, 2).AsAsyncEnumerable().AsIntersection(2.AsAsyncSingle()).AsList().Value()
        );

    [Fact]
    public async Task DivergencyKeepsWhatIsInOnlyOneSource() =>
        Assert.Equal(
            [1, 4],
            await new[] { (1, 2, 3).AsAsyncEnumerable(), (2, 3, 4).AsAsyncEnumerable() }
                .AsDivergency()
                .AsList()
                .Value()
        );

    [Fact]
    public async Task ReplacedSwapsMatchingItems() =>
        Assert.Equal(
            [1, 0, 3, 0],
            await (1, 2, 3, 4).AsAsyncEnumerable().AsReplaced(item => item % 2 == 0, 0).AsList().Value()
        );

    [Fact]
    public async Task ReplacedSwapsByIndex() =>
        Assert.Equal(
            [1, 9, 3],
            await (1, 2, 3).AsAsyncEnumerable().AsReplaced(1, 9).AsList().Value()
        );

    [Fact]
    public async Task CycledStartsOver() =>
        Assert.Equal(
            [1, 2, 3, 1, 2],
            await (1, 2, 3).AsAsyncEnumerable().AsCycled().AsHead(5).AsList().Value()
        );

    [Fact]
    public async Task RepeatedRepeatsTheItem() =>
        Assert.Equal(
            ["x", "x", "x"],
            await "x".AsAsyncRepeated(3).AsList().Value()
        );

    [Fact]
    public async Task EndlessNeverRunsOut() =>
        Assert.Equal(
            [7, 7, 7, 7],
            await 7.AsAsyncEndless().AsHead(4).AsList().Value()
        );

    [Fact]
    public async Task SingleHoldsOneItem() =>
        Assert.Equal([5], await 5.AsAsyncSingle().AsList().Value());

    [Fact]
    public async Task EmptyHoldsNothing() =>
        Assert.Empty(await new Empty<int>().AsList().Value());

    [Fact]
    public async Task PartitionedCutsIntoChunks()
    {
        var chunks = new List<IList<int>>();
        await foreach (var partition in (1, 2, 3, 4, 5).AsAsyncEnumerable().AsPartitioned(2))
        {
            chunks.Add(await partition.AsList().Value());
        }

        Assert.Equal(3, chunks.Count);
        Assert.Equal([1, 2], chunks[0]);
        Assert.Equal([3, 4], chunks[1]);
        Assert.Equal([5], chunks[2]);
    }

    [Fact]
    public async Task ConditionalPicksTheMatchingSource() =>
        Assert.Equal(
            [1, 2],
            await (1, 2)
                .AsAsyncEnumerable()
                .AsConditional((3, 4).AsAsyncEnumerable(), true)
                .AsList()
                .Value()
        );

    [Fact]
    public async Task ConditionalPicksTheOtherSource() =>
        Assert.Equal(
            [3, 4],
            await (1, 2)
                .AsAsyncEnumerable()
                .AsConditional((3, 4).AsAsyncEnumerable(), false)
                .AsList()
                .Value()
        );

    [Fact]
    public async Task BackFallingUsesTheFallbackWhenEmpty() =>
        Assert.Equal(
            [9],
            await new Empty<int>().AsBackFalling(9.AsAsyncSingle()).AsList().Value()
        );

    [Fact]
    public async Task BackFallingKeepsTheOriginWhenFilled() =>
        Assert.Equal(
            [1, 2],
            await (1, 2).AsAsyncEnumerable().AsBackFalling(9.AsAsyncSingle()).AsList().Value()
        );

    [Fact]
    public async Task AssertNotEmptyPassesItemsThrough() =>
        Assert.Equal(
            [1, 2],
            await (1, 2).AsAsyncEnumerable().AssertNotEmpty().AsList().Value()
        );

    [Fact]
    public async Task AssertNotEmptyThrowsOnEmpty() =>
        await Assert.ThrowsAsync<Exception>(
            async () => await new Empty<int>().AssertNotEmpty().AsList().Value()
        );

    [Fact]
    public async Task OnEachCountsFromZero()
    {
        var indexes = new List<int>();
        await ("a", "b", "c").AsAsyncEnumerable().OnEach((string _, int index) => indexes.Add(index)).AsList().Value();
        Assert.Equal([0, 1, 2], indexes);
    }

    [Fact]
    public async Task LinesSplitsAText() =>
        Assert.Equal(
            ["a", "b"],
            await new Lines("a\nb").AsList().Value()
        );

    [Fact]
    public async Task LinesSkipsEmptyOnesWhenAsked() =>
        Assert.Equal(
            ["a", "b"],
            await new Lines("a\n\nb", true).AsList().Value()
        );
}
