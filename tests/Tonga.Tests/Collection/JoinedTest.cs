

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
                new ManyOf<int>(1, -1, 2, 0),
                new ManyOf<int>(1, -1, 2, 0),
                new ManyOf<int>(1, -1, 2, 0)
            );
        }

        [Fact]
        public void Size()
        {
            Assert.Equal(
                8,
                new Joined<String>(
                    new ManyOf<string>("hello", "world", "друг"),
                    new ManyOf<string>("how", "are", "you"),
                    new ManyOf<string>("what's", "up")
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
                    new ManyOf<string>("1", "2"),
                    new ManyOf<string>("3", "4")
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
                 new ManyOf<int>(1, 2),
                 new ManyOf<int>(3, 4),
                 new ManyOf<int>(5, 6)
             ).Add(7));
        }

        [Fact]
        public void RejectsRemove()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new Joined<String>(
                    new ManyOf<string>("w", "a"),
                    new ManyOf<string>("b", "c")
                ).Remove("t"));
        }

        [Fact]
        public void RejectsClear()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new Joined<int>(
                    new ManyOf<int>(10),
                    new ManyOf<int>(20)
                ).Clear());
        }
    }
}
