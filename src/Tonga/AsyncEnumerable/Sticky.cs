using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Async enumerable which memoizes already visited items.
/// The source is read at most once, no matter how often the result is enumerated.
/// <para>
/// Because the source is read once, the first enumeration decides which
/// <see cref="CancellationToken"/> it sees. Later enumerations are served from the memo and
/// only observe their own token while they wait for their turn.
/// </para>
/// <para>
/// One enumeration advances the source at a time. A source which enumerates this very instance
/// while being read therefore waits for itself and never finishes.
/// </para>
/// </summary>
public class Sticky<T>(Func<CancellationToken, IAsyncEnumerator<T>> source) : IAsyncEnumerable<T>
{
    private readonly SemaphoreSlim exclusive = new(1, 1);
    private readonly List<T> copy = [];
    private IAsyncEnumerator<T> enumerator;
    private bool ended;

    /// <summary>
    /// Async enumerable which memoizes already visited items.
    /// </summary>
    public Sticky(IAsyncEnumerable<T> source) : this(source.GetAsyncEnumerator)
    { }

    /// <summary>
    /// Async enumerable which memoizes already visited items.
    /// </summary>
    public Sticky(Func<IAsyncEnumerable<T>> source) : this(
        cancellation => source().GetAsyncEnumerator(cancellation)
    )
    { }

    /// <summary>
    /// Async enumerable which memoizes already visited items.
    /// </summary>
    public Sticky(IAsyncEnumerator<T> source) : this(_ => source)
    { }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var index = 0;
        while (true)
        {
            bool hasValue;
            var item = default(T);

            await this.exclusive.WaitAsync(cancellation);
            try
            {
                if (index < this.copy.Count)
                {
                    hasValue = true;
                    item = this.copy[index];
                }
                else if (this.ended)
                {
                    hasValue = false;
                }
                else
                {
                    this.enumerator ??= source(cancellation);
                    hasValue = await this.enumerator.MoveNextAsync();
                    if (hasValue)
                    {
                        item = this.enumerator.Current;
                        this.copy.Add(item);
                    }
                    else
                    {
                        this.ended = true;
                        await this.enumerator.DisposeAsync();
                        this.enumerator = null;
                    }
                }
            }
            finally
            {
                this.exclusive.Release();
            }

            if (!hasValue)
                yield break;

            yield return item;
            index++;
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Async enumerable which memoizes already visited items.
    /// </summary>
    public static IAsyncEnumerable<T> AsSticky<T>(this IAsyncEnumerable<T> source) => new Sticky<T>(source);

    /// <summary>
    /// Async enumerable which memoizes already visited items.
    /// </summary>
    public static IAsyncEnumerable<T> AsSticky<T>(this Func<IAsyncEnumerable<T>> source) => new Sticky<T>(source);

    /// <summary>
    /// Async enumerable which memoizes already visited items.
    /// </summary>
    public static IAsyncEnumerable<T> AsSticky<T>(this IAsyncEnumerator<T> source) => new Sticky<T>(source);
}
