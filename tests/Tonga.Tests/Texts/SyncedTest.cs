

using System;
using System.Threading.Tasks;
using Xunit;

namespace Tonga.Text.Test
{
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
            Parallel.For(0, max, (nr) => text.AsString());

            Assert.Equal(
                max, check
            );
        }
    }
}
