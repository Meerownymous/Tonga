

using System;
using System.Threading.Tasks;
using Xunit;

namespace Tonga.Scalar.Tests
{
    public sealed class SyncTest
    {
        [Fact]
        public void WorksInMultipleThreads()
        {
            var check = 0;
            var sc = new Sync<int>(() => check += 1);

            var max = Environment.ProcessorCount << 8;
            Parallel.For(0, max, (nr) => sc.Value());

            Assert.Equal(
                max, check
            );
        }
    }
}
