using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable;

/// <summary>
/// Enumeration of a single item.
/// </summary>
public sealed class Single<T>(T item) : IEnumerable<T>
{
    public IEnumerator<T> GetEnumerator()
    {
        yield return item;
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}

public static partial class EnumerableSmarts
{
    public static IEnumerable<T> AsSingle<T>(this T item) => new AsEnumerable<T>(item);
}


