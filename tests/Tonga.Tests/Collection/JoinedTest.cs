using System;
using System.Collections.Generic;
using Tonga.Enumerable;
using Xunit;
using Joined = Tonga.Collection.Joined;

namespace Tonga.Tests.Collection
{
    public sealed class JoinedTest
    {
        [Fact]
        public void BehavesAsCollection()
        {
            new Tonga.Collection.Joined<int>(
                AsEnumerable._(1, -1, 2, 0),
                AsEnumerable._(1, -1, 2, 0),
                AsEnumerable._(1, -1, 2, 0)
            );
        }

        [Fact]
        public void Size()
        {
            Assert.Equal(
                8,
                Joined._(
                    AsEnumerable._("hello", "world", "друг"),
                    AsEnumerable._("how", "are", "you"),
                    AsEnumerable._("what's", "up")
                ).Count);
        }

        [Fact]
        public void SizeEmptyReturnZero()
        {
            Assert.Empty(
                new Tonga.Collection.Joined<String>(
                    new List<string>()
                ));
        }

        [Fact]
        public void WithItemsNotEmpty()
        {
            Assert.NotEmpty(
                new Tonga.Collection.Joined<String>(
                    AsEnumerable._("1", "2"),
                    AsEnumerable._("3", "4")
                ));
        }

        [Fact]
        public void WithoutItemsIsEmpty()
        {
            Assert.Empty(
                new Tonga.Collection.Joined<String>(
                    new List<string>()));
        }

        [Fact]
        public void RejectsAdd()
        {
            Assert.Throws<InvalidOperationException>(() =>
             new Tonga.Collection.Joined<int>(
                 AsEnumerable._(1, 2),
                 AsEnumerable._(3, 4),
                 AsEnumerable._(5, 6)
             ).Add(7));
        }

        [Fact]
        public void RejectsRemove()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new Tonga.Collection.Joined<String>(
                    AsEnumerable._("w", "a"),
                    AsEnumerable._("b", "c")
                ).Remove("t"));
        }

        [Fact]
        public void RejectsClear()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new Tonga.Collection.Joined<int>(
                    AsEnumerable._(10),
                    AsEnumerable._(20)
                ).Clear());
        }
    }
}
