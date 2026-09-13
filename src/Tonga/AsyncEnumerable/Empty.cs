using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Async enumerable which is empty.
/// </summary>
public sealed class Empty<T> : IAsyncEnumerable<T>
{
    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        yield break;
    }
}
