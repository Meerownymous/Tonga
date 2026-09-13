using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tonga.AsyncEnumerable;
using Tonga.Optional;
using Xunit;

namespace Tonga.Tests.AsyncEnumerable;

/// <summary>
/// Properties that the two-axis review of the async branch found broken.
/// </summary>
public sealed class ReviewFixTests
{
    [Fact]
    public async Task HasLessThanReadsNoMoreThanItNeeds()
    {
        var read = 0;
        Assert.False(
            await (1, 2, 3, 4, 5)
                .AsAsyncEnumerable()
                .OnEach((int _) => read++)
                .HasLessThan(3)
                .IsTrue()
        );
        Assert.Equal(3, read);
    }

    [Fact]
    public async Task HasLessThanStillAnswersTrue() =>
        Assert.True(await (1, 2).AsAsyncEnumerable().HasLessThan(3).IsTrue());

    [Fact]
    public async Task FirstOneDoesNotSwallowCancellation()
    {
        using var cancellation = new CancellationTokenSource();
        await cancellation.CancelAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            async () => await 1.AsAsyncEndless().FirstOne(42).Value(cancellation.Token)
        );
    }

    [Fact]
    public async Task ItemAtDoesNotSwallowCancellation()
    {
        using var cancellation = new CancellationTokenSource();
        await cancellation.CancelAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            async () => await 1.AsAsyncEndless().ItemAt(3, 42).Value(cancellation.Token)
        );
    }

    [Fact]
    public async Task ItemAtStillFallsBackOnOtherFailures() =>
        Assert.Equal(42, await (1, 2).AsAsyncEnumerable().ItemAt(9, 42).Value());

    [Fact]
    public async Task ABorrowedEnumeratorIsNotDisposed()
    {
        var enumerator = new TrackingEnumerator([1, 2]);

        Assert.Equal([1, 2], await new AsAsyncEnumerable<int>(enumerator).AsList().Value());
        Assert.False(enumerator.Disposed);
    }

    [Fact]
    public async Task AnOwnedEnumeratorIsDisposed()
    {
        var enumerator = new TrackingEnumerator([1, 2]);

        Assert.Equal(
            [1, 2],
            await new AsAsyncEnumerable<int>(_ => enumerator).AsList().Value()
        );
        Assert.True(enumerator.Disposed);
    }

    [Fact]
    public async Task StickyDisposesTheSourceOnceExhausted()
    {
        var enumerator = new TrackingEnumerator([1, 2]);
        var sticky = new Sticky<int>(_ => enumerator);

        Assert.Equal([1, 2], await sticky.AsList().Value());
        Assert.True(enumerator.Disposed);
        Assert.Equal([1, 2], await sticky.AsList().Value());
    }

    [Fact]
    public async Task CycledOverAnEmptySourceEnds() =>
        Assert.Empty(await new Empty<int>().AsCycled().AsHead(5).AsList().Value());

    [Fact]
    public async Task OptionalValueRunsTheRegisteredAction()
    {
        var seen = 0;
        await (7, 8).AsAsyncEnumerable().AsAsyncOptional().IfHas(item => seen = item).Value();
        Assert.Equal(7, seen);
    }

    [Fact]
    public async Task OptionalValueRunsTheMissingBranchBeforeThrowing()
    {
        var acted = false;
        await Assert.ThrowsAsync<InvalidOperationException>(
            async () => await new Empty<int>().AsAsyncOptional().IfNot(() => acted = true).Value()
        );
        Assert.True(acted);
    }

    [Fact]
    public async Task FullOptionalValueRunsTheRegisteredAction()
    {
        var seen = "";
        await new AsyncOptFull<string>("a").IfHas(item => seen = item).Value();
        Assert.Equal("a", seen);
    }

    [Fact]
    public async Task SyncOptionalBridgeIsLazy()
    {
        var asked = 0;
        var bridged = new AsyncOptSync<int>(() =>
        {
            asked++;
            return new OptFull<int>(1);
        });

        Assert.Equal(0, asked);
        Assert.Equal(1, await bridged.Value());
        Assert.Equal(1, asked);
    }

    [Fact]
    public async Task DistinctUsesTheDefaultComparerWhenNoneIsGiven() =>
        Assert.Equal(
            [1, 2, 3],
            await (1, 2, 1, 3, 2).AsAsyncEnumerable().AsDistinct().AsList().Value()
        );

    [Fact]
    public async Task DistinctStillHonoursACustomComparison() =>
        Assert.Equal(
            [1, 2],
            await new[] { (1, 2, 11, 12).AsAsyncEnumerable() }
                .AsDistinct((a, b) => a % 10 == b % 10)
                .AsList()
                .Value()
        );

    [Fact]
    public async Task RestoredOverloadsWork()
    {
        Assert.Equal(9, await new Maximum<int>(3, 9, 1).Value());
        Assert.Equal(1, await new Minimum<int>(3, 9, 1).Value());
        Assert.Equal([1, 2, 3], await new Sorted<int>(3, 1, 2).AsList().Value());
        Assert.Equal([1, 2, 3], await new Joined<int>(1, (2, 3).AsAsyncEnumerable()).AsList().Value());
        Assert.Equal(
            [1, 2, 3, 4],
            await new Joined<int>(1, 2, (3, 4).AsAsyncEnumerable()).AsList().Value()
        );
        Assert.Equal([2], await new Intersection<int>([1, 2], [2, 3]).AsList().Value());
        Assert.Equal(42, await new Empty<int>().FirstOne(_ => 42).Value());
        Assert.Equal(["x", "x"], await new Repeated<string>(() => new ValueTask<string>("x"), 2).AsList().Value());
    }

    [Fact]
    public async Task MaximumOverScalarsPassesTheToken()
    {
        using var cancellation = new CancellationTokenSource();
        await cancellation.CancelAsync();

        IAsyncScalar<int> scalar =
            new Tonga.Scalar.AsAsyncScalar<int>(token =>
            {
                token.ThrowIfCancellationRequested();
                return new ValueTask<int>(1);
            });
        var scalars = new AsAsyncEnumerable<IAsyncScalar<int>>(scalar);

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            async () => await new Maximum<int>(scalars).Value(cancellation.Token)
        );
    }

    private sealed class TrackingEnumerator(IReadOnlyList<int> items) : IAsyncEnumerator<int>
    {
        private int index = -1;

        public bool Disposed { get; private set; }

        public int Current => items[this.index];

        public ValueTask<bool> MoveNextAsync()
        {
            this.index++;
            return new ValueTask<bool>(this.index < items.Count);
        }

        public ValueTask DisposeAsync()
        {
            this.Disposed = true;
            return default;
        }
    }
}
