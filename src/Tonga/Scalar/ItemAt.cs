using System;
using System.Collections.Generic;
using Tonga.Func;
using Tonga.Text;

namespace Tonga.Scalar
{
    /// <summary>
    /// Element from position in a <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T">type of element</typeparam>
    public sealed class ItemAt<T> : ScalarEnvelope<T>
    {
        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with given Exception thrwon on fallback
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public ItemAt(IEnumerable<T> source, Exception ex) : this(
            source,
            0,
            ex
        )
        { }

        /// <summary>
        /// Element at position in <see cref="IEnumerable{T}"/> with given Exception thrown on fallback
        /// </summary>
        /// <param name="source"></param>
        /// <param name="position"></param>
        /// <param name="ex"></param>
        public ItemAt(IEnumerable<T> source, int position, Exception ex) : this(
            source,
            position,
            new AsFunc<IEnumerable<T>, T>(_ => throw ex)
        )
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        public ItemAt(IEnumerable<T> source) : this(
            source,
            new AsFunc<Exception, IEnumerable<T>, T>((ex, itr) =>
                throw new ArgumentException(
                    new Formatted("Cannot get first element: {0}", ex.Message).AsString()
                )
            )
        )
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public ItemAt(IEnumerable<T> source, T fallback) : this(
            source,
            new AsFunc<IEnumerable<T>, T>(b => fallback)
        )
        { }

        /// <summary>
        /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="position">position</param>
        /// <param name="fallback">fallback func</param>
        public ItemAt(IEnumerable<T> source, int position, T fallback) : this(
            source,
            position,
            new AsFunc<IEnumerable<T>, T>(b => fallback)
        )
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">soruce enum</param>
        /// <param name="fallback">fallback value</param>
        public ItemAt(IEnumerable<T> source, IFunc<Exception, IEnumerable<T>, T> fallback) : this(
            source,
            0,
            fallback
        )
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">soruce enum</param>
        /// <param name="fallback">fallback value</param>
        public ItemAt(IEnumerable<T> source, Func<IEnumerable<T>, T> fallback) : this(
            source,
            0,
            new AsFunc<IEnumerable<T>, T>(fallback)
        )
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">soruce enum</param>
        /// <param name="fallback">fallback value</param>
        public ItemAt(IEnumerable<T> source, IFunc<IEnumerable<T>, T> fallback) : this(
            source,
            0,
            fallback
        )
        { }

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="position">position of item</param>
        public ItemAt(IEnumerable<T> source, int position) : this(
                source,
                position,
                new AsFunc<Exception, IEnumerable<T>, T>((ex, itr) =>
                {
                    throw
                        new ArgumentException(
                            new Formatted(
                                "Cannot get element at position {0}: {1}",
                                position+1,
                                ex.Message,
                                position
                            ).AsString()
                    );
                }
            )
        )
        { }

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="position">position of item</param>
        /// <param name="fallback">fallback func</param>
        public ItemAt(IEnumerable<T> source, int position, IFunc<IEnumerable<T>, T> fallback) : this(
            source,
            position,
            (ex, enumerable) => fallback.Invoke(enumerable)
        )
        { }

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="position">position of item</param>
        /// <param name="fallback">fallback func</param>
        public ItemAt(IEnumerable<T> source, int position, Func<IEnumerable<T>, T> fallback) : this(
            source,
            position,
            new AsFunc<Exception, IEnumerable<T>, T>((ex, enumerable) =>
                fallback.Invoke(enumerable)
            )
        )
        { }

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="position">position of item</param>
        /// <param name="fallback">fallback func</param>
        public ItemAt(IEnumerable<T> source, int position, Func<Exception, IEnumerable<T>, T> fallback) : this(
            source,
            position,
            new AsFunc<Exception, IEnumerable<T>, T>((ex, enumerable) =>
                fallback.Invoke(ex, enumerable)
            )
        )
        { }

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="position">position of item</param>
        /// <param name="fallback">fallback func</param>
        public ItemAt(IEnumerable<T> source, int position, IFunc<Exception, IEnumerable<T>, T> fallback) : base(() =>
            {
                T result;
                try
                {
                    if (position < 0)
                    {
                        throw new InvalidOperationException(
                            new Formatted(
                                "The position must be non-negative but is {0}",
                                position
                            ).AsString()
                        );
                    }

                    var enumerator = source.GetEnumerator();
                    bool moved = false;
                    for(var current = 0;current<=position;current++)
                    {
                        moved = enumerator.MoveNext();
                        if(current == 0 && !moved)
                            throw new InvalidOperationException($"Enumerable is empty.");
                        else if (!moved)
                            throw new InvalidOperationException($"Cannot get item {position + 1} - The enumerable has only {current} items.");
                    }

                    result = enumerator.Current;
                }
                catch (Exception ex)
                {
                    result = fallback.Invoke(ex, source);
                }
                return result;
            }
        )
        { }
    }

