using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tonga.Scalar;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// An <see cref="IAsyncEnumerable{T}"/> whose items are reduced to one item using the given function.
/// </summary>
public sealed class Reduced<T> : AsyncScalarEnvelope<T>
{
    /// <summary>
    /// An <see cref="IAsyncEnumerable{T}"/> whose items are reduced to one item.
    /// </summary>
    public Reduced(IAsyncEnumerable<T> elements, Func<T, T, T> reduction) : this(
        elements, input => reduction(input.currentState, input.nextItem)
    )
    { }

    /// <summary>
    /// An <see cref="IAsyncEnumerable{T}"/> whose items are reduced to one item.
    /// </summary>
    public Reduced(IAsyncEnumerable<T> elements, Func<(T currentState, T nextItem), T> reduction) : base(
        async cancellation =>
        {
            var enm = elements.GetAsyncEnumerator(cancellation);
            try
            {
                if (!await enm.MoveNextAsync())
                    throw new ArgumentException(
                        "Cannot reduce, at least one element is needed but the enumerable is empty."
                    );
                var result = enm.Current;
                while (await enm.MoveNextAsync())
                {
                    result = reduction((result, enm.Current));
                }
                return result;
            }
            finally
            {
                await enm.DisposeAsync();
            }
        }
    )
    { }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// An <see cref="IAsyncEnumerable{T}"/> whose items are reduced to one item.
    /// </summary>
    public static IAsyncScalar<T> AsReduced<T>(this IAsyncEnumerable<T> elements, Func<T, T, T> fnc) =>
        new Reduced<T>(elements, fnc);

    /// <summary>
    /// An <see cref="IAsyncEnumerable{T}"/> whose items are reduced to one item.
    /// </summary>
    public static IAsyncScalar<T> AsReduced<T>(
        this IAsyncEnumerable<T> elements, Func<(T currentState, T nextItem), T> fnc
    ) =>
        new Reduced<T>(elements, fnc);
}
