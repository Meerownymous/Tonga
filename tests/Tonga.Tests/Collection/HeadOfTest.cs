using System;
using System.Collections.Generic;
using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Collection
{
    public sealed class HeadOfTest
    {

        [Fact]
        public void BehavesAsCollection()
        {
            Assert.Contains(
                new Tonga.Collection.Head<int>(
                    2,
                    AsEnumerable._(1, -1, 2, 0)
                ),
                predicate => predicate.Equals(-1));
        }

        [Fact]
        public void Size()
        {
            Assert.Equal(
                2,
                new Tonga.Collection.Head<string>(
                    2,
                    AsEnumerable._(
                        "hello", "world", "друг")
                ).Count);
        }

        [Fact]
        public void SizeEmptyReturnZero()
        {
            Assert.Empty(
                new Tonga.Collection.Head<int>(
                    2,
                    new List<int>()
                )
            );
        }

        [Fact]
        public void SizeLimitZeroReturnZero()
        {
            Assert.Empty(
                new Tonga.Collection.Head<string>(
                    0,
                    AsEnumerable._("1", "2", "3")
                )
            );
        }

        [Fact]
        public void WithItemsNotEmpty()
        {
            Assert.NotEmpty(
                new Tonga.Collection.Head<String>(
                    2,
                    AsEnumerable._("first", "second")
                )
            );
        }

        [Fact]
        public void WithoutItemsIsEmpty()
        {
            Assert.Empty(
                new Tonga.Collection.Head<String>(
                    0,
                    AsEnumerable._("third", "fourth")
                )
            );
        }

        [Fact]
        public void RejectsAdd()
        {
            Assert.Throws<InvalidOperationException>(() =>
            new Tonga.Collection.Head<int>(
                2,
                AsEnumerable._(1, 2, 3, 4)
            ).Add(6));
        }

        [Fact]
        public void RejectsRemove()
        {
            Assert.Throws<InvalidOperationException>(() =>
               new Tonga.Collection.Head<int>(
                   2,
                   AsEnumerable._(1, 2, 3, 4)
               ).Remove(1)
            );
        }

        [Fact]
        public void RejectsClear()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new Tonga.Collection.Head<int>(
                    2, AsEnumerable._(1, 2, 3, 4)
                ).Clear()
            );
        }
    }
}
