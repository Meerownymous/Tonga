using System;
using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.Number;

/// <summary>
/// The maximum of the given numbers
/// </summary>
public sealed class MaxOf : NumberEnvelope
{
    /// <summary>
    /// The maximum of the given int values.
    /// </summary>
    public MaxOf(params int[] src) : this(ToDoubles(src)) { }

    /// <summary>
    /// The maximum of the given int values.
    /// </summary>
    public MaxOf(IEnumerable<int> src) : this(ToDoubles(src)) { }

    /// <summary>
    /// The maximum of the given double values.
    /// </summary>
    public MaxOf(params double[] src) : this((IEnumerable<double>)src) { }

    /// <summary>
    /// The maximum of the given double values.
    /// </summary>
    public MaxOf(IEnumerable<double> src)
        : base(ScalarsFrom(src).Item1, ScalarsFrom(src).Item2, ScalarsFrom(src).Item3, ScalarsFrom(src).Item4)
    { }

    /// <summary>
    /// The maximum of the given long values.
    /// </summary>
    public MaxOf(params long[] src) : this(ToDoubles(src)) { }

    /// <summary>
    /// The maximum of the given long values.
    /// </summary>
    public MaxOf(IEnumerable<long> src) : this(ToDoubles(src)) { }

    /// <summary>
    /// The maximum of the given float values.
    /// </summary>
    public MaxOf(params float[] src) : this(ToDoubles(src)) { }

    /// <summary>
    /// The maximum of the given float values.
    /// </summary>
    public MaxOf(IEnumerable<float> src) : this(ToDoubles(src)) { }

    private static (IScalar<double>, IScalar<int>, IScalar<long>, IScalar<float>) ScalarsFrom(IEnumerable<double> src)
    {
        return (
            new AsScalar<double>(() =>
            {
                var max = double.MinValue;
                using var e = src.GetEnumerator();
                var hasAny = false;
                while (e.MoveNext())
                {
                    hasAny = true;
                    if (e.Current > max) max = e.Current;
                }
                return hasAny ? max : double.MinValue;
            }),
            new AsScalar<int>(() =>
            {
                var max = int.MinValue;
                using var e = src.GetEnumerator();
                var hasAny = false;
                while (e.MoveNext())
                {
                    hasAny = true;
                    var val = (int)e.Current;
                    if (val > max) max = val;
                }
                return hasAny ? max : int.MinValue;
            }),
            new AsScalar<long>(() =>
            {
                var max = long.MinValue;
                using var e = src.GetEnumerator();
                var hasAny = false;
                while (e.MoveNext())
                {
                    hasAny = true;
                    var val = (long)e.Current;
                    if (val > max) max = val;
                }
                return hasAny ? max : long.MinValue;
            }),
            new AsScalar<float>(() =>
            {
                var max = float.MinValue;
                using var e = src.GetEnumerator();
                var hasAny = false;
                while (e.MoveNext())
                {
                    hasAny = true;
                    var val = (float)e.Current;
                    if (val > max) max = val;
                }
                return hasAny ? max : float.MinValue;
            })
        );
    }

    private static IEnumerable<double> ToDoubles(IEnumerable<int> source)
    {
        foreach (var item in source)
            yield return item;
    }

    private static IEnumerable<double> ToDoubles(IEnumerable<long> source)
    {
        foreach (var item in source)
            yield return item;
    }

    private static IEnumerable<double> ToDoubles(IEnumerable<float> source)
    {
        foreach (var item in source)
            yield return item;
    }

    private static IEnumerable<double> ToDoubles(int[] source)
    {
        foreach (var item in source)
            yield return item;
    }

    private static IEnumerable<double> ToDoubles(long[] source)
    {
        foreach (var item in source)
            yield return item;
    }

    private static IEnumerable<double> ToDoubles(float[] source)
    {
        foreach (var item in source)
            yield return item;
    }
}
