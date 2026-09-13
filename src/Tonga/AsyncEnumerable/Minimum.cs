using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tonga.Scalar;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// The smallest item in the given <see cref="IAsyncEnumerable{T}"/>.
/// </summary>
public sealed class Minimum<T>(IAsyncEnumerable<T> items) : AsyncScalarEnvelope<T>(
    async cancellation =>
    {
        var e = items.GetAsyncEnumerator(cancellation);
        try
        {
            if (!await e.MoveNextAsync())
                throw new ArgumentException("Can't find smaller element in an empty iterable");

            var min = e.Current;
            while (await e.MoveNextAsync())
            {
                var next = e.Current;
                if (next.CompareTo(min) < 0)
                    min = next;
            }
            return min;
        }
        finally
        {
            await e.DisposeAsync();
        }
    }
) where T : IComparable<T>
{
    /// <summary>
    /// The smallest of the given items.
    /// </summary>
    public Minimum(params T[] items) : this(items.AsAsyncEnumerable())
    { }

    /// <summary>
    /// The smallest item delivered by the given scalars.
    /// </summary>
    public Minimum(IAsyncEnumerable<IAsyncScalar<T>> items) : this(
        new Mapped<IAsyncScalar<T>, T>((item, _, cancellation) => item.Value(cancellation), items)
    )
    { }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// The smallest item in the given <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public static IAsyncScalar<T> Minimum<T>(this IAsyncEnumerable<T> items) where T : IComparable<T> =>
        new Minimum<T>(items);

    /// <summary>
    /// The smallest of the given items.
    /// </summary>
    public static IAsyncScalar<T> Minimum<T>(this T[] items) where T : IComparable<T> =>
        new Minimum<T>(items);

    /// <summary>
    /// The smallest item delivered by the given scalars.
    /// </summary>
    public static IAsyncScalar<T> Minimum<T>(this IAsyncEnumerable<IAsyncScalar<T>> items)
        where T : IComparable<T> =>
        new Minimum<T>(items);
}
