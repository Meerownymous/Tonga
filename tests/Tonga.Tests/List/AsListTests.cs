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
            AsList._(
                Head._(
                    Endless._(1),
                    () => Interlocked.Increment(ref size)
                )
            );

        Assert.NotEqual(
            Length._(list).Value(),
            Length._(list).Value()
        );
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
            AsList._(
                Lambda._(() => advances++,
                    AsEnumerable._("item1", "item2", "item3")
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
            AsList._("item1", "item2", "item3")
                .IndexOf("item3")
        );
    }

    [Fact]
    public void DeliversIndexWhenNoFinding()
    {
        Assert.Equal(
            -1,
            AsList._("item1", "item2", "item3")
                .IndexOf("item100")
        );
    }

    [Fact]
    public void CanCopyTo()
    {
        var array = new string[5];
        var origin = AsList._("item1", "item2", "item3");
        origin.CopyTo(array, 2);

        Assert.Equal(
            new string[] { null, null, "item1", "item2", "item3" },
            array
        );
    }

    [Fact]
    public void ContainsWorksWithEmptyList()
    {
        Assert.DoesNotContain(
            "item",
            None._<string>()
        );
    }
}
