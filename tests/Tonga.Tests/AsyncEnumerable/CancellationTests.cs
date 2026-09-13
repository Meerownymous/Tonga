using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Tonga.AsyncEnumerable;
using Xunit;

namespace Tonga.Tests.AsyncEnumerable;

public sealed class CancellationTests
{
    [Fact]
    public async Task StopsAnEndlessSource()
    {
        using var cancellation = new CancellationTokenSource();
        await cancellation.CancelAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            async () => await 1.AsAsyncEndless().AsList().Value(cancellation.Token)
        );
    }

    [Fact]
    public async Task ReachesThroughDecoratorsToTheSource()
    {
        using var cancellation = new CancellationTokenSource();
        var seen = CancellationToken.None;

        var chain =
            Observing(token => seen = token)
                .AsMapped(item => item)
                .AsFiltered(_ => true)
                .AsHead(1);

        await chain.AsList().Value(cancellation.Token);

        Assert.Equal(cancellation.Token, seen);
    }

    [Fact]
    public async Task StopsASourceWhichObservesTheToken()
    {
        using var cancellation = new CancellationTokenSource();
        var read = 0;

        var chain =
            Counting(() => read++)
                .AsMapped(item => item)
                .AsFiltered(_ => true);

        await Assert.ThrowsAnyAsync<OperationCanceledException>(async () =>
        {
            await foreach (var item in chain.WithCancellation(cancellation.Token))
            {
                if (item == 2)
                    await cancellation.CancelAsync();
            }
        });

        // cancelled on the second item, so the source never produced a third
        Assert.Equal(2, read);
    }

    [Fact]
    public async Task ReachesATerminalOperation()
    {
        using var cancellation = new CancellationTokenSource();
        await cancellation.CancelAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            async () => await 1.AsAsyncEndless().Length().Value(cancellation.Token)
        );
    }

    [Fact]
    public async Task DefaultTokenNeverCancels() =>
        Assert.Equal([1, 1, 1], await 1.AsAsyncEndless().AsHead(3).AsList().Value());

    private static async IAsyncEnumerable<int> Counting(
        Action onEach, [EnumeratorCancellation] CancellationToken cancellation = default
    )
    {
        for (var i = 1; i <= 10; i++)
        {
            cancellation.ThrowIfCancellationRequested();
            onEach();
            yield return i;
        }
    }

    private static async IAsyncEnumerable<int> Observing(
        Action<CancellationToken> onStart, [EnumeratorCancellation] CancellationToken cancellation = default
    )
    {
        onStart(cancellation);
        yield return 1;
    }
}
