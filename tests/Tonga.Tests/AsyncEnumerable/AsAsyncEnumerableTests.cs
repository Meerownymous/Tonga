using System.Collections.Generic;
using System.Threading.Tasks;
using Tonga.AsyncEnumerable;
using Xunit;

namespace Tonga.Tests.AsyncEnumerable;

public sealed class AsAsyncEnumerableTests
{
    [Fact]
    public async Task MakesEnumerableFromArray()
    {
        Assert.Equal(
            ["a", "b", "c"],
            await new AsAsyncEnumerable<string>("a", "b", "c").AsList().Value()
        );
    }

    [Fact]
    public async Task MakesEnumerableFromTuple()
    {
        Assert.Equal(
            [1, 2, 3, 4],
            await (1, 2, 3, 4).AsAsyncEnumerable().AsList().Value()
        );
    }

    [Fact]
    public async Task AdaptsSyncEnumerable()
    {
        Assert.Equal(
            [1, 2, 3],
            await new List<int> { 1, 2, 3 }.AsAsyncEnumerable().AsList().Value()
        );
    }

    [Fact]
    public async Task IsRepeatable()
    {
        var enumerable = (1, 2, 3).AsAsyncEnumerable();

        Assert.Equal(
            await enumerable.AsList().Value(),
            await enumerable.AsList().Value()
        );
    }

    [Fact]
    public async Task AdaptingSyncEnumerableReadsNothingUpFront()
    {
        var advanced = 0;
        var source = Counting(() => advanced++);

        var chain = source.AsAsyncEnumerable().AsHead(2);
        Assert.Equal(0, advanced);

        await chain.AsList().Value();
        Assert.Equal(2, advanced);
    }

    private static IEnumerable<int> Counting(System.Action onEach)
    {
        for (var i = 0; i < 10; i++)
        {
            onEach();
            yield return i;
        }
    }
}
