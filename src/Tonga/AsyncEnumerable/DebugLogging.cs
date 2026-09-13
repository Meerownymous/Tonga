using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Async enumerable that logs object T when it is iterated.
/// T is logged right after the underlying enumerator is moved.
/// </summary>
public sealed class DebugLogging<T>(IAsyncEnumerable<T> origin, Action<T> log) : IAsyncEnumerable<T>
{
    /// <summary>
    /// Async enumerable that logs object T to the debug console when it is iterated.
    /// </summary>
    public DebugLogging(IAsyncEnumerable<T> origin) : this(origin, item => Debug.WriteLine(item.ToString()))
    { }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        await foreach (var item in origin.WithCancellation(cancellation))
        {
            log(item);
            yield return item;
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Async enumerable that logs object T to the debug console when it is iterated.
    /// </summary>
    public static IAsyncEnumerable<T> AsDebugLogging<T>(this IAsyncEnumerable<T> origin) =>
        new DebugLogging<T>(origin);

    /// <summary>
    /// Async enumerable that logs object T when it is iterated.
    /// </summary>
    public static IAsyncEnumerable<T> AsDebugLogging<T>(this IAsyncEnumerable<T> origin, Action<T> log) =>
        new DebugLogging<T>(origin, log);
}
