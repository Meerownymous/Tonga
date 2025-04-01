using System.Collections;
using Tonga.Enumerable;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.List;

public class ManyOfArrayListTest
{
    [Fact]
    public void BuildsFromStrings()
    {
        var arr = new ArrayList() { "A", "B", "C" };

        Assert.Equal("A",
            new ItemAt<object>(
                new EnumerableOfArrayList(arr)
            ).Value().ToString()
        );
    }
}
