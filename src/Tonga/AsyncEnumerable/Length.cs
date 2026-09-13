using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tonga.Scalar;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Length of an <see cref="IAsyncEnumerable{T}"/>.
/// </summary>
public sealed class Length<T>(IAsyncEnumerable<T> source) : AsyncScalarEnvelope<long>(
    async cancellation =>
    {
        var length = 0L;
        await foreach (var unused in source.WithCancellation(cancellation))
        {
            length++;
        }
        return length;
    }
);

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Length of an <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    public static IAsyncScalar<long> Length<T>(this IAsyncEnumerable<T> items) => new Length<T>(items);
}
