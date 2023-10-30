

using System.Collections.Generic;
using Xunit;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.Collection.Tests
{
    public sealed class FilteredTest
    {
        [Fact]
        public void BehavesAsCollection()
        {
            var col =
                Filtered._(i => i < 2, 1, 2, 0, -1);
            Assert.True(col.Contains(1) && col.Contains(-1));
        }

        [Fact]
        public void FiltersList()
        {
            Assert.Equal(
                2,
                Length._(
                    Filtered._(
                        input => input.Length > 4,
                        AsEnumerable._("hello", "world", "друг")
                    )
                ).Value()
            );
        }

        [Fact]
        public void FiltersEmptyList()
        {
            var col =
                Filtered._(
                    input => input.Length > 4,
                    new List<string>()
                );
            Assert.Empty(col);
        }

        [Fact]
        public void Size()
        {
            Assert.Equal(
                2,
                Filtered._(
                    input => input.Length >= 4,
                    AsEnumerable._("some", "text", "yes")
                ).Count
            );
        }

        [Fact]
        public void WithItemsNotEmpty()
        {
            Assert.NotEmpty(
                Filtered._(
                    input => input.Length > 4,
                    AsEnumerable._("first", "second")
                )
            );
        }

        [Fact]
        public void WithoutItemsIsEmpty()
        {
            Assert.Empty(
                Filtered._(
                    input => input.Length > 16,
                    AsEnumerable._("third", "fourth")
                )
            );
        }
    }
}
