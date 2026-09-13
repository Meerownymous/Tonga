using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tonga.AsyncEnumerable;
using Tonga.Enumerable;
using Tonga.Fact;
using Tonga.Optional;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.AsyncEnumerable;

public sealed class AsSyncTests
{
    [Fact]
    public void EnumerableBecomesSync() =>
        Assert.Equal(
            [1, 2, 3],
            (1, 2, 3).AsAsyncEnumerable().AsSync().ToList()
        );

    [Fact]
    public void SyncEnumerableStaysLazy()
    {
        var read = 0;
        var chain =
            (1, 2, 3, 4, 5)
                .AsAsyncEnumerable()
                .OnEach((int _) => read++)
                .AsSync();

        Assert.Equal(0, read);
        Assert.Equal([1, 2, 3], Tonga.Enumerable.EnumerableSmarts.AsHead(chain, 3).ToList());
        Assert.Equal(3, read);
    }

    [Fact]
    public void ScalarBecomesSync() =>
        Assert.Equal(3, (1, 2, 3).AsAsyncEnumerable().Length().AsSync().Value());

    [Fact]
    public void FactBecomesSync() =>
        Assert.True((1, 2, 3).AsAsyncEnumerable().Contains(2).AsSync().IsTrue());

    [Fact]
    public void FactKnowsFalseWhenSync() =>
        Assert.True(new Tonga.AsyncEnumerable.Empty<int>().Contains(2).AsSync().IsFalse());

    [Fact]
    public void OptionalBecomesSync()
    {
        var optional = (7, 8).AsAsyncEnumerable().AsOptional().AsSync();

        Assert.True(optional.Has());
        Assert.Equal(7, optional.Value());
    }

    [Fact]
    public void SyncOptionalActsOnTheValue()
    {
        var seen = 0;
        (7, 8).AsAsyncEnumerable().AsOptional().AsSync().IfHas(item => seen = item);
        Assert.Equal(7, seen);
    }

    [Fact]
    public void SyncOptionalActsOnTheMissingValue()
    {
        var acted = false;
        new Tonga.AsyncEnumerable.Empty<int>().AsOptional().AsSync().IfNot(() => acted = true);
        Assert.True(acted);
    }

    [Fact]
    public void EmptySyncOptionalThrowsOnValue() =>
        Assert.Throws<InvalidOperationException>(
            () => new Tonga.AsyncEnumerable.Empty<int>().AsOptional().AsSync().Value()
        );

    [Fact]
    public void RoundTripKeepsTheItems() =>
        Assert.Equal(
            [1, 2, 3],
            (1, 2, 3).AsAsyncEnumerable().AsSync().AsAsyncEnumerable().AsSync().ToList()
        );
}

public sealed class NamingTests
{
    /// <remarks>
    /// A source which is not async yet converts through an AsAsync smart. One which already is
    /// carries no marker in the name.
    /// </remarks>
    [Fact]
    public async Task NotYetAsyncSourcesConvertThroughAsAsync()
    {
        Assert.Equal([1], await 1.AsAsyncSingle().AsList().Value());
        Assert.Equal([1, 1], await 1.AsAsyncEndless().AsHead(2).AsList().Value());
        Assert.Equal(["x", "x"], await "x".AsAsyncRepeated(2).AsList().Value());
        Assert.Equal([1, 2], await new List<int> { 1, 2 }.AsAsyncEnumerable().AsList().Value());
        Assert.Equal(5, await new AsScalar<int>(5).AsAsyncScalar().Value());
        Assert.True(await new AsFact(true).AsAsyncFact().IsTrue());
        Assert.Equal("a", await new OptFull<string>("a").AsAsyncOptional().Value());
    }

    [Fact]
    public async Task AlreadyAsyncSourcesCarryNoMarker()
    {
        IAsyncScalar<string> scalar = new AsAsyncScalar<string>("x");
        Assert.Equal(["x", "x"], await scalar.AsRepeated(2).AsList().Value());

        Func<ValueTask<int>> deferred = () => new ValueTask<int>(7);
        Assert.Equal(7, await deferred.AsScalar().Value());

        Func<ValueTask<bool>> condition = () => new ValueTask<bool>(true);
        Assert.True(await condition.AsFact().IsTrue());

        Func<IAsyncEnumerable<int>> source = () => (1, 2).AsAsyncEnumerable();
        Assert.Equal([1, 2], await source.AsEnumerable().AsList().Value());

        Assert.Equal(1, await (1, 2).AsAsyncEnumerable().AsOptional().Value());
    }
}
