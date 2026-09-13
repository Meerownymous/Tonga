using System.Collections.Generic;
using System.Threading;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// A <see cref="IAsyncEnumerable{T}"/> that repeats one element infinitely.
/// </summary>
public sealed class Endless<T>(T elm) : IAsyncEnumerable<T>
{
    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        while (true)
        {
            cancellation.ThrowIfCancellationRequested();
            yield return elm;
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// An async enumerable that repeats one element infinitely.
    /// </summary>
    public static IAsyncEnumerable<T> AsAsyncEndless<T>(this T elm) => new Endless<T>(elm);
}
