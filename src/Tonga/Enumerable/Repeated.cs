

using System;
using System.Collections;
using System.Collections.Generic;
using Tonga.Scalar;

namespace Tonga.Enumerable;

/// <summary>
/// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
/// </summary>
/// <typeparam name="T">type of element to repeat</typeparam>
public sealed class Repeated<T>(Func<T> elm, Func<int> cnt) : IEnumerable<T>
{
    /// <summary>
    /// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
    /// </summary>
    /// <param name="elm">function to get element to repeat</param>
    /// <param name="cnt">how often to repeat</param>
    public Repeated(Func<T> elm, int cnt) : this(
        elm.AsScalar(),
        cnt
    )
    { }

    /// <summary>
    /// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
    /// </summary>
    /// <param name="elm">function to get element to repeat</param>
    /// <param name="cnt">how often to repeat</param>
    public Repeated(IScalar<T> elm, IScalar<int> cnt) : this(
        elm.Value,
        cnt.Value
    )
    { }

    /// <summary>
    /// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
    /// </summary>
    /// <param name="elm">element to repeat</param>
    /// <param name="cnt">how often to repeat</param>
    public Repeated(T elm, int cnt) : this(() => elm, cnt)
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="elm">scalar to get element to repeat</param>
    /// <param name="cnt">how often to repeat</param>
    public Repeated(IScalar<T> elm, int cnt) : this(
        elm.Value, () => cnt
    )
    { }

    public IEnumerator<T> GetEnumerator()
    {
        var times = cnt();
        var element = elm();
        for (int i = 0; i < times; i++)
        {
            yield return element;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}

public static partial class EnumerableSmarts
{
    /// <summary>
    /// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
    /// </summary>
    /// <param name="elm">function to get element to repeat</param>
    /// <param name="cnt">how often to repeat</param>
    public static IEnumerable<T> AsRepeated<T>(this Func<T> elm, int cnt) => new Repeated<T>(elm, cnt);

    /// <summary>
    /// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
    /// </summary>
    /// <param name="elm">function to get element to repeat</param>
    /// <param name="cnt">how often to repeat</param>
    public static IEnumerable<T> AsRepeated<T>(this Func<T> elm, Func<int> cnt) => new Repeated<T>(elm, cnt);

    /// <summary>
    /// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
    /// </summary>
    /// <param name="elm">element to repeat</param>
    /// <param name="cnt">how often to repeat</param>
    public static IEnumerable<T> AsRepeated<T>(this T elm, int cnt) => new Repeated<T>(elm, cnt);

    /// <summary>
    /// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
    /// </summary>
    /// <param name="elm">element to repeat</param>
    /// <param name="cnt">how often to repeat</param>
    public static IEnumerable<T> AsRepeated<T>(this T elm, Func<int> cnt) => new Repeated<T>(() => elm, cnt);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="elm">scalar to get element to repeat</param>
    /// <param name="cnt">how often to repeat</param>
    public static IEnumerable<T> AsRepeated<T>(this IScalar<T> elm, int cnt) => new Repeated<T>(elm, cnt);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="elm">scalar to get element to repeat</param>
    /// <param name="cnt">how often to repeat</param>
    public static IEnumerable<T> AsRepeated<T>(this IScalar<T> elm, IScalar<int> cnt) => new Repeated<T>(elm, cnt);
}
