

using System;
using System.Collections.Generic;
using Xunit;
using Tonga.Enumerable;


namespace Tonga.Collection.Tests
{
    public sealed class HeadOfTest
    {

        [Fact]
        public void BehavesAsCollection()
        {
            Assert.Contains(
                new HeadOf<int>(
                    2,
                    EnumerableOf.Pipe(1, -1, 2, 0)
                ),
                predicate => predicate.Equals(-1));
        }

        [Fact]
        public void Size()
        {
            Assert.Equal(
                2,
                new HeadOf<string>(
                    2,
                    EnumerableOf.Pipe(
                        "hello", "world", "друг")
                ).Count);
        }

        [Fact]
        public void SizeEmptyReturnZero()
        {
            Assert.Empty(
                new HeadOf<int>(
                    2,
                    new List<int>()
                )
            );
        }

        [Fact]
        public void SizeLimitZeroReturnZero()
        {
            Assert.Empty(
                new HeadOf<string>(
                    0,
                    EnumerableOf.Pipe("1", "2", "3")
                )
            );
        }

        [Fact]
        public void WithItemsNotEmpty()
        {
            Assert.NotEmpty(
                new HeadOf<String>(
                    2,
                    EnumerableOf.Pipe("first", "second")
                )
            );
        }

        [Fact]
        public void WithoutItemsIsEmpty()
        {
            Assert.Empty(
                new HeadOf<String>(
                    0,
                    EnumerableOf.Pipe("third", "fourth")
                )
            );
        }

        [Fact]
        public void RejectsAdd()
        {
            Assert.Throws<InvalidOperationException>(() =>
            new HeadOf<int>(
                2,
                EnumerableOf.Pipe(1, 2, 3, 4)
            ).Add(6));
        }

        [Fact]
        public void RejectsRemove()
        {
            Assert.Throws<InvalidOperationException>(() =>
               new HeadOf<int>(
                   2,
                   EnumerableOf.Pipe(1, 2, 3, 4)
               ).Remove(1)
            );
        }

        [Fact]
        public void RejectsClear()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new HeadOf<int>(
                    2, EnumerableOf.Pipe(1, 2, 3, 4)
                ).Clear()
            );
        }
    }
}
