

using System;
using System.Collections;
using System.Collections.Generic;
using Tonga.Enumerator;
using Tonga.Scalar;

namespace Tonga.Collection;

/// <summary>
/// Envelope for Collections. It enables ICollection classes from .Net to accept scalars.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class CollectionEnvelope<T>(Func<ICollection<T>> origin) : ICollection<T>
{
    /// <summary>
    /// scalar of collection
    /// </summary>
    private readonly InvalidOperationException readonlyError = new("The collection is readonly");

    /// <summary>
    /// Number of elements
    /// </summary>
    public int Count => origin().Count;

    /// <summary>
    /// Is the collection readonly?
    /// </summary>
    public bool IsReadOnly => true;

    /// <summary>
    /// Add an element
    /// </summary>
    /// <param name="item">Item to add</param>
    public void Add(T item) => throw this.readonlyError;

    /// <summary>
    /// Clear all items
    /// </summary>
    public void Clear() => throw this.readonlyError;

    /// <summary>
    /// Test if the collection contains an item
    /// </summary>
    /// <param name="item">Item to lookup</param>
    /// <returns>True if item is found</returns>
    public bool Contains(T item) => origin().Contains(item);

    /// <summary>
    /// Copies items from given index to target array
    /// </summary>
    /// <param name="array">Target array</param>
    /// <param name="arrayIndex">Index to start</param>
    public void CopyTo(T[] array, int arrayIndex) => origin().CopyTo(array, arrayIndex);

    /// <summary>
    /// A enumerator to iterate through the items.
    /// </summary>
    /// <returns></returns>
    public IEnumerator<T> GetEnumerator() => origin().GetEnumerator();

    /// <summary>
    /// Remove an item
    /// </summary>
    /// <param name="item">Item to remove</param>
    /// <returns>True if success</returns>
    public bool Remove(T item) => throw this.readonlyError;

    /// <summary>
    /// Get the enumerator to iterate through the items
    /// </summary>
    /// <returns></returns>
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
