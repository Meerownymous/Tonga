using Tonga.Map;
using Xunit;

namespace Tonga.Tests.Map;

public sealed class AsDictionaryTests
{
    [Fact]
    public void ConvertsToDictionary()
    {
        Assert.Equal(
            "Rock",
            ("Castle", "Rock")
                .AsMap()
                .AsDictionary()
                ["Castle"]
        );
    }

    [Fact]
    public void OverwritesValue()
    {
        var dict =
            ("Castle", "Rock")
                .AsMap()
                .AsDictionary();

        dict["Castle"] = "Wolfenstein";

        Assert.Equal(
            "Wolfenstein",
            dict["Castle"]
        );
    }
}
