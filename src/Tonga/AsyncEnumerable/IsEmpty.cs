using System.Collections.Generic;
using System.Threading.Tasks;
using Tonga.Fact;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Tells if an async enumerable source has no elements.
/// Reads at most one item.
/// </summary>
public sealed class IsEmpty<T>(IAsyncEnumerable<T> origin) : AsyncFactEnvelope(
    async cancellation =>
    {
        var e = origin.GetAsyncEnumerator(cancellation);
        try
        {
            return !await e.MoveNextAsync();
        }
        finally
        {
            await e.DisposeAsync();
        }
    }
);

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Tells if an async enumerable source has no elements.
    /// </summary>
    public static IAsyncFact IsEmpty<T>(this IAsyncEnumerable<T> origin) => new IsEmpty<T>(origin);
}
