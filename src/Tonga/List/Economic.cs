using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.List;

/// <summary>
/// List which only advances to the necessary item but remembers it once it has seen it once.
/// </summary>
public class Economic<T>(Func<IEnumerator<T>> enumerator) : IList<T>
{
    private readonly Lazy<IEnumerator<T>> enumerator = new(enumerator);
    private readonly List<T> memory = new();

    /// <summary>
    /// List which only advances to the necessary item but remembers it once it has ssen it once.
    /// smart constructor.
    /// </summary>
    public Economic(IEnumerable<T> enumerable) : this(enumerable.GetEnumerator)
    {
    }

    public T this[int index]
    {
        get
        {
            MemoizeUntil(index);
            return this.memory[index];
        }
        set => RejectWrite();
    }

    public int Count
    {
        get
        {
            MemoizeAll();
            return this.memory.Count;
        }

    }

    public bool IsReadOnly => true;
    public void Add(T item) => RejectWrite();
    public void Clear() => RejectWrite();

    public bool Contains(T item)
    {
        var found = false;
        var enumerator = this.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator.Current.Equals(item))
            {
                found = true;
                break;
            }
        }

        return found;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        foreach (var item in Items())
            array[arrayIndex++] = item;
    }

    public IEnumerator<T> GetEnumerator() => Items().GetEnumerator();

    public int IndexOf(T item)
    {
        var enm = this.GetEnumerator();
        var index = -1;
        var found = false;
        while (enm.MoveNext())
        {
            index++;
            if (enm.Current.Equals(item))
            {
                found = true;
                break;
            }
        }

        return found ? index : -1;
    }

    public void Insert(int index, T item) => RejectWrite();

    public bool Remove(T item)
    {
        RejectWrite();
        return false;
    }

    public void RemoveAt(int index) => RejectWrite();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    private IEnumerable<T> Items()
    {
        foreach (var item in this.memory)
        {
            yield return item;
        }

        while (this.enumerator.Value.MoveNext())
        {
            this.memory.Add(this.enumerator.Value.Current);
            yield return this.enumerator.Value.Current;
        }
    }

    private void MemoizeUntil(int index)
    {
        while (this.memory.Count <= index && this.enumerator.Value.MoveNext())
            this.memory.Add(this.enumerator.Value.Current);
    }

    private void MemoizeAll()
    {
        while (this.enumerator.Value.MoveNext())
            this.memory.Add(this.enumerator.Value.Current);
    }

    private void RejectWrite() =>
        throw new InvalidOperationException("The list is readonly.");
}

public static partial class ListSmarts
{
    /// <summary>
    /// List which only advances to the necessary item but remembers it once it has ssen it once.
    /// </summary>
    public static IList<T> AsEconomic<T>(IEnumerable<T> origin) => new Economic<T>(origin);

    /// <summary>
    /// List which only advances to the necessary item but remembers it once it has ssen it once.
    /// </summary>
    public static IList<T> AsEconomic<T>(Func<IEnumerator<T>> origin) => new Economic<T>(origin);
}

