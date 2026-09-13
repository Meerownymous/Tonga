using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// A <see cref="ArrayList"/> converted to IAsyncEnumerable&lt;object&gt;
/// </summary>
public sealed class AsyncEnumerableOfArrayList(ArrayList src) : IAsyncEnumerable<object>
{
    public async IAsyncEnumerator<object> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        foreach (var item in src)
        {
            cancellation.ThrowIfCancellationRequested();
            yield return item;
        }
    }
}

/// <summary>
/// A <see cref="ArrayList"/> converted to IAsyncEnumerable&lt;T&gt;
/// </summary>
public sealed class ArrayListAsAsyncEnumerable<T>(ArrayList src) : IAsyncEnumerable<T>
{
    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        foreach (var item in src)
        {
            cancellation.ThrowIfCancellationRequested();
            yield return (T)item;
        }
    }
}
