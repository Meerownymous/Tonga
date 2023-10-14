

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
                Params.Of(1, -1, 2, 0),
                Params.Of(1, -1, 2, 0),
                Params.Of(1, -1, 2, 0)
            );
        }

        [Fact]
        public void Size()
        {
            Assert.Equal(
                8,
                new Joined<String>(
                    Params.Of("hello", "world", "друг"),
                    Params.Of("how", "are", "you"),
                    Params.Of("what's", "up")
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
                    Params.Of("1", "2"),
                    Params.Of("3", "4")
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
                 Params.Of(1, 2),
                 Params.Of(3, 4),
                 Params.Of(5, 6)
             ).Add(7));
        }

        [Fact]
        public void RejectsRemove()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new Joined<String>(
                    Params.Of("w", "a"),
                    Params.Of("b", "c")
                ).Remove("t"));
        }

        [Fact]
        public void RejectsClear()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new Joined<int>(
                    Params.Of(10),
                    Params.Of(20)
                ).Clear());
        }
    }
}
