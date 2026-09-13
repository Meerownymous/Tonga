using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Items which do only exist in only one of the async enumerables.
/// </summary>
public class Divergency<T>(IEnumerable<IAsyncEnumerable<T>> sources, Func<T, bool> match) : IAsyncEnumerable<T>
{
    /// <summary>
    /// Items which do only exist in only one of the async enumerables.
    /// </summary>
    public Divergency(params IAsyncEnumerable<T>[] sources) : this(sources, _ => true)
    { }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var all = new List<T>();
        var count = new Dictionary<T, int>(EqualityComparer<T>.Default);

        foreach (var source in sources)
        {
            var set = new HashSet<T>(EqualityComparer<T>.Default);
            await foreach (var item in source.AsFiltered(match).WithCancellation(cancellation))
            {
                set.Add(item);
            }
            foreach (var item in set)
            {
                if (count.TryGetValue(item, out var seen))
                    count[item] = seen + 1;
                else
                {
                    count[item] = 1;
                    all.Add(item);
                }
            }
        }

        foreach (var item in all)
        {
            if (count[item] == 1)
                yield return item;
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Items which do only exist in only one of the async enumerables.
    /// </summary>
    public static IAsyncEnumerable<TItem> AsDivergency<TItem>(this IAsyncEnumerable<TItem>[] sources) =>
        new Divergency<TItem>(sources);

    /// <summary>
    /// Items which do only exist in only one of the async enumerables.
    /// </summary>
    public static IAsyncEnumerable<TItem> AsDivergency<TItem>(
        this IAsyncEnumerable<TItem>[] sources, Func<TItem, bool> match
    ) =>
        new Divergency<TItem>(sources, match);

    /// <summary>
    /// Items which do only exist in only one of the async enumerables.
    /// </summary>
    public static IAsyncEnumerable<TItem> AsDivergency<TItem>(this IEnumerable<IAsyncEnumerable<TItem>> sources) =>
        new Divergency<TItem>(sources, _ => true);

    /// <summary>
    /// Items which do only exist in only one of the async enumerables.
    /// </summary>
    public static IAsyncEnumerable<TItem> AsDivergency<TItem>(
        this IEnumerable<IAsyncEnumerable<TItem>> sources, Func<TItem, bool> match
    ) =>
        new Divergency<TItem>(sources, match);
}
