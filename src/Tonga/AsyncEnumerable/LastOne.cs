using System;
using System.Collections.Generic;
using Tonga.Scalar;
using Tonga.Text;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Last element in an <see cref="IAsyncEnumerable{T}"/>.
/// </summary>
public sealed class LastOne<T> : AsyncScalarEnvelope<T>
{
    /// <summary>
    /// Last element in an <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public LastOne(IAsyncEnumerable<T> source) : this(
        source,
        (ex, _) => throw new ArgumentException(
            new Formatted("Cannot get last element: {0}", ex.Message).Str()
        )
    )
    { }

    /// <summary>
    /// Last element in an <see cref="IAsyncEnumerable{T}"/> with a given exception thrown on fallback.
    /// </summary>
    public LastOne(IAsyncEnumerable<T> source, Exception ex) : this(source, (_, _) => throw ex)
    { }

    /// <summary>
    /// Last element in an <see cref="IAsyncEnumerable{T}"/> with a fallback value.
    /// </summary>
    public LastOne(IAsyncEnumerable<T> source, T fallback) : this(source, (_, _) => fallback)
    { }

    /// <summary>
    /// Last element in an <see cref="IAsyncEnumerable{T}"/> with a fallback function.
    /// </summary>
    public LastOne(IAsyncEnumerable<T> source, Func<Exception, IAsyncEnumerable<T>, T> fallback) : base(
        new FirstOne<T>(new Reversed<T>(source), fallback)
    )
    { }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Last element in an <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public static IAsyncScalar<T> LastOne<T>(this IAsyncEnumerable<T> source) => new LastOne<T>(source);

    /// <summary>
    /// Last element in an <see cref="IAsyncEnumerable{T}"/> with a given exception thrown on fallback.
    /// </summary>
    public static IAsyncScalar<T> LastOne<T>(this IAsyncEnumerable<T> source, Exception ex) =>
        new LastOne<T>(source, ex);

    /// <summary>
    /// Last element in an <see cref="IAsyncEnumerable{T}"/> with a fallback value.
    /// </summary>
    public static IAsyncScalar<T> LastOne<T>(this IAsyncEnumerable<T> source, T fallback) =>
        new LastOne<T>(source, fallback);

    /// <summary>
    /// Last element in an <see cref="IAsyncEnumerable{T}"/> with a fallback function.
    /// </summary>
    public static IAsyncScalar<T> LastOne<T>(
        this IAsyncEnumerable<T> source, Func<Exception, IAsyncEnumerable<T>, T> fallback
    ) =>
        new LastOne<T>(source, fallback);
}
