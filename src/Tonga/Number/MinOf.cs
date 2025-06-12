using System.Collections.Generic;

namespace Tonga.Number;

/// <summary>
/// The minimum of the given numbers
/// </summary>
public sealed class MinOf : NumberEnvelope
{
    /// <summary>
    /// The minimum of the source integers
    /// </summary>
    /// <param name="src">integers to find max in</param>
    public MinOf(params int[] src) : this(ToDoubles(src))
    { }

    /// <summary>
    /// The minimum of the source integers
    /// </summary>
    /// <param name="src">integers to find max in</param>
    public MinOf(IEnumerable<int> src) : this(ToDoubles(src))
    { }

    /// <summary>
    /// The minimum of the source integers
    /// </summary>
    /// <param name="src">integers to find max in</param>
    public MinOf(params double[] src) : this((IEnumerable<double>)src)
    { }

    /// <summary>
    /// The minimum of the source integers
    /// </summary>
    /// <param name="src">integers to find max in</param>
    public MinOf(IEnumerable<double> src) : base(
        () =>
        {
            var min = double.MaxValue;
            using var e = src.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current < min) min = e.Current;
            }
            return min;
        },
        () =>
        {
            var min = int.MaxValue;
            using var e = src.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current < min) min = (int)e.Current;
            }
            return min;
        },
        () =>
        {
            var min = long.MaxValue;
            using var e = src.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current < min) min = (long)e.Current;
            }
            return min;
        },
        () =>
        {
            var min = float.MaxValue;
            using var e = src.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current < min) min = (float)e.Current;
            }
            return min;
        }
    )
    { }

    /// <summary>
    /// The minimum of the source integers
    /// </summary>
    /// <param name="src">integers to find max in</param>
    public MinOf(params long[] src) : this(ToDoubles(src))
    { }

    /// <summary>
    /// The minimum of the source integers
    /// </summary>
    /// <param name="src">integers to find max in</param>
    public MinOf(IEnumerable<long> src) : this(ToDoubles(src))
    { }

    /// <summary>
    /// The minimum of the source integers
    /// </summary>
    /// <param name="src">integers to find max in</param>
    public MinOf(params float[] src) : this(ToDoubles(src))
    { }

    /// <summary>
    /// The minimum of the source integers
    /// </summary>
    /// <param name="src">integers to find max in</param>
    public MinOf(IEnumerable<float> src) : this(ToDoubles(src))
    { }

    private static IEnumerable<double> ToDoubles(int[] src)
    {
        foreach (var item in src)
            yield return item;
    }

    private static IEnumerable<double> ToDoubles(IEnumerable<int> src)
    {
        foreach (var item in src)
            yield return item;
    }

    private static IEnumerable<double> ToDoubles(long[] src)
    {
        foreach (var item in src)
            yield return item;
    }

    private static IEnumerable<double> ToDoubles(IEnumerable<long> src)
    {
        foreach (var item in src)
            yield return item;
    }

    private static IEnumerable<double> ToDoubles(float[] src)
    {
        foreach (var item in src)
            yield return item;
    }

    private static IEnumerable<double> ToDoubles(IEnumerable<float> src)
    {
        foreach (var item in src)
            yield return item;
    }
}
