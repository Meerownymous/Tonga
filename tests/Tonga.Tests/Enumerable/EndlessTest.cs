using Tonga.Enumerable;
using Tonga.Scalar;
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
                ItemAt._(
                    Endless._(1),
                    0
                ).Value()
            );
        }
    }
}
