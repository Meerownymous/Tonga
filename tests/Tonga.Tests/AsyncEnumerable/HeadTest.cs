using System.Threading.Tasks;
using Tonga.AsyncEnumerable;
using Xunit;

namespace Tonga.Tests.AsyncEnumerable;

public sealed class HeadTest
{
    [Fact]
    public async Task EnumeratesOverPrefixOfGivenLength()
    {
        Assert.Equal(
            [0, 1, 2],
            await (0, 1, 2, 3, 4)
                .AsAsyncEnumerable()
                .AsHead(3)
                .AsList()
                .Value()
        );
    }

    [Fact]
    public async Task IteratesOverWholeEnumerableIfThereAreNotEnoughItems()
    {
        Assert.Equal(
            [0, 1, 2, 3, 4, 5],
            await (0, 1, 2, 3, 4, 5)
                .AsAsyncEnumerable()
                .AsHead(10)
                .AsList()
                .Value()
        );
    }

    [Fact]
    public async Task ReadsNoMoreItemsThanTheLimit()
    {
        var advanced = 0;
        Assert.Equal(
            [0, 1, 2],
            await (0, 1, 2, 3, 4)
                .AsAsyncEnumerable()
                .OnEach((int _) => advanced++)
                .AsHead(3)
                .AsList()
                .Value()
        );
        Assert.Equal(3, advanced);
    }

    [Fact]
    public async Task BuildingTheChainReadsNothing()
    {
        var advanced = 0;
        var chain =
            (0, 1, 2, 3, 4)
                .AsAsyncEnumerable()
                .OnEach((int _) => advanced++)
                .AsHead(3);

        Assert.Equal(0, advanced);
        await chain.AsList().Value();
        Assert.Equal(3, advanced);
    }

    [Fact]
    public async Task LimitOfZeroProducesEmptyEnumerable()
    {
        Assert.Empty(
            await (0, 1, 2, 3, 4)
                .AsAsyncEnumerable()
                .AsHead(0)
                .AsList()
                .Value()
        );
    }

    [Fact]
    public async Task NegativeLimitProducesEmptyEnumerable()
    {
        Assert.Empty(
            await (0, 1, 2, 3, 4)
                .AsAsyncEnumerable()
                .AsHead(-1)
                .AsList()
                .Value()
        );
    }

    [Fact]
    public async Task EmptyEnumerableProducesEmptyEnumerable()
    {
        Assert.Empty(
            await new Empty<Nothing>()
                .AsHead(10)
                .AsList()
                .Value()
        );
    }
}
