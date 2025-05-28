using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable;

/// <summary>
/// Enumerable which executes a given lambda function when advancing.
/// </summary>
public sealed class OnEach<T>(Action<T, int> action, IEnumerable<T> origin) : IEnumerable<T>
{
    /// <summary>
    /// Enumerable which executes a given lambda function when advancing.
    /// </summary>
    public OnEach(Action<int> lambda, IEnumerable<T> origin) : this((_, count) => lambda(count), origin)
    { }

    /// <summary>
    /// Enumerable which executes a given lambda function when advancing.
    /// </summary>
    public OnEach(Action<T> lambda, IEnumerable<T> origin) : this((item, _) => lambda(item), origin)
    { }

    /// <summary>
    /// Enumerable which executes a given lambda function when advancing.
    /// </summary>
    public OnEach(Action lambda, IEnumerable<T> origin) : this((_, _) => lambda(), origin)
    { }

    public IEnumerator<T> GetEnumerator()
    {
        var index = -1;
        foreach (var item in origin)
        {
            action(item, index++);
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}

/// <summary>
/// Enumerable which executes a given lambda function when advancing.
/// </summary>
public static partial class EnumerableSmarts
{
    /// <summary>
    /// Enumerable which executes a given lambda function when advancing.
    /// </summary>
    public static IEnumerable<T> OnEach<T>(this IEnumerable<T> origin, Action lambda) =>
        new OnEach<T>(lambda, origin);

    /// <summary>
    /// Enumerable which executes a given lambda function when advancing.
    /// </summary>
    public static IEnumerable<T> OnEach<T>(this IEnumerable<T> origin, Action<T> lambda) =>
        new OnEach<T>(lambda, origin);

    /// <summary>
    /// Enumerable which executes a given lambda function when advancing.
    /// </summary>
    public static IEnumerable<T> OnEach<T>(this IEnumerable<T> origin, Action<T, int> lambda) =>
        new OnEach<T>(lambda, origin);

    /// <summary>
    /// Enumerable which executes a given lambda function when advancing.
    /// </summary>
    public static IEnumerable<T> OnEach<T>(this IEnumerable<T> origin, Action<int> lambda) =>
        new OnEach<T>(lambda, origin);
}

