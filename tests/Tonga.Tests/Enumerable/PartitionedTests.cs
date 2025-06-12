using Tonga.Enumerable;
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
                ("hokus", "pokus")
                    .AsEnumerable()
                    .AsPartitioned(1)
                    .Length()
                    .Value()
            );
        }
    }
}
