

using System.Collections.Generic;
using Tonga.List;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Enumerable.Test
{
    public class PartitionedTests
    {
        [Fact]
        public void NewPartitionTest()
        {
            Assert.Equal(
                2,
                Length._(
                    Partitioned._(1,
                        AsList._("hokus", "pokus")
                    )
                ).Value()
            );
        }
    }
}
