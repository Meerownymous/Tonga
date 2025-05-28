

using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Scalar;

/// <summary>
/// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class AsScalar<T>(Func<T> origin) : IScalar<T>
{
    /// <summary>
    /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
    /// </summary>
    /// <param name="src">func to cache result from</param>
    public AsScalar(T src) : this(() => src)
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
    public static IScalar<T> AsScalar<T>(this Func<T> src) => new AsScalar<T>(src);

    /// <summary>
    /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
    /// </summary>
    /// <param name="src">func to cache result from</param>
    public static IScalar<T> AsScalar<T>(this T src) => new AsScalar<T>(src);

    /// <summary>
    /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
    /// </summary>
    /// <param name="src">func to cache result from</param>
    public static IEnumerable<IScalar<T>> AsScalars<T>(this IEnumerable<T> src) =>
        src.AsMapped(item => item.AsScalar());

    /// <summary>
    /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
    /// </summary>
    /// <param name="src">func to cache result from</param>
    public static IEnumerable<IScalar<T>> AsScalars<T>(this IEnumerable<Func<T>> src) =>
        src.AsMapped(item => item.AsScalar());
}
