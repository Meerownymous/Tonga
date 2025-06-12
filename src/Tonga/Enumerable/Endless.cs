

using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable;

/// <summary>
/// A <see cref="IEnumerable{T}"/> that repeats one element infinitely.
/// </summary>
/// <typeparam name="T">type of the elements</typeparam>
public sealed class Endless<T>(T elm) : IEnumerable<T>
{
    public IEnumerator<T> GetEnumerator()
    {
        while (true)
        {
            yield return elm;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() =>
        this.GetEnumerator();
}

public static partial class EnumerableSmarts
{
    /// <summary>
    /// An enumerable that repeats one element infinitely.
    /// </summary>
    public static IEnumerable<T> AsEndless<T>(this T elm) =>
        new Endless<T>(elm);
}
