

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Tonga.List;

namespace Tonga.Scalar.Tests
{
    public sealed class SolidTest
    {
        [Fact]
        public void CachesResult()
        {
            var check = 0;
            var sc = new Solid<int>(() => check += 1);
            var max = Environment.ProcessorCount << 8;
            Parallel.For(0, max, (nr) => sc.Value());

            Assert.Equal(sc.Value(), sc.Value());
        }

        [Fact]
        public void WorksInMultipleThreads()
        {
            var sc = new Solid<IList<int>>(() => new AsList<int>(1, 2));
            var max = Environment.ProcessorCount << 8;
            Parallel.For(0, max, (nr) => sc.Value());

            Assert.Equal(
                sc.Value(), sc.Value()
            );
        }
    }
}
