using Tonga.Enumerable;
using Tonga.List;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Enumerable
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
