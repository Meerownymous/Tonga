using System.Collections.Generic;
using Tonga.Fact;

namespace Tonga.Enumerable;

/// <summary>
/// Tells if an enumerable source has any elements.
/// </summary>
public sealed class IsEmpty<T> : FactEnvelope
{
    /// <summary>
    /// Tells if an enumerable source has any elements.
    /// </summary>
    public IsEmpty(IEnumerable<T> origin) : base(
        new AsFact(() =>
        {
            using var e = origin.GetEnumerator();
            return e.MoveNext();
        })
    ){ }

    /// <summary>
    /// Tells if an enumerable source has any elements.
    /// </summary>
    public IsEmpty(params T[] origin) : base(
        new AsFact(origin.Length > 0)
    )
    { }
}

public static partial class EnumerableSmarts
{
    public static IsEmpty<T> IsEmpty<T>(this IEnumerable<T> origin) => new(origin);
    public static IsEmpty<T> IsEmpty<T>(this T[] origin) => new(origin);
}
