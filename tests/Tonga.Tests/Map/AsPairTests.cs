

using System;
using Xunit;

namespace Tonga.Map.Tests
{
    public sealed class AsPair2Tests
    {
        [Fact]
        public void SensesChanges()
        {
            var pair =
                AsPair2._(1, () => Guid.NewGuid().ToString());
            Assert.NotEqual(pair.Value(), pair.Value());
        }

        [Fact]
        public void BuildsValue()
        {
            Assert.Equal(
                "value",
                AsPair2._("key", () => "value").Value()
            );
        }

        [Fact]
        public void BuildsValueLazily()
        {
            Assert.Equal(
                "key",
                AsPair2._<string, string>("key", () => throw new ApplicationException()).Key()
            );
        }

        [Fact]
        public void KnowsAboutBeingLazy()
        {
            Assert.True(
                AsPair2._("2", () => "4").IsLazy()
            );
        }

        [Fact]
        public void KnowsAboutNotBeingLazy()
        {
            Assert.False(
                AsPair2._("2", "4").IsLazy()
            );
        }
    }
}
