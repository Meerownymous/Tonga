using System;
using System.Threading.Tasks;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text;

public sealed class SyncedTest
{
    [Fact]
    public void WorksInMultipleThreads()
    {
        var check = 0;
        var text = new Synced(
            AsText._(
                () => AsText._(check++).AsString()
            )
        );

        var max = Environment.ProcessorCount << 8;
        Parallel.For(0, max, (nr) => text.Str());

        Assert.Equal(
            max, check
        );
    }
}
