using Tonga.Enumerable;
using Tonga.Number;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class HeadOfTest
    {
        [Fact]
        public void EnumeratesOverPrefixOfGivenLength()
        {
            Assert.Equal(
                3,
                new SumOf(
                    (0, 1, 2, 3, 4)
                        .AsEnumerable()
                        .AsHead(3)
                ).ToInt()
            );
        }

        [Fact]
        public void IteratesOverWholeEnumerableIfThereAreNotEnoughItems()
        {
            Assert.Equal(
                15,
                new SumOf(
                    (0, 1, 2, 3, 4, 5)
                        .AsEnumerable()
                        .AsHead(10)
                ).ToInt()
            );
        }

        [Fact]
        public void LimitOfZeroProducesEmptyEnumerable()
        {
            Assert.Empty(
                (0, 1, 2, 3, 4)
                    .AsEnumerable()
                    .AsHead(0)
            );
        }

        [Fact]
        public void NegativeLimitProducesEmptyEnumerable()
        {
            Assert.Empty(
                (0, 1, 2, 3, 4)
                    .AsEnumerable()
                    .AsHead(-1)
            );
        }

        [Fact]
        public void EmptyEnumerableProducesEmptyEnumerable()
        {
            Assert.Empty(
                new None<Nothing>()
                    .AsHead(10)
            );
        }
    }
}
