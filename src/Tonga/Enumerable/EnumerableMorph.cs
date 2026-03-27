using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> out of other objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class EnumerableMorph<T>(Func<IEnumerator<T>> source) :
        IEnumerable<T>
    {
        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of an array.
        /// </summary>
        /// <param name="items"></param>
        public EnumerableMorph(params T[] items) : this(
            () => new Enumerator.Array<T>(items)
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="origin">function which retrieves enumerator</param>
        public EnumerableMorph(IEnumerable<T> origin) : this(origin.GetEnumerator)
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="origin">function which retrieves enumerator</param>
        public EnumerableMorph(Func<IEnumerable<T>> origin) : this(
            () => origin().GetEnumerator()
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="origin">function which retrieves enumerator</param>
        public EnumerableMorph(IEnumerator<T> origin) : this(() => origin)
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
        public static IEnumerable<TItem> EnumerableMorph<TItem>(this TItem[] source) =>
            new EnumerableMorph<TItem>(source);

        public static IEnumerable<TItem> EnumerableMorph<TItem>(this Func<IEnumerable<TItem>> source) =>
            new EnumerableMorph<TItem>(source);

        public static IEnumerable<TItem> EnumerableMorph<TItem>(this Func<IEnumerator<TItem>> origin) =>
            new EnumerableMorph<TItem>(origin);

        public static IEnumerable<TItem> EnumerableMorph<TItem>(this IEnumerator<TItem> origin) =>
            new EnumerableMorph<TItem>(origin);

        public static IEnumerable<TItem> EnumerableMorph<TItem>(this (TItem a, TItem b) origin) =>
            new EnumerableMorph<TItem>(origin.a, origin.b);

        public static IEnumerable<TItem> EnumerableMorph<TItem>(this (TItem a, TItem b, TItem c) origin) =>
            new EnumerableMorph<TItem>(origin.a, origin.b, origin.c);

        public static IEnumerable<TItem> EnumerableMorph<TItem>(this (TItem a, TItem b, TItem c, TItem d) origin) =>
            new EnumerableMorph<TItem>(origin.a, origin.b, origin.c, origin.d);

        public static IEnumerable<TItem> EnumerableMorph<TItem>(this (TItem a, TItem b, TItem c, TItem d, TItem e) origin) =>
            new EnumerableMorph<TItem>(origin.a, origin.b, origin.c, origin.d, origin.e);

        public static IEnumerable<TItem> EnumerableMorph<TItem>(this (TItem a, TItem b, TItem c, TItem d, TItem e, TItem f) origin) =>
            new EnumerableMorph<TItem>(origin.a, origin.b, origin.c, origin.d, origin.e, origin.f);

        public static IEnumerable<TItem> EnumerableMorph<TItem>(this (TItem a, TItem b, TItem c, TItem d, TItem e, TItem f, TItem g) origin) =>
            new EnumerableMorph<TItem>(origin.a, origin.b, origin.c, origin.d, origin.e, origin.f, origin.g);

        public static IEnumerable<TItem> EnumerableMorph<TItem>(this
            (TItem a, TItem b, TItem c, TItem d, TItem e, TItem f, TItem g, TItem h, TItem i) origin
        ) =>
            new EnumerableMorph<TItem>(origin.a, origin.b, origin.c, origin.d, origin.e, origin.f, origin.g, origin.h, origin.i);

        public static IEnumerable<TItem> EnumerableMorph<TItem>(this
            (TItem a, TItem b, TItem c, TItem d, TItem e, TItem f, TItem g, TItem h, TItem i, TItem j) origin) =>
            new EnumerableMorph<TItem>(origin.a, origin.b, origin.c, origin.d, origin.e, origin.f, origin.g, origin.h, origin.i, origin.j);

        public static IEnumerable<TItem> EnumerableMorph<TItem>(this
            (TItem a, TItem b, TItem c, TItem d, TItem e, TItem f, TItem g, TItem h, TItem i, TItem j, TItem k)
        origin) =>
            new EnumerableMorph<TItem>(
                origin.a, origin.b, origin.c, origin.d, origin.e, origin.f, origin.g, origin.h, origin.i, origin.j, origin.k
            );

        public static IEnumerable<TItem> EnumerableMorph<TItem>(this
            (TItem a, TItem b, TItem c, TItem d, TItem e, TItem f, TItem g, TItem h, TItem i, TItem j, TItem k, TItem l)
            origin) =>
            new EnumerableMorph<TItem>(
                origin.a, origin.b, origin.c, origin.d, origin.e, origin.f, origin.g, origin.h, origin.i, origin.j, origin.k, origin.l
            );
    }
}
