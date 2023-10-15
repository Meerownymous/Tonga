

using System;
using System.Collections.Generic;
using Xunit;
using Tonga.Enumerable;


namespace Tonga.Collection.Tests
{
    public sealed class JoinedTest
    {
        [Fact]
        public void BehavesAsCollection()
        {
            new Joined<int>(
                EnumerableOf.Pipe(1, -1, 2, 0),
                EnumerableOf.Pipe(1, -1, 2, 0),
                EnumerableOf.Pipe(1, -1, 2, 0)
            );
        }

        [Fact]
        public void Size()
        {
            Assert.Equal(
                8,
                new Joined<String>(
                    EnumerableOf.Pipe("hello", "world", "друг"),
                    EnumerableOf.Pipe("how", "are", "you"),
                    EnumerableOf.Pipe("what's", "up")
                ).Count);
        }

        [Fact]
        public void SizeEmptyReturnZero()
        {
            Assert.Empty(
                new Joined<String>(
                    new List<string>()
                ));
        }

        [Fact]
        public void WithItemsNotEmpty()
        {
            Assert.NotEmpty(
                new Joined<String>(
                    EnumerableOf.Pipe("1", "2"),
                    EnumerableOf.Pipe("3", "4")
                ));
        }

        [Fact]
        public void WithoutItemsIsEmpty()
        {
            Assert.Empty(
                new Joined<String>(
                    new List<string>()));
        }

        [Fact]
        public void RejectsAdd()
        {
            Assert.Throws<InvalidOperationException>(() =>
             new Joined<int>(
                 EnumerableOf.Pipe(1, 2),
                 EnumerableOf.Pipe(3, 4),
                 EnumerableOf.Pipe(5, 6)
             ).Add(7));
        }

        [Fact]
        public void RejectsRemove()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new Joined<String>(
                    EnumerableOf.Pipe("w", "a"),
                    EnumerableOf.Pipe("b", "c")
                ).Remove("t"));
        }

        [Fact]
        public void RejectsClear()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new Joined<int>(
                    EnumerableOf.Pipe(10),
                    EnumerableOf.Pipe(20)
                ).Clear());
        }
    }
}
