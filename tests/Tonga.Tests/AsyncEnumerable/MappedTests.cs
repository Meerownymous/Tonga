using System.Threading.Tasks;
using Tonga.AsyncEnumerable;
using Xunit;

namespace Tonga.Tests.AsyncEnumerable;

public sealed class MappedTests
{
    [Fact]
    public async Task MapsWithSyncFunction()
    {
        Assert.Equal(
            ["a!", "b!"],
            await ("a", "b").AsAsyncEnumerable().AsMapped(item => item + "!").AsList().Value()
        );
    }

    [Fact]
    public async Task MapsWithIndex()
    {
        Assert.Equal(
            ["a0", "b1"],
            await ("a", "b").AsAsyncEnumerable().AsMapped((item, index) => item + index).AsList().Value()
        );
    }

    [Fact]
    public async Task MapsWithAwaitedFunction()
    {
        Assert.Equal(
            [2, 4],
            await (1, 2)
                .AsAsyncEnumerable()
                .AsMapped(async item =>
                {
                    await Task.Yield();
                    return item * 2;
                })
                .AsList()
                .Value()
        );
    }

    [Fact]
    public async Task MapsLazily()
    {
        var mapped = 0;
        var chain =
            (1, 2, 3, 4, 5)
                .AsAsyncEnumerable()
                .AsMapped(item =>
                {
                    mapped++;
                    return item;
                })
                .AsHead(2);

        Assert.Equal(0, mapped);
        await chain.AsList().Value();
        Assert.Equal(2, mapped);
    }
}
