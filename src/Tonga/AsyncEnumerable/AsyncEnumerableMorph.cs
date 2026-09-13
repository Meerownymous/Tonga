using System.Collections.Generic;
using System.Threading;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Implicitly morph from various sources to an async enumerable.
/// </summary>
public sealed class AsyncEnumerableMorph<T>(IAsyncEnumerable<T> seed) : IAsyncEnumerable<T>
{
    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default) =>
        seed.GetAsyncEnumerator(cancellation);

    public static implicit operator AsyncEnumerableMorph<T>(T[] items) => new(new AsAsyncEnumerable<T>(items));
}
