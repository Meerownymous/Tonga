using System;
using Xunit;

namespace Tonga.Map.Tests
{
    public sealed class LazyMap2Tests
    {
        [Fact]
        public void SensesChanges()
        {
            var map =
                LazyMap2._(
                    rejectBuildingAllValues: false,
                    AsPair2._("one", () => Guid.NewGuid().ToString())
                );

            Assert.NotEqual(
                map["one"], map["one"]
            );
        }

        [Fact]
        public void AllowsValueEnumerationWhenPreventionIsDisabled()
        {
            Assert.True(
                LazyMap2._(
                    rejectBuildingAllValues: false,
                    AsPair2._("one", () => 1)
                ).Values.GetEnumerator().MoveNext()
            );
        }

        [Fact]
        public void AllowsPairEnumerationWhenPreventionIsDisabled()
        {
            Assert.True(
                LazyMap2._(
                    rejectBuildingAllValues: false,
                    AsPair2._("one", () => 1)
                ).GetEnumerator().MoveNext()
            );
        }

        [Fact]
        public void AllowsPairEnumerationWhenPairsNotLazy()
        {
            Assert.True(
                LazyMap2._(
                    rejectBuildingAllValues: true,
                    AsPair2._("one", 1)
                ).GetEnumerator().MoveNext()
            );
        }

        [Fact]
        public void RejectsEnumeratingValues()
        {
            var ex =
                Assert.Throws<InvalidOperationException>(() =>
                    LazyMap2._(
                        rejectBuildingAllValues: true,
                        AsPair2._("one", () => 1)
                    ).Values.GetEnumerator()
                );
            Assert.StartsWith("Cannot get all values because this is a lazy dictionary.", ex.Message);
        }

        [Fact]
        public void RejectsEnumeratingPairs()
        {
            var ex =
                Assert.Throws<InvalidOperationException>(() =>
                    LazyMap2._(
                        rejectBuildingAllValues: true,
                        AsPair2._("one", () => 1)
                    ).GetEnumerator()
                );
            Assert.StartsWith("Cannot get the enumerator because this is a lazy dictionary.", ex.Message);
        }

        [Fact]
        public void EnumeratesWhenEmpty()
        {
            Assert.False(
                LazyMap2._<int,int>().GetEnumerator().MoveNext()
            );
        }
    }
}
