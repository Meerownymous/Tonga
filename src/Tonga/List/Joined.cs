

using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.List
{
    /// <summary>
    /// Multiple lists joined together as one.
    /// </summary>
    /// <typeparam name="T">type of items in list</typeparam>
    public sealed class Joined<T> : ListEnvelope<T>
    {
        /// <summary>
        /// Multiple <see cref="IList{T}"/> joined together
        /// </summary>
        /// <param name="origin">a list to join</param>
        /// <param name="src">lists to join</param>
        public Joined(IList<T> origin, params IList<T>[] src) : this(
            Enumerable.Joined.Pipe(
                Single.Pipe(origin),
                src
            )
        )
        { }

        /// <summary>
        /// Multiple <see cref="IList{T}"/> joined together
        /// </summary>
        /// <param name="src">The lists to join together</param>
        /// <param name="origin">a list to join</param>
        public Joined(IList<T> origin, params T[] src) : this(
            Enumerable.Joined.Pipe(
                Single.Pipe(origin),
                src
            )
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">The lists to join together</param>
        public Joined(params IList<T>[] src) : this(
            EnumerableOf.Pipe(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">The lists to join together</param>
        public Joined(IEnumerable<IList<T>> src) : base(() =>
            ListOf.Pipe(
                Enumerable.Joined.Pipe<T>(src)
            )
        )
        { }
    }

    public static class Joined
    {
        /// <summary>
        /// Multiple <see cref="IList{T}"/> joined together
        /// </summary>
        /// <param name="origin">a list to join</param>
        /// <param name="src">lists to join</param>
        public static IList<T> Pipe<T>(IList<T> origin, params IList<T>[] src)
            => new Joined<T>(origin, src);

        /// <summary>
        /// Multiple <see cref="IList{T}"/> joined together
        /// </summary>
        /// <param name="src">The lists to join together</param>
        /// <param name="origin">a list to join</param>
        public static IList<T> Pipe<T>(IList<T> origin, params T[] src)
            => new Joined<T>(origin, src);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">The lists to join together</param>
        public static IList<T> Pipe<T>(params IList<T>[] src)
            => new Joined<T>(src);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">The lists to join together</param>
        public static IList<T> Pipe<T>(IEnumerable<IList<T>> src)
            => new Joined<T>(src);

        /// <summary>
        /// Multiple <see cref="IList{T}"/> joined together
        /// </summary>
        /// <param name="origin">a list to join</param>
        /// <param name="src">lists to join</param>
        public static IList<T> Sticky<T>(IList<T> origin, params IList<T>[] src)
            => List.Sticky.New(Pipe(origin, src));

        /// <summary>
        /// Multiple <see cref="IList{T}"/> joined together
        /// </summary>
        /// <param name="src">The lists to join together</param>
        /// <param name="origin">a list to join</param>
        public static IList<T> Sticky<T>(IList<T> origin, params T[] src)
            => List.Sticky.New(Pipe(origin, src));

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">The lists to join together</param>
        public static IList<T> Sticky<T>(params IList<T>[] src)
            => List.Sticky.New(Pipe(src));

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">The lists to join together</param>
        public static IList<T> Sticky<T>(IEnumerable<IList<T>> src)
            => List.Sticky.New(Pipe(src));
    }
}
