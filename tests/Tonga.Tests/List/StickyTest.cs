using System;
using Tonga.Enumerable;
using Xunit;

namespace Tonga.List.Tests
{
    public sealed class StickyTest
    {
        [Fact]
        public void AdvancesOnlyWhenAsked()
        {
            var advances = 0;
            var sticky =
                Sticky._(
                    Lambda.From(() => advances++,
                        AsEnumerable._("one", "two", "three")
                    )
                );
            sticky.GetEnumerator().MoveNext();

            Assert.Equal(1, advances);
        }

        [Fact]
        public void AdvancesOnlyToIndex()
        {
            var advances = 0;
            var sticky =
                Sticky._(
                    Lambda.From(() => advances++,
                        AsEnumerable._("one", "two", "three")
                    )
                );
            _ = sticky[1];

            Assert.Equal(2, advances);
        }

        [Fact]
        public void AdvancesOnlyToItemForContains()
        {
            var advances = 0;
            var sticky =
                Sticky._(
                    Lambda.From(() => advances++,
                        AsEnumerable._("one", "two", "three")
                    )
                );
            _ = sticky.Contains("two");

            Assert.Equal(2, advances);
        }

        [Fact]
        public void AdvancesAllForCount()
        {
            var advances = 0;
            var sticky =
                Sticky._(
                    Lambda.From(() => advances++,
                        AsEnumerable._("one", "two", "three")
                    )
                );
            _ = sticky.Count;

            Assert.Equal(3, advances);
        }

        [Fact]
        public void MemoizesSeenItems()
        {
            var advances = 0;
            var sticky =
                Sticky._(
                    Lambda.From(() => advances++,
                        AsEnumerable._("one", "two", "three")
                    )
                );
            _ = sticky.Count;
            _ = sticky[2];

            Assert.Equal(3, advances);
        }
    }
}