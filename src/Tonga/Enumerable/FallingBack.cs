using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable;

/// <summary>
/// Delivers contents from a fallback source in case the original source is empty.
/// </summary>
public sealed class FallingBack<T>(IEnumerable<T> origin, IEnumerable<T> fallback) : IEnumerable<T>
{
    /// <summary>
    /// Delivers contents from a fallback source in case the original source is empty.
    /// </summary>
    public FallingBack(IEnumerable<T> origin, params T[] fallback) : this(
        origin, new AsEnumerable<T>(fallback)
    )
    { }

    public IEnumerator<T> GetEnumerator()
    {
        using var e = origin.GetEnumerator();
        if (e.MoveNext())
        {
            do yield return e.Current;
            while (e.MoveNext());
        }
        else
        {
            foreach (var item in fallback)
                yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

/// <summary>
/// Smarts for <see cref="FallingBack{T}"/>
/// </summary>
public static class FallbackSmarts
{
    public static IEnumerable<T> AsBackFalling<T>(this IEnumerable<T> origin, IEnumerable<T> fallback) =>
        new FallingBack<T>(origin, fallback);
}
