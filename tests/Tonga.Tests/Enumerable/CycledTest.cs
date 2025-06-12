using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class CycledTest
    {
        [Fact]
        public void RepeatsEnumerable()
        {
            Assert.Equal(
                "two",
                new AsEnumerable<string>("one", "two", "three")
                    .AsCycled()
                    .ItemAt(7)
                    .Value()
            );
        }
    }
}
