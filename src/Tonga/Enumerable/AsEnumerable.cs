using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> out of other objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class AsEnumerable<T>(Func<IEnumerator<T>> source) :
        IEnumerable<T>
    {
        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of an array.
        /// </summary>
        /// <param name="items"></param>
        public AsEnumerable(params T[] items) : this(
            () => new Enumerator.Array<T>(items)
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="origin">function which retrieves enumerator</param>
        public AsEnumerable(IEnumerable<T> origin) : this(origin.GetEnumerator)
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="origin">function which retrieves enumerator</param>
        public AsEnumerable(Func<IEnumerable<T>> origin) : this(
            () => origin().GetEnumerator()
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="origin">function which retrieves enumerator</param>
        public AsEnumerable(IEnumerator<T> origin) : this(() => origin)
        { }

        public IEnumerator<T> GetEnumerator()
        {
            var enumerator = source();
            while(enumerator.MoveNext())
                yield return enumerator.Current;
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    public static partial class EnumerableSmarts
    {
        public static IEnumerable<TItem> AsEnumerable<TItem>(this TItem[] source) =>
            new AsEnumerable<TItem>(source);

        public static IEnumerable<TItem> AsEnumerable<TItem>(this Func<IEnumerable<TItem>> source) =>
            new AsEnumerable<TItem>(source);

        public static IEnumerable<TItem> AsEnumerable<TItem>(this Func<IEnumerator<TItem>> origin) =>
            new AsEnumerable<TItem>(origin);

        public static IEnumerable<TItem> AsEnumerable<TItem>(this IEnumerator<TItem> origin) =>
            new AsEnumerable<TItem>(origin);

        public static IEnumerable<TItem> AsEnumerable<TItem>(this (TItem a, TItem b) origin) =>
            new AsEnumerable<TItem>(origin.a, origin.b);

        public static IEnumerable<TItem> AsEnumerable<TItem>(this (TItem a, TItem b, TItem c) origin) =>
            new AsEnumerable<TItem>(origin.a, origin.b, origin.c);

        public static IEnumerable<TItem> AsEnumerable<TItem>(this (TItem a, TItem b, TItem c, TItem d) origin) =>
            new AsEnumerable<TItem>(origin.a, origin.b, origin.c, origin.d);

        public static IEnumerable<TItem> AsEnumerable<TItem>(this (TItem a, TItem b, TItem c, TItem d, TItem e) origin) =>
            new AsEnumerable<TItem>(origin.a, origin.b, origin.c, origin.d, origin.e);

        public static IEnumerable<TItem> AsEnumerable<TItem>(this (TItem a, TItem b, TItem c, TItem d, TItem e, TItem f) origin) =>
            new AsEnumerable<TItem>(origin.a, origin.b, origin.c, origin.d, origin.e, origin.f);

        public static IEnumerable<TItem> AsEnumerable<TItem>(this (TItem a, TItem b, TItem c, TItem d, TItem e, TItem f, TItem g) origin) =>
            new AsEnumerable<TItem>(origin.a, origin.b, origin.c, origin.d, origin.e, origin.f, origin.g);

        public static IEnumerable<TItem> AsEnumerable<TItem>(this
            (TItem a, TItem b, TItem c, TItem d, TItem e, TItem f, TItem g, TItem h, TItem i) origin
        ) =>
            new AsEnumerable<TItem>(origin.a, origin.b, origin.c, origin.d, origin.e, origin.f, origin.g, origin.h, origin.i);

        public static IEnumerable<TItem> AsEnumerable<TItem>(this
            (TItem a, TItem b, TItem c, TItem d, TItem e, TItem f, TItem g, TItem h, TItem i, TItem j) origin) =>
            new AsEnumerable<TItem>(origin.a, origin.b, origin.c, origin.d, origin.e, origin.f, origin.g, origin.h, origin.i, origin.j);

        public static IEnumerable<TItem> AsEnumerable<TItem>(this
            (TItem a, TItem b, TItem c, TItem d, TItem e, TItem f, TItem g, TItem h, TItem i, TItem j, TItem k)
        origin) =>
            new AsEnumerable<TItem>(
                origin.a, origin.b, origin.c, origin.d, origin.e, origin.f, origin.g, origin.h, origin.i, origin.j, origin.k
            );

        public static IEnumerable<TItem> AsEnumerable<TItem>(this
            (TItem a, TItem b, TItem c, TItem d, TItem e, TItem f, TItem g, TItem h, TItem i, TItem j, TItem k, TItem l)
            origin) =>
            new AsEnumerable<TItem>(
                origin.a, origin.b, origin.c, origin.d, origin.e, origin.f, origin.g, origin.h, origin.i, origin.j, origin.k, origin.l
            );
    }
}
