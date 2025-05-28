using System;
using System.Collections.Generic;
using Tonga.Scalar;

namespace Tonga.Enumerable;

/// <summary>
/// <see cref="IEnumerable{T}"/> whose items are reduced to one item using the given function.
/// </summary>
/// <typeparam name="T">type of elements in a list to reduce</typeparam>
public sealed class Reduced<T> : ScalarEnvelope<T>
{
    /// <summary>
    /// <see cref="IEnumerable{Element}"/> whose items are reduced to one item using the given function.
    /// </summary>
    /// <param name="elements">enumerable to reduce</param>
    public Reduced(IEnumerable<T> elements, Func<T, T, T> fnc) : this(
        elements, input => fnc(input.currentState, input.nextItem)
    )
    { }

    /// <summary>
    /// <see cref="IEnumerable{Element}"/> whose items are reduced to one item using the given function.
    /// </summary>
    public Reduced(IEnumerable<T> elements, Func<(T currentState, T nextItem), T> reduction) : base(() =>
        {
            var enm = elements.GetEnumerator();

            if (!enm.MoveNext()) throw new ArgumentException($"Cannot reduce, at least one element is needed but the enumerable is empty.");
            T result = enm.Current;
            while (enm.MoveNext())
            {
                result = reduction.Invoke((result, enm.Current));
            }
            return result;
        }
    )
    { }
}

/// <summary>
/// <see cref="IEnumerable{T}"/> whose items are reduced to one item using the given function.
/// </summary>
public static partial class EnumerableSmarts
{
    /// <summary>
    /// <see cref="IEnumerable{Element}"/> whose items are reduced to one item using the given function.
    /// </summary>
    /// <param name="elements">enumerable to reduce</param>
    /// <param name="fnc">reducing function</param>
    public static Reduced<T> Reduced<T>(this IEnumerable<T> elements, Func<T, T, T> fnc) => new(elements, fnc);

    /// <summary>
    /// <see cref="IEnumerable{Element}"/> whose items are reduced to one item using the given function.
    /// </summary>
    /// <param name="elements">enumerable to reduce</param>
    /// <param name="fnc">reducing function</param>
    public static Reduced<T> Reduced<T>(this IEnumerable<T> elements, Func<(T currentState, T nextItem), T> fnc) => new(elements, fnc);
}
