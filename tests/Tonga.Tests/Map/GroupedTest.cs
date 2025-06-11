using System;
using Tonga.List;
using Tonga.Map;
using Xunit;

namespace Tonga.Tests.Map;

public sealed class GroupedTest
{
    [Fact]
    public void GroupsList()
    {
        var srcList = ("ABC", "ABCD", "ABCDE").AsList();
        var keyFunc =
            new Func<string, double>(str => str.Length);

        var valueFunc =
            new Func<string, string>(str => "ica" + str);

        Assert.Equal(
            "icaABCD",
            new Grouped<string, double, string>(srcList, keyFunc, valueFunc)[3.0][1]
        );
    }
}
