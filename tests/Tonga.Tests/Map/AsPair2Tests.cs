

using System;
using Xunit;

namespace Tonga.Map.Tests
{
    public sealed class AsPairTests
    {
        [Fact]
        public void BuildsValue()
        {
            Assert.Equal(
                "value",
                new AsPair("key", () => "value").Value()
            );
        }

        [Fact]
        public void BuildsValueLazily()
        {
            Assert.Equal(
                "key",
                new AsPair("key", () => throw new ApplicationException()).Key()
            );
        }

        [Fact]
        public void KnowsAboutBeingLazy()
        {
            Assert.True(new AsPair("2", () => "4").IsLazy());
        }

        [Fact]
        public void KnowsAboutBeingNotLazy()
        {
            Assert.False(new AsPair("2", "4").IsLazy());
        }

        [Fact]
        public void BuildsValueTyped()
        {
            Assert.Equal(
                1,
                new AsPair<int>("key", () => 1).Value()
            );
        }

        [Fact]
        public void BuildsValueTypedLazily()
        {
            Assert.Equal(
                "key",
                new AsPair<int>("key", () => throw new ApplicationException()).Key()
            );
        }

        [Fact]
        public void KnowsAboutBeingLazyValue()
        {
            Assert.True(new AsPair<int>("2", () => 4).IsLazy());
        }

        [Fact]
        public void KnowsAboutBeingNotLazyValue()
        {
            Assert.False(new AsPair<int>("2", 4).IsLazy());
        }

        [Fact]
        public void BuildsKeyValueTyped()
        {
            Assert.Equal(
                1,
                new AsPair<int, int>(8, () => 1).Value()
            );
        }

        [Fact]
        public void BuildsKeyValueTypedLazily()
        {
            Assert.Equal(
                8,
                new AsPair<int, int>(8, () => throw new ApplicationException()).Key()
            );
        }

        [Fact]
        public void KnowsAboutBeingLazyKeyValue()
        {
            Assert.True(new AsPair<int, int>(2, () => 4).IsLazy());
        }

        [Fact]
        public void KnowsAboutBeingNotLazyKeyValue()
        {
            Assert.False(new AsPair<int, int>(2, 4).IsLazy());
        }
    }
}
