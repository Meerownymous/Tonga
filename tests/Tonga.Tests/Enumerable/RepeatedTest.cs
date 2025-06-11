using Tonga.Enumerable;
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
                element
                    .AsRepeated(size)
                    .AsFiltered(input => input == element)
                    .Length()
                    .Value()
            );
        }

        [Fact]
        public void EmptyTest()
        {
            Assert.Equal(
                0,
                123.AsRepeated(0)
                    .Length()
                    .Value()
            );
        }
    }
}
