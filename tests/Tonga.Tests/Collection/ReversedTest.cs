using System;
using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Collection
{
    public sealed class ReversedTest
    {

        [Fact]
        public void BehavesAsCollection()
        {
            Assert.Equal(
                2,
                new ItemAt<int>(
                    new Tonga.Collection.Reversed<int>(
                        AsEnumerable._(0, -1, 2))
                ).Value()
            );
        }

        [Fact]
        public void ReversesList()
        {
            String last = "last";
            Assert.Equal(
                last,
                new ItemAt<string>(
                    new Tonga.Collection.Reversed<string>(
                        AsEnumerable._("item", last)
                    )
                ).Value()
            );
        }

        [Fact]
        public void ReversesEmptyList()
        {
            Assert.Empty(
                new Tonga.Collection.Reversed<string>(
                    new List<string>()));
        }

        [Fact]
        public void Size()
        {
            Assert.Equal(
                3,
                new Tonga.Collection.Reversed<string>(
                    AsEnumerable._("0", "1", "2")
                ).Count
            );
        }

        [Fact]
        public void NotEmpty()
        {
            Assert.NotEmpty(
                new Tonga.Collection.Reversed<int>(
                    AsEnumerable._(
                        6, 16
                    )
                )
            );
        }

        [Fact]
        public void Contains()
        {
            String word = "objects";

            Assert.Contains(
                word,
                new Tonga.Collection.Reversed<string>(
                    AsEnumerable._(
                        "hello", "elegant", word)
                    )
                );
        }

        [Fact]
        public void RejectsAdd()
        {
            Assert.Throws<InvalidOperationException>(() =>
              new Tonga.Collection.Reversed<int>(
                  AsEnumerable._(
                      1, 2, 3, 4)
              ).Add(6));
        }

        [Fact]
        public void RejectsRemove()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new Tonga.Collection.Reversed<int>(
                    AsEnumerable._(
                        1, 2, 3, 4
                    )
                ).Remove(1)
            );
        }



        [Fact]
        public void RejectsClear()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new Tonga.Collection.Reversed<int>(
                    AsEnumerable._(
                        1, 2, 3, 4)
                ).Clear()
            );
        }
    }
}
