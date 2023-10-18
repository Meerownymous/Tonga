

using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.List
{
    /// <summary>
    /// Multiple lists joined together as one.
    /// </summary>
    /// <typeparam name="T">type of items in list</typeparam>
    public sealed class Joined<T> : ListEnvelopeOriginal<T>
    {
        /// <summary>
        /// Multiple <see cref="IList{T}"/> joined together
        /// </summary>
        /// <param name="origin">a list to join</param>
        /// <param name="src">lists to join</param>
        public Joined(IList<T> origin, params IList<T>[] src) : this(
            new Enumerable.Joined<IList<T>>(
                new EnumerableOf<IList<T>>(origin), src
            )
        )
        { }

        /// <summary>
        /// Multiple <see cref="IList{T}"/> joined together
        /// </summary>
        /// <param name="src">The lists to join together</param>
        /// <param name="origin">a list to join</param>
        public Joined(IList<T> origin, params T[] src) : this(
            new Enumerable.Joined<IList<T>>(
                new EnumerableOf<IList<T>>(origin), src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">The lists to join together</param>
        public Joined(params IList<T>[] src) : this(new EnumerableOf<IList<T>>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">The lists to join together</param>
        public Joined(IEnumerable<IList<T>> src) : base(() =>
            {
                return
                    new ListOf<T>(
                        new Enumerable.Joined<T>(src)
                    );
            },
            false
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
        public static IList<T> New<T>(IList<T> origin, params IList<T>[] src)
            => new Joined<T>(origin, src);

        /// <summary>
        /// Multiple <see cref="IList{T}"/> joined together
        /// </summary>
        /// <param name="src">The lists to join together</param>
        /// <param name="origin">a list to join</param>
        public static IList<T> New<T>(IList<T> origin, params T[] src)
            => new Joined<T>(origin, src);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">The lists to join together</param>
        public static IList<T> New<T>(params IList<T>[] src)
            => new Joined<T>(src);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">The lists to join together</param>
        public static IList<T> New<T>(IEnumerable<IList<T>> src)
            => new Joined<T>(src);
    }
}
