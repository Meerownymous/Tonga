

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tonga.Enumerable;

/// <summary>
/// Enumerable that logs object T when it is iterated.
/// T is logged right after the underlying enumerator is moved.
/// </summary>
public sealed class DebugLogging<T>(IEnumerable<T> origin, Action<T> log) : IEnumerable<T>
{
    /// <summary>
    /// Enumerable that logs object T to debug console when it is iterated.
    /// T is logged right after the underlying enumerator is moved.
    /// </summary>
    public DebugLogging(IEnumerable<T> origin) : this(origin, (item) => Debug.WriteLine(item.ToString()))
    { }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in origin)
        {
            log(item);
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}

public static partial class EnumerableSmarts
{
    /// <summary>
    /// Enumerable that logs object T to debug console when it is iterated.
    /// T is logged right after the underlying enumerator is moved.
    /// </summary>
    public static IEnumerable<T> AsDebugLogging<T>(this IEnumerable<T> origin) => new DebugLogging<T>(origin);

    /// <summary>
    /// Enumerable that logs object T when it is iterated.
    /// T is logged right after the underlying enumerator is moved.
    /// </summary>
    public static IEnumerable<T> AsDebugLogging<T>(this IEnumerable<T> origin, Action<T> log) => new DebugLogging<T>(origin, log);
}
