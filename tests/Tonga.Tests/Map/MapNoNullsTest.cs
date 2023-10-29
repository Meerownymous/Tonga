

using System;
using System.Collections.Generic;
using Xunit;

namespace Tonga.Map.Tests
{
    public class MapNoNullTest
    {
        [Fact]
        public void PairsContainExistingItem()
        {
            var pair = AsPair._(0, 0);
            Assert.Contains(
                pair,
                NoNulls._(
                    AsMap._(
                        pair
                    )
                ).Pairs()
            );
        }

        [Fact]
        public void ContainsKey()
        {
            var map =
                NoNulls._(
                    AsMap._(
                        AsPair._(0, 0)
                    )
                );
            Assert.True(map.Keys().Contains(0));
        }

        [Fact]
        public void WrapsNewPair()
        {
            var pair = AsPair._(0, 0);
            var map =
                NoNulls._(
                    Empty._<int,int>()
                );
            map = map.With(pair);
            Assert.Contains(
                pair,
                map.Pairs()
            );
        }

        [Fact]
        public void RejectsEntryWithNullKey()
        {
            var map =
                NoNulls._(
                    Empty._<object,object>()
                );
            Assert.Throws<ArgumentException>(
                () => map.With(AsPair._<object,object>(null, 0))
            );
        }

        [Fact]
        public void RejectsEntryWithNullValue()
        {
            var map =
                NoNulls._(
                    Empty._<object,object>()
                );
            Assert.Throws<ArgumentException>(
                () => map.With(AsPair._<object, object>(0, null))
            );
        }

        [Fact]
        public void RejectsEntryWithNullKeyAndNullValue()
        {
            Assert.Throws<ArgumentException>(() =>
                NoNulls._(
                    Empty._<object, object>()
                ).With(AsPair._<object, object>(null, null))
            );
        }

        [Fact]
        public void DeliversValue()
        {
            Assert.Equal(
                0,
                NoNulls._(
                    AsMap._(
                        AsPair._(0, 0)
                    )
                )[0]
            );
        }

        [Fact]
        public void RejectsGettingValueWithNullKey()
        {
            Assert.Throws<ArgumentException>(() =>
                NoNulls._(
                    AsMap._(
                        AsPair._(new object(), new object())
                    )
                )[null]
            );
        }

        [Fact]
        public void RejectsDeliveringNullValue()
        {
            Assert.Throws<ArgumentException>(() =>
                NoNulls._(
                    AsMap._(
                        AsPair._<int,object>(0, null)
                    )
                )
            );
        }
    }
}
