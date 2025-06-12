using System.Threading;
using Tonga.Enumerable;
using Tonga.List;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.List;

public sealed class ListOfTest
{
    [Fact]
    public void SensesChangesInList()
    {
        int size = 2;
        var list =
            new AsList<int>(
                new Head<int>(
                    new Endless<int>(1),
                    () => Interlocked.Increment(ref size)
                )
            );

        Assert.NotEqual(list.Length().Value(), list.Length().Value());
    }

    [Fact]
    public void ContainsWorksWithFirstItem()
    {
        var list = new AsList<string>("item");
        Assert.Contains("item", list);
    }

    [Fact]
    public void ContainsWorksWithHigherItem()
    {
        var list = new AsList<string>("item1", "item2", "item3");
        Assert.Contains("item2", list);
    }

    [Fact]
    public void CountingAdvancesAll()
    {
        var advances = 0;
        var list =
            new AsList<string>(
                new OnEach<string>(
                    () => advances++,
                    ("item1", "item2", "item3").AsEnumerable()
                )
            );

        var count = list.Count;

        Assert.Equal(3, advances);

    }

    [Fact]
    public void FindsIndexOf()
    {
        Assert.Equal(
            2,
            new AsList<string>("item1", "item2", "item3")
                .IndexOf("item3")
        );
    }

    [Fact]
    public void DeliversIndexWhenNoFinding()
    {
        Assert.Equal(
            -1,
            new AsList<string>("item1", "item2", "item3")
                .IndexOf("item100")
        );
    }

    [Fact]
    public void CanCopyTo()
    {
        var array = new string[5];
        var origin = new AsList<string>("item1", "item2", "item3");
        origin.CopyTo(array, 2);

        Assert.Equal(
            new[] { null, null, "item1", "item2", "item3" },
            array
        );
    }

    [Fact]
    public void ContainsWorksWithEmptyList()
    {
        Assert.DoesNotContain(
            "item",
            new None<string>()
        );
    }
}
