using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Tonga.Enumerable;
using Tonga.Map;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Map;

public sealed class AsMapTests
{
    [Fact]
    public void IsThreadSafe()
    {
        IMap<string, string> oldMap = AsMap._();
        Parallel.For(0, 10000, (index) =>
        {
            oldMap = oldMap.With(AsPair._(index.ToString(), index.ToString()));
            oldMap.Keys();
            _ = oldMap[index.ToString()];
            _ = oldMap.Lazy(index.ToString())();
        });
        Assert.Equal(10000, oldMap.Keys().Count);
    }

    [Fact]
    public void MapsKeysToValues()
    {
        var one = AsPair._(45, 10);
        var two = AsPair._(33, 20);

        var map = AsMap._(one, two);

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
            AsMap._(
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
            AsMap._(
                AsMapInput._(AsPair._(12, 39478624)),
                AsMapInput._(AsPair._(24, 60208801))
            )[key]
        );
    }

    [Theory]
    [InlineData(9, 0)]
    [InlineData(10, 1)]
    public void BuildsFromPairs(int key, int value)
    {
        var m =
            AsMap._(
                AsPair._(9, 0),
                AsPair._(10, 1)
            );

        Assert.Equal(m[key], value);
    }

    [Fact]
    public void IgnoresChangesInOriginPairSequence()
    {
        int size = 1;
        var random = new Random();

        var map =
            AsMap._(
                Repeated._(
                    () => AsPair._(random.Next(), 1),
                    () =>
                    {
                        Interlocked.Increment(ref size);
                        return size;
                    }
                )
            );

        Assert.Equal(map.Keys(), map.Keys());
    }

    [Fact]
    public void SensesChangesInValues()
    {
        var map =
            AsMap._(
                AsPair._(123, () => new Random().NextInt64())
            );

        Assert.NotEqual(map[123], map[123]);
    }

    [Fact]
    public void BuildsOnlyRequestedValue()
    {
        Assert.Equal(
            "works",
            AsMap._(
                AsPair._<string, string>("name", () => throw new ApplicationException()),
                AsPair._("anothername", () => "works")
            )["anothername"]
        );
    }

    [Fact]
    public void WorksWithEmptyList()
    {
        var map = Empty._<int, int>();
        Assert.Equal(0, Length._(map.Pairs()).Value());
    }


    [Fact]
    public void BehavesAsMap()
    {
        var m =
            AsMap._(
                AsPair._("hello", "map"),
                AsPair._("goodbye", "dictionary")
            );

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
            AsMap._(
                "A", "B",
                "C", "D"
            )["A"]
        );
    }

    [Fact]
    public void RejectsOddValueCount()
    {
        Assert.Throws<ArgumentException>(() =>
            AsMap._(
                "A", "B",
                "C"
            )["A"]
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
                AsMapInput._(
                    AsPair._<string, string>(i.ToString(), Guid.NewGuid().ToString())
                )
            );
        }

        for (var i = 0; i < 100; i++)
        {
            inputs2.Add(
                AsMapInput._(
                    AsPair._<string, string>(i.ToString(), Guid.NewGuid().ToString())
                )
            );
        }

        var map1 = AsMap._(inputs);
        var map2 = AsMap._(inputs2);

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
