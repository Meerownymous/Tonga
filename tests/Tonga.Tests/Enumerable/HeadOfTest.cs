

using Xunit;
using Tonga.Misc;
using Tonga.Number;
using Tonga.Scalar;

namespace Tonga.Enumerable.Test
{
    public sealed class HeadOfTest
    {
        [Fact]
        public void EnumeratesOverPrefixOfGivenLength()
        {
            Assert.True(
                new SumOf(
                    new Head<int>(
                        EnumerableOf.Pipe(0, 1, 2, 3, 4),
                        3
                    )
                ).AsInt() == 3,
            "Can't limit an enumerable with more items");
        }

        [Fact]
        public void IteratesOverWholeEnumerableIfThereAreNotEnoughItems()
        {
            Assert.True(
                new SumOf(
                    new Head<int>(
                        EnumerableOf.Pipe(0, 1, 2, 3, 4, 5),
                        10
                    )
                ).AsInt() == 15,
                "Can't limit an enumerable with less items"
            );
        }

        [Fact]
        public void LimitOfZeroProducesEmptyEnumerable()
        {
            Assert.True(
                new LengthOf(
                    new Head<int>(
                        EnumerableOf.Pipe(0, 1, 2, 3, 4),
                        0
                    )
                ).Value() == 0,
                "Can't limit an iterable to zero items"
            );
        }

        [Fact]
        public void NegativeLimitProducesEmptyEnumerable()
        {
            Assert.True(
                new LengthOf(
                    new Head<int>(
                        EnumerableOf.Pipe(0, 1, 2, 3, 4),
                        -1
                    )
                ).Value() == 0,
                "Can't limit an iterable to negative number of items"
            );
        }

        [Fact]
        public void EmptyEnumerableProducesEmptyEnumerable()
        {
            Assert.True(
                new LengthOf(
                    new Head<Nothing>(
                        new None<Nothing>(),
                        10
                    )
                ).Value() == 0,
                "Can't limit an empty enumerable"
            );
        }
    }
}
