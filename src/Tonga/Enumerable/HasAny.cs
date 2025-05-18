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

/// <summary>
/// Tells if an enumerable source has any elements.
/// </summary>
public static class HasAnySmarts
{
    public static HasAny<T> HasAny<T>(IEnumerable<T> source) => new(source);
    public static HasAny<T> HasAny<T>(params T[] source) => new(source);
}
