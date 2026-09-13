using System;
using System.Threading.Tasks;
using Tonga.AsyncEnumerable;
using Xunit;

namespace Tonga.Tests.AsyncEnumerable;

public sealed class TerminalTests
{
    [Fact]
    public async Task FirstOneDeliversTheFirstItem() =>
        Assert.Equal(1, await (1, 2, 3).AsAsyncEnumerable().FirstOne().Value());

    [Fact]
    public async Task FirstOneReadsNoFurtherThanTheMatch()
    {
        var read = 0;
        Assert.Equal(
            3,
            await (1, 2, 3, 4, 5)
                .AsAsyncEnumerable()
                .OnEach((int _) => read++)
                .FirstOne(item => item == 3)
                .Value()
        );
        Assert.Equal(3, read);
    }

    [Fact]
    public async Task FirstOneFallsBackWhenEmpty() =>
        Assert.Equal(42, await new Empty<int>().FirstOne(42).Value());

    [Fact]
    public async Task FirstOneThrowsWhenEmpty() =>
        await Assert.ThrowsAsync<ArgumentException>(
            async () => await new Empty<int>().FirstOne().Value()
        );

    [Fact]
    public async Task LastOneDeliversTheLastItem() =>
        Assert.Equal(3, await (1, 2, 3).AsAsyncEnumerable().LastOne().Value());

    [Fact]
    public async Task LastOneFallsBackWhenEmpty() =>
        Assert.Equal(42, await new Empty<int>().LastOne(42).Value());

    [Fact]
    public async Task ItemAtDeliversThePosition() =>
        Assert.Equal(3, await (1, 2, 3, 4).AsAsyncEnumerable().ItemAt(2).Value());

    [Fact]
    public async Task ItemAtReadsNoFurtherThanThePosition()
    {
        var read = 0;
        await (1, 2, 3, 4, 5).AsAsyncEnumerable().OnEach((int _) => read++).ItemAt(1).Value();
        Assert.Equal(2, read);
    }

    [Fact]
    public async Task ItemAtFallsBackBeyondTheEnd() =>
        Assert.Equal(42, await (1, 2).AsAsyncEnumerable().ItemAt(9, 42).Value());

    [Fact]
    public async Task ItemAtRejectsNegativePosition() =>
        await Assert.ThrowsAsync<ArgumentException>(
            async () => await (1, 2).AsAsyncEnumerable().ItemAt(-1).Value()
        );

    [Fact]
    public async Task LengthCounts() =>
        Assert.Equal(4L, await (1, 2, 3, 4).AsAsyncEnumerable().Length().Value());

    [Fact]
    public async Task LengthOfEmptyIsZero() =>
        Assert.Equal(0L, await new Empty<int>().Length().Value());

    [Fact]
    public async Task MaximumFindsTheGreatest() =>
        Assert.Equal(9, await (3, 9, 1).AsAsyncEnumerable().Maximum().Value());

    [Fact]
    public async Task MinimumFindsTheSmallest() =>
        Assert.Equal(1, await (3, 9, 1).AsAsyncEnumerable().Minimum().Value());

    [Fact]
    public async Task MaximumRejectsEmptySource() =>
        await Assert.ThrowsAsync<ArgumentException>(
            async () => await new Empty<int>().Maximum().Value()
        );

    [Fact]
    public async Task ReducedFoldsTheItems() =>
        Assert.Equal(10, await (1, 2, 3, 4).AsAsyncEnumerable().AsReduced((a, b) => a + b).Value());

    [Fact]
    public async Task ReducedRejectsEmptySource() =>
        await Assert.ThrowsAsync<ArgumentException>(
            async () => await new Empty<int>().AsReduced((a, b) => a + b).Value()
        );

    [Fact]
    public async Task SiblingFindsTheNextOne() =>
        Assert.Equal(3, await (1, 2, 3, 4).AsAsyncEnumerable().Sibling(2).Value());

    [Fact]
    public async Task SiblingFindsThePreviousOne() =>
        Assert.Equal(1, await (1, 2, 3, 4).AsAsyncEnumerable().Sibling(2, -1).Value());

    [Fact]
    public async Task SiblingFallsBackWhenThereIsNone() =>
        Assert.Equal(42, await (1, 2).AsAsyncEnumerable().Sibling(2, 1, 42).Value());

    [Fact]
    public async Task BuildingATerminalReadsNothing()
    {
        var read = 0;
        var chain = (1, 2, 3).AsAsyncEnumerable().OnEach((int _) => read++).Length();

        Assert.Equal(0, read);
        await chain.Value();
        Assert.Equal(3, read);
    }
}
