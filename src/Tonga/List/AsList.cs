using System;
using System.Collections;
using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.List;

/// <summary>
/// A readonly list.
/// </summary>
/// <typeparam name="T">type of items</typeparam>
public sealed class AsList<T>(Func<IEnumerable<T>> origin) : IList<T>
{
    private readonly InvalidOperationException readOnlyError = new("The list is readonly.");

    /// <summary>
    /// ctor
    /// </summary>
    public AsList(params T[] items) : this(() => new AsEnumerable<T>(items))
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="src">source enumerable</param>
    public AsList(IEnumerable<T> src) : this(() => src)
    { }

    public T this[int index]
    {
        get
        {
            T result = default(T);
            var found = false;
            var current = -1;
            var items = origin();
            foreach(var item in items)
            {
                current++;
                if(current == index)
                {
                    found = true;
                    result = item;
                    break;
                }
            }
            if (!found)
                throw new ArgumentException($"Cannot get item at {index} - only {current + 1} are in the list.");
            return result;
        }
        set => throw this.readOnlyError;
    }

    public int Count => (int)origin().Length().Value();
    public bool IsReadOnly => true;
    public void Add(T item) => throw this.readOnlyError;
    public void Clear() => throw this.readOnlyError;

    public bool Contains(T item)
    {
        var found = false;
        var current = -1;
        foreach (var candidate in origin())
        {
            current++;
            if (item.Equals(candidate))
            {
                found = true;
                break;
            }
        }
        return found;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        foreach(var item in origin())
        {
            array[arrayIndex++] = item;
        }
    }

    public IEnumerator<T> GetEnumerator() => origin().GetEnumerator();

    public int IndexOf(T item)
    {
        var found = false;
        var index = -1;
        foreach (var candidate in origin())
        {
            index++;
            if (item.Equals(candidate))
            {
                found = true;
                break;
            }
        }
        return found ? index : -1;
    }

    public void Insert(int index, T item) => throw this.readOnlyError;
    public bool Remove(T item) => throw this.readOnlyError;
    public void RemoveAt(int index) => throw this.readOnlyError;
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}

public static partial class ListSmarts
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="src">source enumerable</param>
    public static IList<string> AsList(this string[] src)
        => new AsList<string>(src);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="src">source enumerable</param>
    public static IList<T> AsList<T>(this Func<IEnumerable<T>> src)
        => new AsList<T>(src);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="src">source enumerable</param>
    public static IList<T> AsList<T>(this IEnumerable<T> src)
        => new AsList<T>(src);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="src">source enumerable</param>
    public static IList<T> AsList<T>(this (T i1, T i2) src)
        => new AsList<T>(src.i1, src.i2);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="src">source enumerable</param>
    public static IList<T> AsList<T>(this (T i1, T i2, T i3) src)
        => new AsList<T>(src.i1, src.i2, src.i3);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="src">source enumerable</param>
    public static IList<T> AsList<T>(this (T i1, T i2, T i3, T i4) src)
        => new AsList<T>(src.i1, src.i2, src.i3, src.i4);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="src">source enumerable</param>
    public static IList<T> AsList<T>(this (T i1, T i2, T i3, T i4, T i5) src)
        => new AsList<T>(src.i1, src.i2, src.i3, src.i4, src.i5);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="src">source enumerable</param>
    public static IList<T> AsList<T>(this (T i1, T i2, T i3, T i4, T i5, T i6) src)
        => new AsList<T>(src.i1, src.i2, src.i3, src.i4, src.i5, src.i6);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="src">source enumerable</param>
    public static IList<T> AsList<T>(this (T i1, T i2, T i3, T i4, T i5, T i6, T i7) src)
        => new AsList<T>(src.i1, src.i2, src.i3, src.i4, src.i5, src.i6, src.i7);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="src">source enumerable</param>
    public static IList<T> AsList<T>(this (T i1, T i2, T i3, T i4, T i5, T i6, T i7, T i8) src)
        => new AsList<T>(src.i1, src.i2, src.i3, src.i4, src.i5, src.i6, src.i7, src.i8);


}
