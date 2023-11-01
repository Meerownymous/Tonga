

using System.Threading;
using Xunit;
using Tonga.Enumerable;

using Tonga.Scalar;
using System;

namespace Tonga.Collection.Tests
{
    public sealed class AsCollectionTest
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
                new AsCollection<int>(-1, 0).Count
            );
        }

        [Fact]
        public void CanBeEmpty()
        {
            Assert.Empty(
                new AsCollection<int>()
            );
        }

        [Fact]
        public void Contains()
        {
            Assert.Contains(
                2,
                new AsCollection<int>(1, 2)
            );
        }

        [Fact]
        public void RejectsAdd()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new AsCollection<int>(1, 2).Add(1)
            );
        }

        [Fact]
        public void RejectsRemove()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new AsCollection<int>(1, 2).Remove(1)
            );
        }


        [Fact]
        public void RejectsClear()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new AsCollection<int>(1, 2).Clear()
            );
        }

        [Fact]
        public void BuildsCollection()
        {
            Assert.Contains(
                -1,
                new AsCollection<int>(1, 2, 0, -1)
            );
        }

        [Fact]
        public void BuildsCollectionFromEnumerator()
        {
            Assert.Contains(
                -1,
                AsCollection._(
                    AsEnumerable._(1, 2, 0, -1).GetEnumerator())
            );
        }

        [Fact]
        public void SensesChanges()
        {
            var count = 1;
            var col =
                AsCollection._(() =>
                    Repeated._(
                        () =>
                        {
                            count++;
                            return 0;
                        },
                        count
                    ).GetEnumerator()
                );
            Assert.NotEqual(col.Count, col.Count);
        }
    }
}
