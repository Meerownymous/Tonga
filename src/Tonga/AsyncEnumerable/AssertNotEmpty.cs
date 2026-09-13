using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Ensures that an <see cref="IAsyncEnumerable{T}"/> is not empty.
/// </summary>
public sealed class AssertNotEmpty<T>(IAsyncEnumerable<T> origin, Exception ex) : IAsyncEnumerable<T>
{
    /// <summary>
    /// Ensures that an <see cref="IAsyncEnumerable{T}"/> is not empty.
    /// </summary>
    public AssertNotEmpty(IAsyncEnumerable<T> origin) : this(
        origin, new Exception("Enumerable is empty")
    )
    { }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var empty = true;
        await foreach (var item in origin.WithCancellation(cancellation))
        {
            empty = false;
            yield return item;
        }
        if (empty)
        {
            throw ex;
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Ensures that an <see cref="IAsyncEnumerable{T}"/> is not empty.
    /// </summary>
    public static IAsyncEnumerable<T> AssertNotEmpty<T>(this IAsyncEnumerable<T> origin) =>
        new AssertNotEmpty<T>(origin);

    /// <summary>
    /// Ensures that an <see cref="IAsyncEnumerable{T}"/> is not empty.
    /// </summary>
    public static IAsyncEnumerable<T> AssertNotEmpty<T>(this IAsyncEnumerable<T> origin, Exception ex) =>
        new AssertNotEmpty<T>(origin, ex);
}
