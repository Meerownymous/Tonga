

using Xunit;

namespace Tonga.Enumerable.Test
{
    public sealed class EndlessTest
    {
        [Fact]
        public void EndlessIterableTest()
        {
            Assert.True(
                new ItemAt<int>(
                    new Endless<int>(1),
                    0
                ).Value() == 1,
                "Can't get unique endless iterable item"
            );
        }
    }
}
