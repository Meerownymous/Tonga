using System;
using System.Collections.Generic;
using Tonga.Fact;

namespace Tonga.Enumerable;

/// <summary>
/// Lookup if an item is in a enumerable.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class Contains<T>(IEnumerable<T> src, Func<T, bool> match) : FactEnvelope(
    new AsFact(() =>
        {
            var found = false;
            var enumerator = src.GetEnumerator();
            while(!found && enumerator.MoveNext())
            {
                found = match(enumerator.Current);
            }
            return found;
        })
    )
{
    /// <summary>
    /// Lookup if an item is in a enumerable by calling .Equals(...) of the item.
    /// </summary>
    /// <param name="item">item to lookup</param>
    /// <param name="src">enumerable to test</param>
    public Contains(IEnumerable<T> src, T item) : this(
        src,
        cdd => cdd.Equals(item))
    { }
}
