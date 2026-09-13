using System.Threading.Tasks;
using Tonga.AsyncEnumerable;
using Xunit;

namespace Tonga.Tests.AsyncEnumerable;

public sealed class FilteredTests
{
    [Fact]
    public async Task FiltersWithSyncCondition()
    {
        Assert.Equal(
            [2, 4],
            await (1, 2, 3, 4).AsAsyncEnumerable().AsFiltered(item => item % 2 == 0).AsList().Value()
        );
    }

    [Fact]
    public async Task FiltersWithAwaitedCondition()
    {
        Assert.Equal(
            [2, 4],
            await (1, 2, 3, 4)
                .AsAsyncEnumerable()
                .AsFiltered(async item =>
                {
                    await Task.Yield();
                    return item % 2 == 0;
                })
                .AsList()
                .Value()
        );
    }

    [Fact]
    public async Task ReadsOnlyWhatTheHeadNeeds()
    {
        var read = 0;
        Assert.Equal(
            [2],
            await (1, 2, 3, 4, 5, 6)
                .AsAsyncEnumerable()
                .OnEach((int _) => read++)
                .AsFiltered(item => item % 2 == 0)
                .AsHead(1)
                .AsList()
                .Value()
        );
        Assert.Equal(2, read);
    }
}
