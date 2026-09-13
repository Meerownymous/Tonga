using System.Threading.Tasks;
using System.Collections.Generic;
using Tonga.Scalar;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// An <see cref="IAsyncEnumerable{T}"/> materialised into a list.
/// This is the boundary: everything before it costs nothing, awaiting this reads the source.
/// </summary>
public sealed class AsList<T>(IAsyncEnumerable<T> source) : AsyncScalarEnvelope<IList<T>>(
    async cancellation =>
    {
        var result = new List<T>();
        await foreach (var item in source.WithCancellation(cancellation))
        {
            result.Add(item);
        }
        return result;
    }
);

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// An <see cref="IAsyncEnumerable{T}"/> materialised into a list.
    /// </summary>
    public static IAsyncScalar<IList<T>> AsList<T>(this IAsyncEnumerable<T> source) => new AsList<T>(source);
}
