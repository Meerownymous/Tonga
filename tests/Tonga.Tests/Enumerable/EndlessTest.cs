using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class EndlessTest
    {
        [Fact]
        public void DeliversItem()
        {
            Assert.Equal(
                1,
                1.AsEndless()
                .ItemAt(0)
                .Value()
            );
        }
    }
}
