

using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Scalar;

/// <summary>
/// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class ScalarMorph<T>(Func<T> origin) : IScalar<T>
{
    /// <summary>
    /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
    /// </summary>
    /// <param name="src">func to cache result from</param>
    public ScalarMorph(T src) : this(() => src)
    {
    }

    /// <summary>
    /// Get the value.
    /// </summary>
    /// <returns>the value</returns>
    public T Value() => origin();
}

public static partial class ScalarSmarts
{
    /// <summary>
    /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
    /// </summary>
    /// <param name="src">func to cache result from</param>
    public static IScalar<T> ScalarMorph<T>(this Func<T> src) => new ScalarMorph<T>(src);

    /// <summary>
    /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
    /// </summary>
    /// <param name="src">func to cache result from</param>
    public static IScalar<T> ScalarMorph<T>(this T src) => new ScalarMorph<T>(src);

    /// <summary>
    /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
    /// </summary>
    /// <param name="src">func to cache result from</param>
    public static IEnumerable<IScalar<T>> ScalarMorphs<T>(this IEnumerable<T> src) =>
        src.AsMapped(item => item.ScalarMorph());

    /// <summary>
    /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
    /// </summary>
    /// <param name="src">func to cache result from</param>
    public static IEnumerable<IScalar<T>> ScalarMorphs<T>(this IEnumerable<Func<T>> src) =>
        src.AsMapped(item => item.ScalarMorph());
}
