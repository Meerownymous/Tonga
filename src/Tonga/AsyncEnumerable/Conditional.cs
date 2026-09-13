using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Async enumerable sourced depending on a given condition.
/// </summary>
public sealed class Conditional<T>(
    IAsyncEnumerable<T> whenMatching,
    IAsyncEnumerable<T> whenNotMatching,
    Func<CancellationToken, ValueTask<bool>> condition
) : IAsyncEnumerable<T>
{
    /// <summary>
    /// Async enumerable sourced depending on a given condition.
    /// </summary>
    public Conditional(IAsyncEnumerable<T> whenMatching, IAsyncEnumerable<T> whenNotMatching, bool condition) : this(
        whenMatching, whenNotMatching, _ => new ValueTask<bool>(condition)
    )
    { }

    /// <summary>
    /// Async enumerable sourced depending on a given condition.
    /// </summary>
    public Conditional(
        IAsyncEnumerable<T> whenMatching, IAsyncEnumerable<T> whenNotMatching, Func<bool> condition
    ) : this(
        whenMatching, whenNotMatching, _ => new ValueTask<bool>(condition())
    )
    { }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var source = await condition(cancellation) ? whenMatching : whenNotMatching;
        await foreach (var item in source.WithCancellation(cancellation))
        {
            yield return item;
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Async enumerable sourced depending on a given condition.
    /// </summary>
    public static IAsyncEnumerable<T> AsConditional<T>(
        this IAsyncEnumerable<T> whenMatching, IAsyncEnumerable<T> whenNotMatching, bool condition
    ) =>
        new Conditional<T>(whenMatching, whenNotMatching, condition);

    /// <summary>
    /// Async enumerable sourced depending on a given condition.
    /// </summary>
    public static IAsyncEnumerable<T> AsConditional<T>(
        this IAsyncEnumerable<T> whenMatching, IAsyncEnumerable<T> whenNotMatching, Func<bool> condition
    ) =>
        new Conditional<T>(whenMatching, whenNotMatching, condition);

    /// <summary>
    /// Async enumerable sourced depending on an awaited condition.
    /// </summary>
    public static IAsyncEnumerable<T> AsConditional<T>(
        this IAsyncEnumerable<T> whenMatching,
        IAsyncEnumerable<T> whenNotMatching,
        Func<CancellationToken, ValueTask<bool>> condition
    ) =>
        new Conditional<T>(whenMatching, whenNotMatching, condition);
}
