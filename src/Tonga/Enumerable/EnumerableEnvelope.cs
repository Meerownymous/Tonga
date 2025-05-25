

using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable;

/// <summary>
/// Envelope for Enumerable.
/// It bundles the methods offered by IEnumerable and enables scalar based ctors.
/// </summary>
public abstract class EnumerableEnvelope<T>(Func<IEnumerable<T>> origin) : IEnumerable<T>
{
    /// <summary>
    /// Envelope for Enumerable.
    /// It bundles the methods offered by IEnumerable and enables scalar based ctors.
    /// </summary>
    public EnumerableEnvelope(IEnumerable<T> origin) :this(() => origin)
    { }

    /// <summary>
    /// Enumerator for this envelope.
    /// </summary>
    /// <returns>The enumerator</returns>
    public IEnumerator<T> GetEnumerator() => origin().GetEnumerator();

    /// <summary>
    /// Enumerator for this envelope.
    /// </summary>
    /// <returns>The enumerator</returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
