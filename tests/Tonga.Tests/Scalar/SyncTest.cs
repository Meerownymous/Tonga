using System;
using System.Threading.Tasks;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Scalar
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
