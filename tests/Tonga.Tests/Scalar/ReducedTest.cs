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
                    (0, 1, 1, 2, 2, 3, 4, 5, 6)
                        .AsEnumerable()
                        .AsReduced((first, second) => first + second)
                .Value()
            );
        }
    }
}
