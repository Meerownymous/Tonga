

using System.Collections.Generic;
using Xunit;
using Tonga.Enumerable;

namespace Tonga.Enumerator.Test
{
    public sealed class CachedTests
    {
        [Fact]
        public void DeliversMovementAbilityFromCache()
        {
            var advances = 0;
            var contents = new List<int>() { 1 };
            var enumerator =
                new Sticky<int>(
                    new Sticky<int>.Cache<int>(() => contents.GetEnumerator())
                );

            while (enumerator.MoveNext())
            {
                advances++;
            }

            contents.Clear();
            enumerator.Reset();
            Assert.True(enumerator.MoveNext());
        }

        [Fact]
        public void DeliversFromCache()
        {
            var advances = 0;
            var contents = new List<int>() { 1, 2, 3 };
            var enumerator =
                new Sticky<int>(
                    new Sticky<int>.Cache<int>(() => contents.GetEnumerator())
                );

            while (enumerator.MoveNext())
            {
                advances++;
            }

            contents.Clear();
            enumerator.Reset();

            var result = new List<int>();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);
            }
            Assert.Equal(new List<int>() { 1, 2, 3 }, result);
        }

        [Fact]
        public void CacheCachesItemCount()
        {
            var contents = new List<int>() { 1 };
            var cache = new Sticky<int>.Cache<int>(() => contents.GetEnumerator());

            var count = cache.Count;

            contents.Clear();

            Assert.True(cache.Count == 1);
        }
    }
}
