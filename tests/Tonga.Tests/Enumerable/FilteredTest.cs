

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Tonga.Tests;
using Tonga.Scalar;
using Tonga.List;

namespace Tonga.Enumerable.Test
{
    public sealed class FilteredTests
    {
        [Fact]
        public void Filters()
        {
            Assert.True(
                new LengthOf(
                    new Filtered<string>(
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
                    new List<string>() { "A", "B", "C" }
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
            Assert.True(
                LengthOf._(
                    Filtered._(
                        input => input.Length > 1,
                        None._<string>()
                    )
                ).Value() == 0,
                "cannot filter empty enumerable"
            );
        }

        [Fact]
        public void FiltersItemsGivenByParamsCtor()
        {
            Assert.Equal(
                2,
                LengthOf._(
                    Filtered._(
                       (input) => input != "B",
                       AsEnumerable._("A", "B", "C")
                    )
                ).Value()
            );
        }
    }
}
