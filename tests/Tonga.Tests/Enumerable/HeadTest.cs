using Tonga.Enumerable;
using Tonga.Number;
using Tonga.Scalar;
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
                    Head._(
                        AsEnumerable._(0, 1, 2, 3, 4),
                        3
                    )
                ).AsInt()
            );
        }

        [Fact]
        public void IteratesOverWholeEnumerableIfThereAreNotEnoughItems()
        {
            Assert.Equal(
                15,
                new SumOf(
                    Head._(
                        AsEnumerable._(0, 1, 2, 3, 4, 5),
                        10
                    )
                ).AsInt()
            );
        }

        [Fact]
        public void LimitOfZeroProducesEmptyEnumerable()
        {
            Assert.Equal(
                0,
                Length._(
                    Head._(
                        AsEnumerable._(0, 1, 2, 3, 4),
                        0
                    )
                ).Value()
            );
        }

        [Fact]
        public void NegativeLimitProducesEmptyEnumerable()
        {
            Assert.Equal(
                0,
                Length._(
                    Head._(
                        AsEnumerable._(0, 1, 2, 3, 4),
                        -1
                    )
                ).Value()
            );
        }

        [Fact]
        public void EmptyEnumerableProducesEmptyEnumerable()
        {
            Assert.Equal(
                0,
                Length._(
                    Head._(
                        None._<Nothing>(),
                        10
                    )
                ).Value()
            );
        }
    }
}
