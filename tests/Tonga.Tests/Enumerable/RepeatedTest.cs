

using Tonga.Scalar;
using Xunit;

namespace Tonga.Enumerable.Test
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
                LengthOf._(
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
                LengthOf._(
                    Repeated._(0, 0)
                ).Value()
            );
        }
    }
}
