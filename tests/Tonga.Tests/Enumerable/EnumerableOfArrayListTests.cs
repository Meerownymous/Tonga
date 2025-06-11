using System.Collections;
using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable;

public class EnumerableOfArrayListTests
{
    [Fact]
    public void BuildsFromStrings() =>

        Assert.Equal(
            "A",
            new EnumerableOfArrayList(
                    new ArrayList{ "A", "B", "C" }
                ).First()
                .Value()
        );
}
