using Tonga.Func;
using Tonga.IO;
using Tonga.List;
using Tonga.Map;
using Tonga.Number;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Map;

public sealed class GroupedTest
{
    [Fact]
    public void GroupsList()
    {
        var srcList = AsList._("ABC", "ABCD", "ABCDE");
        var keyFunc =
            new AsFunc<string, double>((str) =>
                new AsNumber(
                    Length._(new AsConduit(str)).Value()
                ).ToDouble()
            );

        var valueFunc =
            new AsFunc<string, string>((str) =>
                "ica" + str
            );
        Assert.Equal(
            "icaABCD",
            new Grouped<string, double, string>(srcList, keyFunc, valueFunc)[3.0][1]
        );
    }
}
