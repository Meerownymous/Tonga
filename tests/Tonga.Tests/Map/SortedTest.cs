using System;
using Tonga.Enumerable;
using Tonga.List;
using Tonga.Map;
using Tonga.Scalar;
using Xunit;
using Sorted = Tonga.Map.Sorted;

namespace Tonga.Tests.Map
{
    public sealed class SortedTest
    {
        [Theory]
        [InlineData(1, 4)]
        [InlineData(6, 3)]
        [InlineData(-5, 2)]
        public void ValueStillBehindCorrectKeyMap(int key, int expectedValue)
        {
            var unsorted =
                AsMap._(
                    (1, 4),
                    (6, 3),
                    (-5, 2)
                );

            var sorted = Sorted._(unsorted);
            Assert.Equal(expectedValue, sorted[key]);
        }

        [Theory]
        [InlineData(1, 4)]
        [InlineData(6, 3)]
        [InlineData(-5, 2)]
        public void ValueStillBehindCorrectKeyIEnumerableKeyValuePairs(int key, int expectedValue)
        {
            Assert.Equal(
                expectedValue,
                Sorted._(
                    AsList._(
                        AsEnumerable._(
                            AsPair._(1, 4),
                            AsPair._(6, 3),
                            AsPair._(-5, 2)
                        )
                    )
                )[key]
            );
        }

        [Theory]
        [InlineData(0, -5)]
        [InlineData(1, 1)]
        [InlineData(2, 6)]
        public void SortsByFunction(int index, int expectedKey)
        {
            Assert.Equal(
                expectedKey,
                ItemAt._(
                    Sorted._(
                        AsMap._(
                            (1, 4),
                            (6, 3),
                            (-5, 2)
                        ),
                        (a, b) => a.CompareTo(b)
                    )
                    .Pairs(),
                    index
                )
                .Value()
                .Key()
            );
        }

        [Theory]
        [InlineData(0, -5)]
        [InlineData(1, 1)]
        [InlineData(2, 6)]
        public void DefaultComparerSortsByKey(int index, int expectedKey)
        {
            Assert.Equal(
                expectedKey,
                AsList._(
                    Sorted._(
                        AsMap._(
                            (1, 4),
                            (6, 3),
                            (-5, 2)
                        )
                    ).Pairs()
                )[index]
                .Key()
            );
        }

        [Theory]
        [InlineData(0, -5)]
        [InlineData(1, 1)]
        [InlineData(2, 6)]
        public void EnumeratesKeysWhenLazy(int index, int expectedKey)
        {
            Assert.Equal(
                expectedKey,
                ItemAt._(
                    Sorted._(
                        AsMap._(
                            AsPair._(1, () => { throw new Exception("i shall not be called"); }),
                            AsPair._(6, () => { throw new Exception("i shall not be called"); }),
                            AsPair._(-5, () => { throw new Exception("i shall not be called"); })
                        )
                    ).Keys(),
                    index
                ).Value()
            );
        }

        [Fact]
        public void DeliversSingleValueWhenLazy()
        {
            Assert.Equal(
                4,
                Sorted._(
                    AsMap._(
                        AsPair._(1, () => 4),
                        AsPair._<int, int>(6, () => { throw new Exception("i shall not be called"); }),
                        AsPair._<int, int>(-5, () => { throw new Exception("i shall not be called"); })
                    )
                )[1]
            );
        }
    }
}
