using Tonga.Map;
using Xunit;

namespace Tonga.Tests.Map
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
                        (["one", "rubbish"], 1),
                        (new[] { "two", "irrelevant stuff" }, 2)
                    )
                )[["one", "otherthings"]]
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
                        (new[] { "one", "rubbish" }, 1),
                        (new[] { "two", "irrelevant stuff" }, 2)
                    )
                )
                .With(AsPair._(new[] { "three", "trash" }, 3))
                [["three", "otherthings"]]
            );
        }
    }
}

