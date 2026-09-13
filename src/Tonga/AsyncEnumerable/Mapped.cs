using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Mapped content of an <see cref="IAsyncEnumerable{T}"/> to another type using the given function.
/// </summary>
/// <typeparam name="In">type of input elements</typeparam>
/// <typeparam name="Out">type of mapped elements</typeparam>
public sealed class Mapped<In, Out>(Func<In, int, CancellationToken, ValueTask<Out>> fnc, IAsyncEnumerable<In> src) :
    IAsyncEnumerable<Out>
{
    /// <summary>
    /// Mapped content of an <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public Mapped(Func<In, int, Out> fnc, IAsyncEnumerable<In> src) : this(
        (item, index, _) => new ValueTask<Out>(fnc(item, index)),
        src
    )
    { }

    /// <summary>
    /// Mapped content of an <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public Mapped(Func<In, Out> fnc, IAsyncEnumerable<In> src) : this(
        (item, _) => fnc(item),
        src
    )
    { }

    /// <summary>
    /// Mapped content of an <see cref="IAsyncEnumerable{T}"/> using an awaited function.
    /// </summary>
    public Mapped(Func<In, ValueTask<Out>> fnc, IAsyncEnumerable<In> src) : this(
        (item, _, _) => fnc(item),
        src
    )
    { }

    /// <summary>
    /// Mapped content of an <see cref="IAsyncEnumerable{T}"/> using an awaited function.
    /// </summary>
    public Mapped(Func<In, int, ValueTask<Out>> fnc, IAsyncEnumerable<In> src) : this(
        (item, index, _) => fnc(item, index),
        src
    )
    { }

    public async IAsyncEnumerator<Out> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var index = 0;
        await foreach (var item in src.WithCancellation(cancellation))
        {
            yield return await fnc(item, index++, cancellation);
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Mapped content of an <see cref="IAsyncEnumerable{T}"/> to another type.
    /// </summary>
    public static IAsyncEnumerable<Out> AsMapped<In, Out>(this IAsyncEnumerable<In> src, Func<In, Out> fnc) =>
        new Mapped<In, Out>(fnc, src);

    /// <summary>
    /// Mapped content of an <see cref="IAsyncEnumerable{T}"/> to another type, with index.
    /// </summary>
    public static IAsyncEnumerable<Out> AsMapped<In, Out>(this IAsyncEnumerable<In> src, Func<In, int, Out> fnc) =>
        new Mapped<In, Out>(fnc, src);

    /// <summary>
    /// Mapped content of an <see cref="IAsyncEnumerable{T}"/> using an awaited function.
    /// </summary>
    public static IAsyncEnumerable<Out> AsMapped<In, Out>(this IAsyncEnumerable<In> src, Func<In, ValueTask<Out>> fnc) =>
        new Mapped<In, Out>(fnc, src);

    /// <summary>
    /// Mapped content of an <see cref="IAsyncEnumerable{T}"/> using an awaited function, with index.
    /// </summary>
    public static IAsyncEnumerable<Out> AsMapped<In, Out>(
        this IAsyncEnumerable<In> src, Func<In, int, ValueTask<Out>> fnc
    ) =>
        new Mapped<In, Out>(fnc, src);
}
