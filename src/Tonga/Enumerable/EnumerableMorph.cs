using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable;

/// <summary>
/// Implicitly morph from various sources to enumerable.
/// </summary>
/// <param name="seed"></param>
/// <typeparam name="T"></typeparam>
public sealed class EnumerableMorph<T>(IEnumerable<T> seed) : IEnumerable<T>
{
    public IEnumerator<T> GetEnumerator() => seed.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => seed.GetEnumerator();

    public static implicit operator EnumerableMorph<T>(T[] items) => new(new AsEnumerable<T>(items));
}
