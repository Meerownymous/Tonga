

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
                new Filtered<int>(i => i < 2, 1, 2, 0, -1);
            Assert.True(col.Contains(1) && col.Contains(-1));
        }

        [Fact]
        public void FiltersList()
        {
            Assert.Equal(
                2,
                new LengthOf(
                    new Filtered<string>(
                        input => input.Length > 4,
                        EnumerableOf.Pipe("hello", "world", "друг")
                    )
                ).Value()
            );
        }

        [Fact]
        public void FiltersEmptyList()
        {
            var col =
                new Filtered<string>(
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
                new Filtered<string>(
                    input => input.Length >= 4,
                    EnumerableOf.Pipe("some", "text", "yes")
                ).Count
            );
        }

        [Fact]
        public void WithItemsNotEmpty()
        {
            Assert.NotEmpty(
                new Filtered<string>(
                    input => input.Length > 4,
                    EnumerableOf.Pipe("first", "second")
                )
            );
        }

        [Fact]
        public void WithoutItemsIsEmpty()
        {
            Assert.Empty(
                new Filtered<string>(
                    input => input.Length > 16,
                    EnumerableOf.Pipe("third", "fourth")
                )
            );
        }
    }
}
