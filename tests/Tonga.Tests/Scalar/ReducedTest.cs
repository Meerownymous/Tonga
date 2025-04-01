using Tonga.Enumerable;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Scalar
{
    public sealed class ReducedTest
    {
        [Fact]
        public void Reduces()
        {
            Assert.Equal(
                24,
                Reduced._(
                    AsEnumerable._(0, 1, 1, 2, 2, 3, 4, 5, 6),
                    (first, second) => first + second
                ).Value()
            );
        }
    }
}
