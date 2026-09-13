using System;
using System.Collections.Generic;
using System.Threading;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Envelope for async enumerables.
/// It bundles the methods offered by IAsyncEnumerable and enables function based ctors.
/// </summary>
public abstract class AsyncEnumerableEnvelope<T>(Func<IAsyncEnumerable<T>> origin) : IAsyncEnumerable<T>
{
    /// <summary>
    /// Envelope for async enumerables.
    /// </summary>
    public AsyncEnumerableEnvelope(IAsyncEnumerable<T> origin) : this(() => origin)
    { }

    /// <summary>
    /// Envelope for async enumerables.
    /// </summary>
    public AsyncEnumerableEnvelope(AsyncEnumerableMorph<T> origin) : this(() => origin)
    { }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default) =>
        origin().GetAsyncEnumerator(cancellation);
}
