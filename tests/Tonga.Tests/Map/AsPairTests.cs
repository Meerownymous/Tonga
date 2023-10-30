

using System;
using Xunit;

namespace Tonga.Map.Tests
{
    public sealed class AsPairTests
    {
        [Fact]
        public void SensesChanges()
        {
            var pair =
                AsPair._(1, () => Guid.NewGuid().ToString());
            Assert.NotEqual(pair.Value(), pair.Value());
        }

        [Fact]
        public void BuildsValue()
        {
            Assert.Equal(
                "value",
                AsPair._("key", () => "value").Value()
            );
        }

        [Fact]
        public void BuildsValueLazily()
        {
            Assert.Equal(
                "key",
                AsPair._<string, int>("key", () => throw new ApplicationException()).Key()
            );
        }

        [Fact]
        public void KnowsAboutBeingLazy()
        {
            Assert.True(AsPair._("2", () => "4").IsLazy());
        }

        [Fact]
        public void KnowsAboutBeingNotLazy()
        {
            Assert.False(AsPair._("2", "4").IsLazy());
        }
    }
}