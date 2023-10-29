

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
                AsCollection._(
                    AsEnumerable._(1, 2, 0, -1)
                )
            );
        }

        [Fact]
        public void IgnoresChangesInIterable()
        {
            int size = 2;
            var list =
                AsCollection._(
                    Repeated._(
                        AsScalar._(() => 0),
                        AsScalar._(() =>
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
                AsCollection._(-1, 0).Count
            );
        }

        [Fact]
        public void Empty()
        {
            Assert.Empty(
                AsCollection._<int>()
            );
        }

        [Fact]
        public void Contains()
        {
            Assert.Contains(
                2,
                AsCollection._(1, 2)
            );
        }

        [Fact]
        public void RejectsAdd()
        {
            Assert.Throws<InvalidOperationException>(() =>
                AsCollection._(1, 2).Add(1)
            );
        }

        [Fact]
        public void RejectsRemove()
        {
            Assert.Throws<InvalidOperationException>(() =>
                AsCollection._(1, 2).Remove(1)
            );
        }


        [Fact]
        public void RejectsClear()
        {
            Assert.Throws<InvalidOperationException>(() =>
                AsCollection._(1, 2).Clear()
            );
        }

    }
}
