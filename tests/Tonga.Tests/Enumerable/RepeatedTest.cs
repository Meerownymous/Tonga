using Tonga.Enumerable;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class RepeatedTest
    {
        [Fact]
        public void AllSameTest()
        {
            int size = 42;
            int element = 11;

            Assert.Equal(
                size,
                Length._(
                    Filtered._(
                    input => input == element,
                    Repeated._(
                            element,
                            size
                        )
                    )
                ).Value()
            );
        }

        [Fact]
        public void EmptyTest()
        {
            Assert.Equal(
                0,
                Length._(
                    Repeated._(0, 0)
                ).Value()
            );
        }
    }
}
