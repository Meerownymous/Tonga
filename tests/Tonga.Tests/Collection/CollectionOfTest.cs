

using System.Threading;
using Xunit;
using Tonga.Enumerable;

using Tonga.Scalar;
using System;

namespace Tonga.Collection.Tests
{
    public sealed class CollectionOfTest
    {
        [Fact]
        public void BehavesAsCollection()
        {
            Assert.Contains(
                -1,
                new CollectionOf<int>(
                    Enumerable.EnumerableOf.Pipe(1, 2, 0, -1))
                );
        }

        [Fact]
        public void IgnoresChangesInIterable()
        {
            int size = 2;
            var list =
                new CollectionOf<int>(
                    new Enumerable.Repeated<int>(
                        new Scalar.Live<int>(() => 0),
                        new Scalar.Live<int>(() =>
                        {
                            Interlocked.Increment(ref size);
                            return size;
                        })
                    ));

        }

        [Fact]
        public void DecoratesArray()
        {
            Assert.Equal(
                2,
                new CollectionOf<int>(-1, 0).Count);
        }

        [Fact]
        public void Empty()
        {
            Assert.Empty(
                new CollectionOf<int>());
        }

        [Fact]
        public void Contains()
        {
            Assert.Contains(
                2,
                new CollectionOf<int>(1, 2)
            );
        }

        [Fact]
        public void RejectsAdd()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new CollectionOf<int>(1, 2).Add(1));
        }

        [Fact]
        public void RejectsRemove()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new CollectionOf<int>(1, 2).Remove(1));
        }


        [Fact]
        public void RejectsClear()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new CollectionOf<int>(1, 2).Clear());
        }

    }
}
