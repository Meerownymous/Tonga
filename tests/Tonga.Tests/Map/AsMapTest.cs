using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Tonga.Enumerable;
using Tonga.Map;
using Xunit;

namespace Tonga.Tests.Map;

public sealed class AsMapTests
{
    [Fact]
    public void IsThreadSafe()
    {
        IMap<int, int> oldMap = new Empty<int, int>();
        Parallel.For(0, 10000, index =>
            {
                oldMap = oldMap.With((index, index).AsPair());
                oldMap.Keys();
                _ = oldMap[index];
                _ = oldMap.Lazy(index)();
            }
        );
        Assert.Equal(10000, oldMap.Keys().Count);
    }

    [Fact]
    public void MapsKeysToValues()
    {
        var one = (45, 10).AsPair();
        var two = (33, 20).AsPair();

        var map =
            new Empty<int, int>()
                .With(one)
                .With(two);

        Assert.Equal(
            20,
            map[33]
        );
    }

    [Fact]
    public void BuildsFromTuples()
    {
        Assert.Equal(
            20,
            new AsMap<int,int>(
                (45, 10),
                (33, 20)
            )[33]
        );
    }

    [Theory]
    [InlineData(12, 39478624)]
    [InlineData(24, 60208801)]
    public void BuildsFromInputs(int key, int value)
    {
        Assert.Equal(
            value,
            new AsMap<int,int>(
                (12, 39478624).AsPair().AsMapInput(),
                (24, 60208801).AsPair().AsMapInput()
            )[key]
        );
    }

    [Theory]
    [InlineData(9, 0)]
    [InlineData(10, 1)]
    public void BuildsFromPairs(int key, int value)
    {
        var m =
            (
                (9, 0).AsPair(),
                (10, 1).AsPair()
            ).AsMap();

        Assert.Equal(m[key], value);
    }

    [Fact]
    public void IgnoresChangesInOriginPairSequence()
    {
        int size = 1;
        var random = new Random();

        var map =
            new Repeated<IPair<int,int>>(
                () => (random.Next(), 1).AsPair(),
                () =>
                {
                    Interlocked.Increment(ref size);
                    return size;
                }
            ).AsMap();

        Assert.Equal(map.Keys(), map.Keys());
    }

    [Fact]
    public void SensesChangesInValues()
    {
        var map =
            new AsPair<int,long>(
                123, () => new Random().NextInt64()
            ).AsMap();

        Assert.NotEqual(map[123], map[123]);
    }

    [Fact]
    public void BuildsOnlyRequestedValue()
    {
        Assert.Equal(
            "works",
            new AsMap<string,string>(
                new AsPair<string, string>("name", () => throw new ApplicationException()),
                new AsPair<string, string>("anothername", () => "works")
            )["anothername"]
        );
    }

    [Fact]
    public void WorksWithEmptyList()
    {
        var map = new Empty<int, int>();
        Assert.Equal(0, map.Pairs().Length().Value());
    }

    [Fact]
    public void BehavesAsMap()
    {
        var m =
            (
                ("hello", "map").AsPair(),
                ("goodbye", "dictionary").AsPair()
            ).AsMap();

        Assert.Equal(
            "dictionary",
            m["goodbye"]
        );
    }

    [Fact]
    public void BuildsFromPairParams()
    {
        Assert.Equal(
            "B",
            (
                ("A", "B").AsPair(),
                ("C", "D").AsPair()
            ).AsMap()["A"]
        );
    }

    [Fact]
    public void BuildsFromInputsFasterThanMap()
    {
        var inputs = new List<IMapInput<string, string>>();
        var inputs2 = new List<IMapInput<string, string>>();

        for (var i = 0; i < 100; i++)
        {
            inputs.Add(
                (i.ToString(), Guid.NewGuid().ToString()).AsMapInput()
            );
        }

        for (var i = 0; i < 100; i++)
        {
            inputs2.Add(
                (i.ToString(), Guid.NewGuid().ToString()).AsMapInput()
            );
        }

        var map1 = inputs.AsMap();
        var map2 = inputs2.AsMap();

        Debug.WriteLine(
            new ElapsedTime(() =>
                _ = map1["87"]
            ).AsTimeSpan().TotalMilliseconds
            + " vs " +
            new ElapsedTime(() =>
                _ = map2["87"]
            ).AsTimeSpan().TotalMilliseconds
        );
    }
}
