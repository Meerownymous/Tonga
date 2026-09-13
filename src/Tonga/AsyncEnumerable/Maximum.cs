using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tonga.Scalar;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// The greatest item in the given <see cref="IAsyncEnumerable{T}"/>.
/// </summary>
public sealed class Maximum<T>(IAsyncEnumerable<T> items) : AsyncScalarEnvelope<T>(
    async cancellation =>
    {
        var e = items.GetAsyncEnumerator(cancellation);
        try
        {
            if (!await e.MoveNextAsync())
                throw new ArgumentException("Can't find greater element in an empty iterable");

            var max = e.Current;
            while (await e.MoveNextAsync())
            {
                var next = e.Current;
                if (next.CompareTo(max) > 0)
                    max = next;
            }
            return max;
        }
        finally
        {
            await e.DisposeAsync();
        }
    }
) where T : IComparable<T>
{
    /// <summary>
    /// The greatest of the given items.
    /// </summary>
    public Maximum(params T[] items) : this(items.AsAsyncEnumerable())
    { }

    /// <summary>
    /// The greatest item delivered by the given scalars.
    /// </summary>
    public Maximum(IAsyncEnumerable<IAsyncScalar<T>> items) : this(
        new Mapped<IAsyncScalar<T>, T>((item, _, cancellation) => item.Value(cancellation), items)
    )
    { }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// The greatest item in the given <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public static IAsyncScalar<T> Maximum<T>(this IAsyncEnumerable<T> items) where T : IComparable<T> =>
        new Maximum<T>(items);

    /// <summary>
    /// The greatest of the given items.
    /// </summary>
    public static IAsyncScalar<T> Maximum<T>(this T[] items) where T : IComparable<T> =>
        new Maximum<T>(items);

    /// <summary>
    /// The greatest item delivered by the given scalars.
    /// </summary>
    public static IAsyncScalar<T> Maximum<T>(this IAsyncEnumerable<IAsyncScalar<T>> items)
        where T : IComparable<T> =>
        new Maximum<T>(items);
}
