using System;
using Tonga.Enumerable;
using Tonga.List;
using Tonga.Map;
using Xunit;

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
                (
                    (1, 4).AsPair(),
                    (6, 3).AsPair(),
                    (-5, 2).AsPair()
                ).AsMap();

            var sorted = unsorted.AsSorted();
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
                (
                    (1, 4).AsPair(),
                    (6, 3).AsPair(),
                    (-5, 2).AsPair()
                )
                .AsMap()
                .AsSorted()
                [key]
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
                (
                    (1, 4).AsPair(),
                    (6, 3).AsPair(),
                    (-5, 2).AsPair()
                ).AsMap()
                .AsSorted((a, b) => a.CompareTo(b))
                .Pairs()
                .ItemAt(index)
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
                (
                    (1, 4).AsPair(),
                    (6, 3).AsPair(),
                    (-5, 2).AsPair()
                ).AsMap()
                .AsSorted()
                .Pairs()
                .AsList()
                [index]
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
                (
                    1.AsPair(() => throw new Exception("i shall not be called")),
                    6.AsPair(() => throw new Exception("i shall not be called")),
                    (-5).AsPair(() => throw new Exception("i shall not be called"))
                ).AsMap()
                .AsSorted()
                .Keys()
                .ItemAt(index)
                .Value()
            );
        }

        [Fact]
        public void DeliversSingleValueWhenLazy()
        {
            Assert.Equal(
                4,
                (
                    1.AsPair(() => 4),
                    6.AsPair(() => throw new Exception("i shall not be called")),
                    (-5).AsPair(() => throw new Exception("i shall not be called"))
                )
                .AsMap()
                .AsSorted()[1]
            );
        }
    }
}
