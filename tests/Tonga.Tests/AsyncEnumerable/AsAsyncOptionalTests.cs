using System;
using System.Threading.Tasks;
using Tonga.AsyncEnumerable;
using Tonga.Optional;
using Xunit;

namespace Tonga.Tests.AsyncEnumerable;

public sealed class AsAsyncOptionalTests
{
    [Fact]
    public async Task KnowsItHasAValue() =>
        Assert.True(await (1, 2, 3).AsAsyncEnumerable().AsAsyncOptional().Has());

    [Fact]
    public async Task KnowsItHasNoValue() =>
        Assert.False(await new Empty<int>().AsAsyncOptional().Has());

    [Fact]
    public async Task DeliversTheFirstItem() =>
        Assert.Equal(1, await (1, 2, 3).AsAsyncEnumerable().AsAsyncOptional().Value());

    [Fact]
    public async Task DeliversTheFirstMatchingItem() =>
        Assert.Equal(3, await (1, 2, 3, 4).AsAsyncEnumerable().AsAsyncOptional(item => item > 2).Value());

    [Fact]
    public async Task ThrowsWhenEmpty() =>
        await Assert.ThrowsAsync<InvalidOperationException>(
            async () => await new Empty<int>().AsAsyncOptional().Value()
        );

    [Fact]
    public async Task ReadsNoMoreThanOneItem()
    {
        var read = 0;
        await (1, 2, 3, 4, 5)
            .AsAsyncEnumerable()
            .OnEach((int _) => read++)
            .AsAsyncOptional()
            .Value();

        Assert.Equal(1, read);
    }

    [Fact]
    public async Task BuildingTheOptionalReadsNothing()
    {
        var read = 0;
        var optional =
            (1, 2, 3)
                .AsAsyncEnumerable()
                .OnEach((int _) => read++)
                .AsAsyncOptional();

        Assert.Equal(0, read);
        await optional.Value();
        Assert.Equal(1, read);
    }

    [Fact]
    public async Task ActsWhenItHasAValue()
    {
        var seen = 0;
        await (7, 8).AsAsyncEnumerable().AsAsyncOptional().IfHas(item => seen = item).Has();
        Assert.Equal(7, seen);
    }

    [Fact]
    public async Task DoesNotActWhenItHasNoValue()
    {
        var acted = false;
        await new Empty<int>().AsAsyncOptional().IfHas(_ => acted = true).Has();
        Assert.False(acted);
    }

    [Fact]
    public async Task ActsWhenItHasNoValue()
    {
        var acted = false;
        await new Empty<int>().AsAsyncOptional().IfNot(() => acted = true).Has();
        Assert.True(acted);
    }

    [Fact]
    public async Task AwaitsTheRegisteredAction()
    {
        var seen = 0;
        await (7, 8)
            .AsAsyncEnumerable()
            .AsAsyncOptional()
            .IfHas(async item =>
            {
                await Task.Yield();
                seen = item;
            })
            .Has();

        Assert.Equal(7, seen);
    }

    [Fact]
    public async Task ChainsBothBranches()
    {
        var seen = 0;
        var missed = false;

        await (7, 8)
            .AsAsyncEnumerable()
            .AsAsyncOptional()
            .IfHas(item => seen = item)
            .IfNot(() => missed = true)
            .Has();

        Assert.Equal(7, seen);
        Assert.False(missed);
    }

    [Fact]
    public async Task FullOptionalDeliversItsValue() =>
        Assert.Equal("a", await new AsyncOptFull<string>("a").Value());

    [Fact]
    public async Task FullOptionalDoesNotMaterialiseWithoutAnAction()
    {
        var materialised = false;
        var optional = new AsyncOptFull<int>(() =>
        {
            materialised = true;
            return new ValueTask<int>(1);
        });

        Assert.True(await optional.Has());
        Assert.False(materialised);
    }

    [Fact]
    public async Task EmptyOptionalThrowsOnValue() =>
        await Assert.ThrowsAsync<InvalidOperationException>(
            async () => await new AsyncOptEmpty<int>().Value()
        );

    [Fact]
    public async Task SyncOptionalBecomesAsync() =>
        Assert.Equal("a", await new OptFull<string>("a").AsAsyncOptional().Value());
}
