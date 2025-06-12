using System.Collections.Generic;
using Tonga.Fact;

namespace Tonga.Enumerable;

/// <summary>
/// Tells if an enumerable source has any elements.
/// </summary>
public sealed class HasAny<T> : FactEnvelope
{
    /// <summary>
    /// Tells if an enumerable source has any elements.
    /// </summary>
    public HasAny(IEnumerable<T> origin) : base(
        new AsFact(() =>
        {
            using var e = origin.GetEnumerator();
            return e.MoveNext();
        })
    ){ }

    /// <summary>
    /// Tells if an enumerable source has any elements.
    /// </summary>
    public HasAny(params T[] origin) : base(
        new AsFact(origin.Length > 0)
    )
    { }
}

public static partial class EnumerableSmarts
{
    public static HasAny<T> HasAny<T>(this IEnumerable<T> origin) => new(origin);
    public static HasAny<T> HasAny<T>(this T[] origin) => new(origin);
}
