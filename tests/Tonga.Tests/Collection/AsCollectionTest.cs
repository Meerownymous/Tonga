using System;
using System.Threading;
using Tonga.Collection;
using Tonga.Enumerable;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Collection
{
    public sealed class AsCollectionTest
    {
        [Fact]
        public void BehavesAsCollection()
        {
            Assert.Contains(
                -1,
                new[] { 1, 2, 0, -1 }.AsCollection()
            );
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
                new AsEnumerable<int>(1, 2, 0, -1)
                    .GetEnumerator()
                    .AsCollection()
            );
        }

        [Fact]
        public void SensesChanges()
        {
            var count = 1;
            var col =
                count
                    .AsRepeated(
                        repeats: () =>
                        {
                            Interlocked.Increment(ref count);
                            return count;
                        })
                    .AsCollection();

            Assert.NotEqual(col.Count, col.Count);
        }
    }
}
