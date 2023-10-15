

using Xunit;

namespace Tonga.Enumerable.Test
{
    public sealed class ReducedTest
    {
        [Fact]
        public void Reduces()
        {
            Assert.Equal(
                24,
                new Reduced<int>(
                    EnumerableOf.Pipe(0, 1, 1, 2, 2, 3, 4, 5, 6),
                    (first, second) => first + second
                ).Value()
            );
        }
    }
}
