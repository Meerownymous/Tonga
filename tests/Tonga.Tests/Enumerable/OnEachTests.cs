using System.Collections.Generic;
using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable;

public sealed class OnEachTests
{
    [Fact]
    public void PassesTheItemToASingleParameterLambda()
    {
        var seen = new List<int>();
        (10, 20, 30).AsEnumerable().OnEach(item => seen.Add(item)).Length().Value();
        Assert.Equal([10, 20, 30], seen);
    }

    [Fact]
    public void CountsFromZero()
    {
        var seen = new List<int>();
        (10, 20, 30).AsEnumerable().OnEach((int _, int index) => seen.Add(index)).Length().Value();
        Assert.Equal([0, 1, 2], seen);
    }

    [Fact]
    public void RunsOnceForEveryItem()
    {
        var advanced = 0;
        ("a", "b", "c").AsEnumerable().OnEach(_ => advanced++).Length().Value();
        Assert.Equal(3, advanced);
    }

    [Fact]
    public void RunsTheParameterlessLambda()
    {
        var advanced = 0;
        (1, 2).AsEnumerable().OnEach(() => advanced++).Length().Value();
        Assert.Equal(2, advanced);
    }
}
