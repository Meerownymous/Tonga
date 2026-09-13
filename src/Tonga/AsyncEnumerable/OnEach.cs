using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Async enumerable which executes a given lambda function when advancing.
/// </summary>
public sealed class OnEach<T>(Func<T, int, CancellationToken, ValueTask> action, IAsyncEnumerable<T> origin) :
    IAsyncEnumerable<T>
{
    /// <summary>
    /// Async enumerable which executes a given lambda function when advancing.
    /// </summary>
    public OnEach(Action<T, int> lambda, IAsyncEnumerable<T> origin) : this(
        (item, index, _) =>
        {
            lambda(item, index);
            return default;
        },
        origin
    )
    { }

    /// <summary>
    /// Async enumerable which executes a given lambda function when advancing.
    /// </summary>
    public OnEach(Action<T> lambda, IAsyncEnumerable<T> origin) : this((item, _) => lambda(item), origin)
    { }

    /// <summary>
    /// Async enumerable which executes a given lambda function when advancing.
    /// </summary>
    public OnEach(Action lambda, IAsyncEnumerable<T> origin) : this((_, _) => lambda(), origin)
    { }

    /// <summary>
    /// Async enumerable which awaits a given function when advancing.
    /// </summary>
    public OnEach(Func<T, ValueTask> lambda, IAsyncEnumerable<T> origin) : this(
        (item, _, _) => lambda(item), origin
    )
    { }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var index = 0;
        await foreach (var item in origin.WithCancellation(cancellation))
        {
            await action(item, index++, cancellation);
            yield return item;
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Async enumerable which executes a given lambda function when advancing.
    /// </summary>
    public static IAsyncEnumerable<T> OnEach<T>(this IAsyncEnumerable<T> origin, Action lambda) =>
        new OnEach<T>(lambda, origin);

    /// <summary>
    /// Async enumerable which executes a given lambda function when advancing.
    /// </summary>
    public static IAsyncEnumerable<T> OnEach<T>(this IAsyncEnumerable<T> origin, Action<T> lambda) =>
        new OnEach<T>(lambda, origin);

    /// <summary>
    /// Async enumerable which executes a given lambda function when advancing.
    /// </summary>
    public static IAsyncEnumerable<T> OnEach<T>(this IAsyncEnumerable<T> origin, Action<T, int> lambda) =>
        new OnEach<T>(lambda, origin);

    /// <summary>
    /// Async enumerable which awaits a given function when advancing.
    /// </summary>
    public static IAsyncEnumerable<T> OnEach<T>(this IAsyncEnumerable<T> origin, Func<T, ValueTask> lambda) =>
        new OnEach<T>(lambda, origin);
}
