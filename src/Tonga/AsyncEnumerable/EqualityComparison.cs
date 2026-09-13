using System;
using System.Collections.Generic;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// An equality function as an <see cref="IEqualityComparer{T}"/>.
/// <para>
/// An arbitrary equality function admits no matching hash, so every item lands in one bucket and
/// lookups degrade to a linear scan. Prefer <see cref="EqualityComparer{T}.Default"/> wherever the
/// caller did not insist on its own comparison.
/// </para>
/// </summary>
internal sealed class EqualityComparison<T>(Func<T, T, bool> comparison) : IEqualityComparer<T>
{
    public bool Equals(T x, T y) => comparison(x, y);

    public int GetHashCode(T obj) => 0;
}