    public sealed class ItemAt
    {
        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with given Exception thrwon on fallback
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public static IScalar<T> _<T>(IEnumerable<T> source, Exception ex)
            => new ItemAt<T>(source, ex);

        /// <summary>
        /// Element at position in <see cref="IEnumerable{T}"/> with given Exception thrown on fallback
        /// </summary>
        /// <param name="source"></param>
        /// <param name="position"></param>
        /// <param name="ex"></param>
        public static IScalar<T> _<T>(IEnumerable<T> source, int position, Exception ex)
            => new ItemAt<T>(source, position, ex);

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        public static IScalar<T> _<T>(IEnumerable<T> source)
            => new ItemAt<T>(source);

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public static IScalar<T> _<T>(IEnumerable<T> source, T fallback)
            => new ItemAt<T>(source, fallback);

        /// <summary>
        /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="position">position</param>
        /// <param name="fallback">fallback func</param>
        public static IScalar<T> _<T>(IEnumerable<T> source, int position, T fallback)
            => new ItemAt<T>(source, position, fallback);

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">soruce enum</param>
        /// <param name="fallback">fallback value</param>
        public static IScalar<T> _<T>(IEnumerable<T> source, IFunc<Exception, IEnumerable<T>, T> fallback)
            => new ItemAt<T>(source, fallback);

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">soruce enum</param>
        /// <param name="fallback">fallback value</param>
        public static IScalar<T> _<T>(IEnumerable<T> source, Func<IEnumerable<T>, T> fallback)
            => new ItemAt<T>(source, fallback);

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">soruce enum</param>
        /// <param name="fallback">fallback value</param>
        public static IScalar<T> _<T>(IEnumerable<T> source, IFunc<IEnumerable<T>, T> fallback)
            => new ItemAt<T>(source, fallback);

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="position">position of item</param>
        public static IScalar<T> _<T>(IEnumerable<T> source, int position)
            => new ItemAt<T>(source, position);

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="position">position of item</param>
        /// <param name="fallback">fallback func</param>
        public static IScalar<T> _<T>(IEnumerable<T> source, int position, IFunc<IEnumerable<T>, T> fallback)
            => new ItemAt<T>(source, position, fallback);

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="position">position of item</param>
        /// <param name="fallback">fallback func</param>
        public static IScalar<T> _<T>(IEnumerable<T> source, int position, Func<IEnumerable<T>, T> fallback)
            => new ItemAt<T>(source, position, fallback);

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="position">position of item</param>
        /// <param name="fallback">fallback func</param>
        public static IScalar<T> _<T>(IEnumerable<T> source, int position, Func<Exception, IEnumerable<T>, T> fallback)
            => new ItemAt<T>(source, position, fallback);

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="position">position of item</param>
        /// <param name="fallback">fallback func</param>
        public static IScalar<T> _<T>(IEnumerable<T> source, int position, IFunc<Exception, IEnumerable<T>, T> fallback)
            => new ItemAt<T>(source, position, fallback);
    }
}
