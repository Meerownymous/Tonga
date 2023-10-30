using System.Collections.Generic;
using Tonga.List;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Enumerable.Test
{
    public sealed class FilteredTests
    {
        [Fact]
        public void Filters()
        {
            Assert.True(
                Length._(
                    Filtered._(
                       (input) => input != "B",
                       new List<string>() { "A", "B", "C" }
                    )
                ).Value() == 2,
                "cannot filter items"
            );
        }

        [Fact]
        public void SensesChanges()
        {
            var filterings = 0;
            var filtered =
                Filtered._(
                    (input) =>
                    {
                        filterings++;
                        return input != "B";
                    },
                    AsList._("A", "B", "C")
                );

            var enm1 = filtered.GetEnumerator();
            enm1.MoveNext();
            var current = enm1.Current;

            var enm2 = filtered.GetEnumerator();
            enm2.MoveNext();
            var current2 = enm2.Current;

            Assert.Equal(2, filterings);
        }

        [Fact]
        public void FiltersEmptyList()
        {
            Assert.Empty(
                Filtered._(
                    input => input.Length > 1,
                    None._<string>()
                )
            );
        }

        [Fact]
        public void FiltersItemsGivenByParamsCtor()
        {
            Assert.Equal(
                2,
                Length._(
                    Filtered._(
                       (input) => input != "B",
                       AsEnumerable._("A", "B", "C")
                    )
                ).Value()
            );
        }
    }
}
