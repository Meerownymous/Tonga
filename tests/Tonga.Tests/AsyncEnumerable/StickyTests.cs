using System.Collections.Generic;
using System.Threading.Tasks;
using Tonga.AsyncEnumerable;
using Xunit;

namespace Tonga.Tests.AsyncEnumerable;

public sealed class StickyTests
{
    [Fact]
    public async Task ReadsTheSourceOnlyOnce()
    {
        var read = 0;
        var sticky =
            (1, 2, 3)
                .AsAsyncEnumerable()
                .OnEach((int _) => read++)
                .AsSticky();

        Assert.Equal([1, 2, 3], await sticky.AsList().Value());
        Assert.Equal([1, 2, 3], await sticky.AsList().Value());
        Assert.Equal(3, read);
    }

    [Fact]
    public async Task ChangesOfSourceAreIgnored()
    {
        var content = 0;
        var items =
            new Sticky<string>(
                Growing(() => (++content).ToString())
            );

        Assert.Equal(
            await items.AsList().Value(),
            await items.AsList().Value()
        );
    }

    [Fact]
    public async Task MemoizesOnlyWhatWasRead()
    {
        var read = 0;
        var sticky =
            (1, 2, 3, 4, 5)
                .AsAsyncEnumerable()
                .OnEach((int _) => read++)
                .AsSticky();

        await sticky.AsHead(2).AsList().Value();
        Assert.Equal(2, read);

        Assert.Equal([1, 2, 3, 4, 5], await sticky.AsList().Value());
        Assert.Equal(5, read);
    }

    private static async IAsyncEnumerable<string> Growing(System.Func<string> next)
    {
        yield return next();
    }
}
