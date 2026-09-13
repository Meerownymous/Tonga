using System;
using System.Threading.Tasks;
using Tonga.AsyncEnumerable;
using Xunit;

namespace Tonga.Tests.AsyncEnumerable;

public sealed class FactTests
{
    [Fact]
    public async Task ContainsFindsTheItem() =>
        Assert.True(await (1, 2, 3).AsAsyncEnumerable().Contains(2).IsTrue());

    [Fact]
    public async Task ContainsMissesTheItem() =>
        Assert.False(await (1, 2, 3).AsAsyncEnumerable().Contains(9).IsTrue());

    [Fact]
    public async Task ContainsReadsNoFurtherThanTheMatch()
    {
        var read = 0;
        await (1, 2, 3, 4, 5).AsAsyncEnumerable().OnEach((int _) => read++).Contains(2).IsTrue();
        Assert.Equal(2, read);
    }

    [Fact]
    public async Task HasAtLeastKnowsTrue() =>
        Assert.True(await (1, 2, 3).AsAsyncEnumerable().HasAtLeast(3).IsTrue());

    [Fact]
    public async Task HasAtLeastKnowsFalse() =>
        Assert.False(await (1, 2).AsAsyncEnumerable().HasAtLeast(3).IsTrue());

    [Fact]
    public async Task HasAtLeastStopsAtAmount()
    {
        var read = 0;
        Assert.True(
            await ("a", "b", "c", "d", "e", "f")
                .AsAsyncEnumerable()
                .OnEach((string _) => read++)
                .HasAtLeast(3)
                .IsTrue()
        );
        Assert.Equal(3, read);
    }

    [Fact]
    public async Task HasAtLeastRejectsNegativeAmount() =>
        await Assert.ThrowsAsync<ArgumentException>(
            async () => await (1, 2).AsAsyncEnumerable().HasAtLeast(-1).IsTrue()
        );

    [Fact]
    public async Task HasLessThanKnowsTrue() =>
        Assert.True(await (1, 2).AsAsyncEnumerable().HasLessThan(3).IsTrue());

    [Fact]
    public async Task HasLessThanKnowsFalse() =>
        Assert.False(await (1, 2, 3).AsAsyncEnumerable().HasLessThan(3).IsTrue());

    [Fact]
    public async Task HasMoreThanKnowsTrue() =>
        Assert.True(await (1, 2, 3, 4).AsAsyncEnumerable().HasMoreThan(3).IsTrue());

    [Fact]
    public async Task HasMoreThanKnowsFalse() =>
        Assert.False(await (1, 2, 3).AsAsyncEnumerable().HasMoreThan(3).IsTrue());

    /// <remarks>
    /// The sync <see cref="Tonga.Enumerable.IsEmpty{T}"/> answers the opposite: it is true when the
    /// source has items. The async branch answers what the name says.
    /// </remarks>
    [Fact]
    public async Task IsEmptyKnowsEmpty() =>
        Assert.True(await new Empty<int>().IsEmpty().IsTrue());

    [Fact]
    public async Task IsEmptyKnowsNotEmpty() =>
        Assert.False(await 1.AsAsyncSingle().IsEmpty().IsTrue());

    [Fact]
    public async Task IsEmptyReadsAtMostOneItem()
    {
        var read = 0;
        await (1, 2, 3).AsAsyncEnumerable().OnEach((int _) => read++).IsEmpty().IsTrue();
        Assert.Equal(1, read);
    }

    [Fact]
    public async Task IsFalseIsTheNegation() =>
        Assert.True(await new Empty<int>().IsEmpty().IsFalse() == false);
}
