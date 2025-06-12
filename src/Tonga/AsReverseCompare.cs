using System.Collections.Generic;

namespace Tonga;

/// <summary>
/// <see cref="Comparer{T}"/> that can compare reverse to help reversing lists.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class AsReverseCompare<T>(Comparer<T> comparer) : Comparer<T>
{
    public new static readonly AsReverseCompare<T> Default = new(Comparer<T>.Default);
    public static AsReverseCompare<T> Reverse(Comparer<T> comparer) => new(comparer);
    public override int Compare(T x, T y) =>
        comparer.Compare(y, x);
}
