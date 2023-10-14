

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Tonga.Tests;

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
        public void CachesFilterResult()
        {
            var filterings = 0;
            var filtered =
                new Filtered<string>(
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

            Assert.Equal(1, filterings);
        }

        [Fact]
        public void FiltersEmptyList()
        {
            Assert.True(
                new LengthOf(
                    new Filtered<string>(
                        input => input.Length > 1,
                        new None()
                    )
                ).Value() == 0,
                "cannot filter empty enumerable"
            );
        }

        [Fact]
        public void PerformanceMatchesLinQ()
        {
            Func<string, bool> filter = (input) => input != "B";

            var linq = new ElapsedTime(() => new List<string>() { "A", "B", "C" }.Where(filter)).AsTimeSpan();
            var atoms =
                new ElapsedTime(
                    () => new Filtered<string>(
                        filter,
                        new List<string>() { "A", "B", "C" }
                    )
                ).AsTimeSpan();

            Assert.True((linq - atoms).Duration().Milliseconds < 10);
        }

        [Fact]
        public void FiltersItemsGivenByParamsCtor()
        {
            Assert.True(
                new LengthOf(
                    new Filtered<string>(
                       (input) => input != "B",
                       "A", "B", "C")
                ).Value() == 2,
                "cannot filter items"
            );
        }

        [Fact]
        public void IsSticky()
        {
            var calls = 0;

            var enm =
                new Filtered<string>(
                    (i) => { Debug.WriteLine("Read"); return true; },
                    new List<string>() { "A" }
                );

            var enmr1 = enm.GetEnumerator();
            var enmr2 = enm.GetEnumerator();

            enmr1.MoveNext();
            enmr2.MoveNext();


            var enumerable =
                new Filtered<string>(
                    (input) =>
                    {
                        calls++;
                        return input != "B";
                    },
                    new List<string>() { "A", "B", "C" }
                );

            new LengthOf(enumerable).Value();
            new LengthOf(enumerable).Value();

            Assert.Equal(
                3,
                calls
            );
        }
    }
}
