using System;
using System.Diagnostics;
using Xunit;

namespace Tonga.Map.Tests
{
    public sealed class DeepMap
    {
        [Fact]
        public void RetrievesValueByDiggingDown()
        {
            Assert.Equal(
                1,
                new DeepMap<string[], string, int>(
                    digDown: key => key[0],
                    AsMap._(
                        AsPair._(new string[] { "one", "rubbish" }, 1),
                        AsPair._(new string[] { "two", "irrelevant stuff" }, 2)
                    )
                )[new string[] { "one", "otherthings" }]
            );
        }

        [Fact]
        public void CanBeRefined()
        {
            Assert.Equal(
                3,
                new DeepMap<string[], string, int>(
                    digDown: key => key[0],
                    AsMap._(
                        AsPair._(new string[] { "one", "rubbish" }, 1),
                        AsPair._(new string[] { "two", "irrelevant stuff" }, 2)
                    )
                )
                .With(AsPair._(new string[] { "three", "trash" }, 3))
                [new string[] { "three", "otherthings" }]
            );
        }
    }
}

