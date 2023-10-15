

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

            Assert.True(
                new LengthOf(
                    new Filtered<int>(
                    input => input == element,
                    new Repeated<int>(
                            element,
                            size
                        )
                    )
                ).Value() == size,
            "Can't generate an iterable with fixed size");
        }

        [Fact]
        public void EmptyTest()
        {
            Assert.True(
                new LengthOf(
                    new Repeated<int>(0, 0)
                ).Value() == 0,
            "Can't generate an empty iterable");
        }
    }
}
