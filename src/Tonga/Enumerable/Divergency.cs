using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tonga.Enumerable;

/// <summary>
/// Items which do only exist in only one of the enumerables.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Divergency<T>(IEnumerable<IEnumerable<T>> sources, Func<T, bool> match) : IEnumerable<T>
{
    private readonly IEnumerable<T> result =
        new AsEnumerable<T>(() => Produced(sources.Select(s => s.AsFiltered(match))));

    /// <summary>
    /// Items which do only exist in only one of the enumerables.
    /// </summary>
    public Divergency(params IEnumerable<T>[] sources) : this(sources, _ => true) { }

    public IEnumerator<T> GetEnumerator() => result.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    private static IEnumerator<T> Produced(IEnumerable<IEnumerable<T>> filtered)
    {
        var sets = filtered.Select(e => new HashSet<T>(e, EqualityComparer<T>.Default)).ToArray();
        var all = new HashSet<T>(EqualityComparer<T>.Default);
        var count = new Dictionary<T, int>();

        foreach (var set in sets)
        {
            foreach (var item in set)
            {
                all.Add(item);
                if (count.ContainsKey(item))
                    count[item]++;
                else
                    count[item] = 1;
            }
        }

        foreach (var item in all)
        {
            if (count[item] == 1)
                yield return item;
        }
    }
}

public static partial class EnumerableSmarts
{
    /// <summary>
    /// Items which do only exist in only one of the enumerables.
    /// </summary>
    public static IEnumerable<TItem> AsDivergency<TItem>(
        this IEnumerable<TItem>[] sources
    ) =>
        new Divergency<TItem>(sources);

    /// <summary>
    /// Items which do only exist in only one of the enumerables.
    /// </summary>
    public static IEnumerable<TItem> AsDivergency<TItem>(
        this IEnumerable<TItem>[] sources,
        Func<TItem, bool> match
    ) =>
        new Divergency<TItem>(sources, match);

    /// <summary>
    /// Items which do only exist in only one of the enumerables.
    /// </summary>
    public static IEnumerable<TItem> AsDivergency<TItem>(
        this IEnumerable<IEnumerable<TItem>> sources
    ) =>
        new Divergency<TItem>(sources.ToArray());

    /// <summary>
    /// Items which do only exist in only one of the enumerables.
    /// </summary>
    public static IEnumerable<TItem> AsDivergency<TItem>(
        this IEnumerable<IEnumerable<TItem>> sources,
        Func<TItem, bool> match
    ) =>
        new Divergency<TItem>(sources.ToArray(), match);
}
