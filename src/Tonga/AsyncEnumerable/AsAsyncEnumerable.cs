using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// A <see cref="IAsyncEnumerable{T}"/> out of other objects.
/// </summary>
public sealed class AsAsyncEnumerable<T> : IAsyncEnumerable<T>
{
    private readonly Func<CancellationToken, IAsyncEnumerator<T>> source;
    private readonly bool owned;

    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> out of a function which retrieves an enumerator.
    /// The enumerator is created here, so it is disposed here.
    /// </summary>
    public AsAsyncEnumerable(Func<CancellationToken, IAsyncEnumerator<T>> source) : this(source, true)
    { }

    private AsAsyncEnumerable(Func<CancellationToken, IAsyncEnumerator<T>> source, bool owned)
    {
        this.source = source;
        this.owned = owned;
    }

    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> out of an array.
    /// </summary>
    public AsAsyncEnumerable(params T[] items) : this(
        cancellation => Produced(items, cancellation)
    )
    { }

    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> out of a sync <see cref="IEnumerable{T}"/>.
    /// </summary>
    public AsAsyncEnumerable(IEnumerable<T> origin) : this(
        cancellation => Produced(origin, cancellation)
    )
    { }

    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> out of another one.
    /// </summary>
    public AsAsyncEnumerable(IAsyncEnumerable<T> origin) : this(origin.GetAsyncEnumerator)
    { }

    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> out of a function which retrieves one.
    /// </summary>
    public AsAsyncEnumerable(Func<IAsyncEnumerable<T>> origin) : this(
        cancellation => origin().GetAsyncEnumerator(cancellation)
    )
    { }

    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> out of a <see cref="IAsyncEnumerator{T}"/>.
    /// The enumerator belongs to the caller, so it is not disposed here.
    /// </summary>
    public AsAsyncEnumerable(IAsyncEnumerator<T> origin) : this(_ => origin, false)
    { }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var enumerator = this.source(cancellation);
        try
        {
            while (await enumerator.MoveNextAsync())
                yield return enumerator.Current;
        }
        finally
        {
            if (this.owned)
                await enumerator.DisposeAsync();
        }
    }

    private static async IAsyncEnumerator<T> Produced(
        IEnumerable<T> source, [EnumeratorCancellation] CancellationToken cancellation
    )
    {
        foreach (var item in source)
        {
            cancellation.ThrowIfCancellationRequested();
            yield return item;
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    public static IAsyncEnumerable<TItem> AsAsyncEnumerable<TItem>(this TItem[] source) =>
        new AsAsyncEnumerable<TItem>(source);

    public static IAsyncEnumerable<TItem> AsAsyncEnumerable<TItem>(this IEnumerable<TItem> source) =>
        new AsAsyncEnumerable<TItem>(source);

    public static IAsyncEnumerable<TItem> AsAsyncEnumerable<TItem>(this Func<IAsyncEnumerable<TItem>> source) =>
        new AsAsyncEnumerable<TItem>(source);

    public static IAsyncEnumerable<TItem> AsAsyncEnumerable<TItem>(this IAsyncEnumerator<TItem> origin) =>
        new AsAsyncEnumerable<TItem>(origin);

    public static IAsyncEnumerable<TItem> AsAsyncEnumerable<TItem>(this (TItem a, TItem b) origin) =>
        new AsAsyncEnumerable<TItem>(origin.a, origin.b);

    public static IAsyncEnumerable<TItem> AsAsyncEnumerable<TItem>(this (TItem a, TItem b, TItem c) origin) =>
        new AsAsyncEnumerable<TItem>(origin.a, origin.b, origin.c);

    public static IAsyncEnumerable<TItem> AsAsyncEnumerable<TItem>(this (TItem a, TItem b, TItem c, TItem d) origin) =>
        new AsAsyncEnumerable<TItem>(origin.a, origin.b, origin.c, origin.d);

    public static IAsyncEnumerable<TItem> AsAsyncEnumerable<TItem>(
        this (TItem a, TItem b, TItem c, TItem d, TItem e) origin) =>
        new AsAsyncEnumerable<TItem>(origin.a, origin.b, origin.c, origin.d, origin.e);

    public static IAsyncEnumerable<TItem> AsAsyncEnumerable<TItem>(
        this (TItem a, TItem b, TItem c, TItem d, TItem e, TItem f) origin) =>
        new AsAsyncEnumerable<TItem>(origin.a, origin.b, origin.c, origin.d, origin.e, origin.f);

    public static IAsyncEnumerable<TItem> AsAsyncEnumerable<TItem>(
        this (TItem a, TItem b, TItem c, TItem d, TItem e, TItem f, TItem g) origin) =>
        new AsAsyncEnumerable<TItem>(origin.a, origin.b, origin.c, origin.d, origin.e, origin.f, origin.g);

    public static IAsyncEnumerable<TItem> AsAsyncEnumerable<TItem>(
        this (TItem a, TItem b, TItem c, TItem d, TItem e, TItem f, TItem g, TItem h, TItem i) origin) =>
        new AsAsyncEnumerable<TItem>(
            origin.a, origin.b, origin.c, origin.d, origin.e, origin.f, origin.g, origin.h, origin.i
        );

    public static IAsyncEnumerable<TItem> AsAsyncEnumerable<TItem>(
        this (TItem a, TItem b, TItem c, TItem d, TItem e, TItem f, TItem g, TItem h, TItem i, TItem j) origin) =>
        new AsAsyncEnumerable<TItem>(
            origin.a, origin.b, origin.c, origin.d, origin.e, origin.f, origin.g, origin.h, origin.i, origin.j
        );

    public static IAsyncEnumerable<TItem> AsAsyncEnumerable<TItem>(
        this (TItem a, TItem b, TItem c, TItem d, TItem e, TItem f, TItem g, TItem h, TItem i, TItem j, TItem k) origin) =>
        new AsAsyncEnumerable<TItem>(
            origin.a, origin.b, origin.c, origin.d, origin.e, origin.f, origin.g, origin.h, origin.i, origin.j, origin.k
        );

    public static IAsyncEnumerable<TItem> AsAsyncEnumerable<TItem>(
        this (TItem a, TItem b, TItem c, TItem d, TItem e, TItem f, TItem g, TItem h, TItem i, TItem j, TItem k, TItem l)
            origin) =>
        new AsAsyncEnumerable<TItem>(
            origin.a, origin.b, origin.c, origin.d, origin.e, origin.f, origin.g, origin.h, origin.i, origin.j,
            origin.k, origin.l
        );
}
