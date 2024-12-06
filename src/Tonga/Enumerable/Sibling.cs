

using System;
using System.Collections.Generic;
using System.IO;
using Tonga.Func;
using Tonga.Scalar;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Element before or after another element in a <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T">type of element</typeparam>
    public sealed class Sibling<T>(
        T item,
        IEnumerable<T> source,
        int relativeposition,
        IFunc<IEnumerable<T>, T> fallback
    ) : ScalarEnvelope<T>(
        () =>
        {
            var trace = new Queue<T>();
            var itemFound = false;
            T result = default;
            using var enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (!itemFound && item.CompareTo(enumerator.Current) == 0)
                {
                    itemFound = true;
                }

                if (relativeposition < 0)
                {
                    if (!itemFound)
                    {
                        trace.Enqueue(enumerator.Current);
                        if (trace.Count > -relativeposition)
                        {
                            trace.Dequeue();
                        }
                    }
                    else
                    {
                        result = trace.Count < -relativeposition
                            ? fallback.Invoke(source)
                            : trace.ToArray()[-relativeposition - 1];
                        break;
                    }
                }
                else if (itemFound)
                {
                    for (int i = 0; i < relativeposition && enumerator.MoveNext(); i++) { }

                    result = relativeposition > 0 ? fallback.Invoke(source) : enumerator.Current;
                    break;
                }
            }

            if (result == null || !itemFound)
            {
                result = fallback.Invoke(source);
            }

            return result;

        })
        where T : IComparable<T>
    {
        /// <summary>
        /// Next neighbour element in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="item">item to start</param>
        public Sibling(T item, IEnumerable<T> source) : this(
            item,
            source,
            new FuncOf<IEnumerable<T>, T>(_ => throw new ArgumentException("Can't get neighbour from iterable"))
        )
        {
        }

        /// <summary>
        /// Next neighbour in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="item">item to start</param>
        public Sibling(T item, IEnumerable<T> source, T fallback) : this(item, source, 1,
            new FuncOf<IEnumerable<T>, T>(b => fallback))
        {
        }

        /// <summary>
        /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="relativeposition">requested position relative to the given item</param>
        /// <param name="item">item to start</param>
        public Sibling(T item, IEnumerable<T> source, int relativeposition) : this(item, source, relativeposition,
            new FuncOf<IEnumerable<T>, T>(itr =>
            {
                throw new IOException($"Can't get neighbour at position {relativeposition} from iterable");
            }))
        {
        }

        /// <summary>
        /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="relativeposition">requested position relative to the given item</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="item">item to start</param>
        public Sibling(T item, IEnumerable<T> source, int relativeposition, T fallback) : this(item, source,
            relativeposition, new FuncOf<IEnumerable<T>, T>(b => fallback))
        {
        }

        /// <summary>
        /// Next neighbour of an item in a <see cref="IEnumerable{T}"/> with a fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">soruce enum</param>
        /// <param name="fallback">fallback value</param>
        /// <param name="item">item to start</param>
        public Sibling(T item, IEnumerable<T> source, IFunc<IEnumerable<T>, T> fallback) : this(item, source, 1,
            fallback)
        {
        }
    }

    public static class Sibling
    {

        /// <summary>
        /// Next neighbour element in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="item">item to start</param>
        public static IScalar<T> _<T>(T item, IEnumerable<T> source)
            where T : IComparable<T> =>
            new Sibling<T>(item, source);

        /// <summary>
        /// Next neighbour in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="item">item to start</param>
        public static IScalar<T> _<T>(T item, IEnumerable<T> source, T fallback)
            where T : IComparable<T> =>
            new Sibling<T>(item, source, fallback);

        /// <summary>
        /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="relativeposition">requested position relative to the given item</param>
        /// <param name="item">item to start</param>
        public static IScalar<T> _<T>(T item, IEnumerable<T> source, int relativeposition)
            where T : IComparable<T> =>
            new Sibling<T>(item, source, relativeposition);

        /// <summary>
        /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="relativeposition">requested position relative to the given item</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="item">item to start</param>
        public static IScalar<T> _<T>(T item, IEnumerable<T> source, int relativeposition, T fallback)
            where T : IComparable<T> =>
            new Sibling<T>(item, source, relativeposition, fallback);

        /// <summary>
        /// Next neighbour of an item in a <see cref="IEnumerable{T}"/> with a fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">soruce enum</param>
        /// <param name="fallback">fallback value</param>
        /// <param name="item">item to start</param>
        public static IScalar<T> _<T>(T item, IEnumerable<T> source, IFunc<IEnumerable<T>, T> fallback)
            where T : IComparable<T> =>
            new Sibling<T>(item, source, fallback);

        /// <summary>
        /// Element that comes before another element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="item">item to start</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="relativeposition">requested position relative to the given item</param>
        public static IScalar<T> _<T>(T item, IEnumerable<T> source, int relativeposition, IFunc<IEnumerable<T>, T> fallback)
            where T : IComparable<T> =>
            new Sibling<T>(item, source, relativeposition, fallback);
    }

        public static class SiblingSmarts
    {

        /// <summary>
        /// Next neighbour element in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        public static IScalar<T> Sibling<T>(this IEnumerable<T> source, T item)
            where T : IComparable<T> =>
            new Sibling<T>(item, source);

        /// <summary>
        /// Next neighbour in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        public static IScalar<T> Sibling<T>(this IEnumerable<T> source, T item, T fallback)
            where T : IComparable<T> =>
            new Sibling<T>(item, source, fallback);

        /// <summary>
        /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="relativeposition">requested position relative to the given item</param>
        /// <param name="item">item to start</param>
        public static IScalar<T> Sibling<T>(this IEnumerable<T> source, T item, int relativeposition)
            where T : IComparable<T> =>
            new Sibling<T>(item, source, relativeposition);

        /// <summary>
        /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="relativeposition">requested position relative to the given item</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="item">item to start</param>
        public static IScalar<T> Sibling<T>(this IEnumerable<T> source, T item, int relativeposition, T fallback)
            where T : IComparable<T> =>
            new Sibling<T>(item, source, relativeposition, fallback);

        /// <summary>
        /// Next neighbour of an item in a <see cref="IEnumerable{T}"/> with a fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">soruce enum</param>
        /// <param name="fallback">fallback value</param>
        /// <param name="item">item to start</param>
        public static IScalar<T> Sibling<T>(this IEnumerable<T> source, T item, IFunc<IEnumerable<T>, T> fallback)
            where T : IComparable<T> =>
            new Sibling<T>(item, source, fallback);

        /// <summary>
        /// Element that comes before another element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="item">item to start</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="relativeposition">requested position relative to the given item</param>
        public static IScalar<T> Sibling<T>(this IEnumerable<T> source, T item, int relativeposition, IFunc<IEnumerable<T>, T> fallback)
            where T : IComparable<T> =>
            new Sibling<T>(item, source, relativeposition, fallback);
    }
}
