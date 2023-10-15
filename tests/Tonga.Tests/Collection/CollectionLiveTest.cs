

using Xunit;
using Tonga.Enumerable;

namespace Tonga.Collection.Tests
{
    public sealed class LiveTest
    {
        [Fact]
        public void BehavesAsCollection()
        {
            var col = new LiveCollection<int>(1, 2, 0, -1);

            Assert.True(col.Contains(1) && col.Contains(2) && col.Contains(0) && col.Contains(-1));
        }

        [Fact]
        public void BuildsCollection()
        {
            Assert.Contains(
                -1,
                new LiveCollection<int>(1, 2, 0, -1)
            );
        }

        [Fact]
        public void BuildsCollectionFromIterator()
        {
            Assert.Contains(
                -1,
                new LiveCollection<int>(
                    EnumerableOf.Pipe(1, 2, 0, -1).GetEnumerator())
            );
        }

        [Fact]
        public void SensesChanges()
        {
            var count = 1;
            var col =
                new LiveCollection<int>(
                    Enumerable.EnumerableOf.Pipe(() =>
                        new Repeated<int>(
                            () =>
                            {
                                count++;
                                return 0;
                            },
                            count
                        ).GetEnumerator()
                    )
                );
            Assert.NotEqual(col.Count, col.Count);
        }
    }
}
