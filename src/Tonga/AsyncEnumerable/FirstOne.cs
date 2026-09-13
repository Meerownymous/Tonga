using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tonga.Scalar;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// First element in an <see cref="IAsyncEnumerable{T}"/>.
/// Reads no further than the first match.
/// </summary>
public sealed class FirstOne<T>(
    Func<T, bool> condition,
    IAsyncEnumerable<T> src,
    Func<Exception, IAsyncEnumerable<T>, T> fallback
) : AsyncScalarEnvelope<T>(
    async cancellation =>
    {
        T result;
        try
        {
            var filtered = src.AsFiltered(condition).GetAsyncEnumerator(cancellation);
            try
            {
                result =
                    await filtered.MoveNextAsync()
                        ? filtered.Current
                        : fallback(new ArgumentException("Source is empty"), src);
            }
            finally
            {
                await filtered.DisposeAsync();
            }
        }
        catch (Exception e)
        {
            result = fallback(e, src);
        }
        return result;
    })
{
    /// <summary>
    /// First element in an <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public FirstOne(IAsyncEnumerable<T> source) : this(
        _ => true,
        source,
        new ArgumentException("Cannot get first element - no match.")
    )
    { }

    /// <summary>
    /// First element in an <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public FirstOne(IAsyncEnumerable<T> source, Exception ex) : this(_ => true, source, (_, _) => throw ex)
    { }

    /// <summary>
    /// First matching element in an <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public FirstOne(Func<T, bool> condition, IAsyncEnumerable<T> source) : this(
        condition,
        source,
        new ArgumentException("Cannot get first element - no match.")
    )
    { }

    /// <summary>
    /// First matching element in an <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public FirstOne(Func<T, bool> condition, IAsyncEnumerable<T> source, Exception ex) : this(
        condition, source, (_, _) => throw ex
    )
    { }

    /// <summary>
    /// First element in an <see cref="IAsyncEnumerable{T}"/> with a fallback value.
    /// </summary>
    public FirstOne(IAsyncEnumerable<T> source, T fallback) : this(_ => true, source, (_, _) => fallback)
    { }

    /// <summary>
    /// First matching element in an <see cref="IAsyncEnumerable{T}"/> with a fallback value.
    /// </summary>
    public FirstOne(Func<T, bool> condition, IAsyncEnumerable<T> source, T fallback) : this(
        condition, source, (_, _) => fallback
    )
    { }

    /// <summary>
    /// First element in an <see cref="IAsyncEnumerable{T}"/> with a fallback function.
    /// </summary>
    public FirstOne(IAsyncEnumerable<T> src, Func<Exception, IAsyncEnumerable<T>, T> fallback) : this(
        _ => true, src, fallback
    )
    { }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// First element in an <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public static IAsyncScalar<T> FirstOne<T>(this IAsyncEnumerable<T> source) => new FirstOne<T>(source);

    /// <summary>
    /// First element in an <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public static IAsyncScalar<T> FirstOne<T>(this IAsyncEnumerable<T> source, Exception ex) =>
        new FirstOne<T>(source, ex);

    /// <summary>
    /// First matching element in an <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public static IAsyncScalar<T> FirstOne<T>(this IAsyncEnumerable<T> source, Func<T, bool> condition) =>
        new FirstOne<T>(condition, source);

    /// <summary>
    /// First matching element in an <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public static IAsyncScalar<T> FirstOne<T>(
        this IAsyncEnumerable<T> source, Func<T, bool> condition, Exception ex
    ) =>
        new FirstOne<T>(condition, source, ex);

    /// <summary>
    /// First element in an <see cref="IAsyncEnumerable{T}"/> with a fallback value.
    /// </summary>
    public static IAsyncScalar<T> FirstOne<T>(this IAsyncEnumerable<T> source, T fallback) =>
        new FirstOne<T>(source, fallback);

    /// <summary>
    /// First matching element in an <see cref="IAsyncEnumerable{T}"/> with a fallback value.
    /// </summary>
    public static IAsyncScalar<T> FirstOne<T>(this IAsyncEnumerable<T> source, Func<T, bool> condition, T fallback) =>
        new FirstOne<T>(condition, source, fallback);

    /// <summary>
    /// First element in an <see cref="IAsyncEnumerable{T}"/> with a fallback function.
    /// </summary>
    public static IAsyncScalar<T> FirstOne<T>(
        this IAsyncEnumerable<T> src, Func<Exception, IAsyncEnumerable<T>, T> fallback
    ) =>
        new FirstOne<T>(src, fallback);
}
