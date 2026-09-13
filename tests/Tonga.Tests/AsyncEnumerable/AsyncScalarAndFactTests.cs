using System.Threading.Tasks;
using Tonga.Fact;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.AsyncEnumerable;

public sealed class AsyncScalarAndFactTests
{
    [Fact]
    public async Task ScalarDeliversItsValue() =>
        Assert.Equal(3, await new AsAsyncScalar<int>(3).Value());

    [Fact]
    public async Task ScalarRunsOnlyWhenAwaited()
    {
        var ran = false;
        var scalar = new AsAsyncScalar<int>(() =>
        {
            ran = true;
            return new ValueTask<int>(1);
        });

        Assert.False(ran);
        await scalar.Value();
        Assert.True(ran);
    }

    [Fact]
    public async Task ScalarRunsAgainOnEveryAwait()
    {
        var runs = 0;
        var scalar = new AsAsyncScalar<int>(() => new ValueTask<int>(++runs));

        Assert.Equal(1, await scalar.Value());
        Assert.Equal(2, await scalar.Value());
    }

    [Fact]
    public async Task SyncScalarBecomesAsync() =>
        Assert.Equal(5, await new AsScalar<int>(5).AsAsyncScalar().Value());

    [Fact]
    public async Task FactKnowsTrue() =>
        Assert.True(await new AsAsyncFact(true).IsTrue());

    [Fact]
    public async Task FactKnowsFalse() =>
        Assert.True(await new AsAsyncFact(false).IsFalse());

    [Fact]
    public async Task FactRunsOnlyWhenAwaited()
    {
        var ran = false;
        var fact = new AsAsyncFact(() =>
        {
            ran = true;
            return new ValueTask<bool>(true);
        });

        Assert.False(ran);
        await fact.IsTrue();
        Assert.True(ran);
    }

    [Fact]
    public async Task SyncFactBecomesAsync() =>
        Assert.True(await new AsFact(true).AsAsyncFact().IsTrue());
}
