using Tonga.Map;
using Xunit;

namespace Tonga.Tests.Map;

public sealed class DeepMap
{
    [Fact]
    public void RetrievesValueByDiggingDown()
    {
        Assert.Equal(
            1,
            new DeepMap<string[], string, int>(
                digDown: key => key[0],
                (
                    (new[] { "one", "rubbish" }, 1).AsPair(),
                    (new[] { "two", "irrelevant stuff" }, 2).AsPair()
                ).AsMap()
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
                    (
                        (new[] { "one", "rubbish" }, 1).AsPair(),
                        (new[] { "two", "irrelevant stuff" }, 2).AsPair()
                    ).AsMap()
                )
                .With((new[] { "three", "trash" }, 3).AsPair())
                [["three", "otherthings"]]
        );
    }
}
