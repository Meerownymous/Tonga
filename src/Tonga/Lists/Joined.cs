

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
            Enumerable.Joined._(
                Single._(origin),
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
            Enumerable.Joined._(
                Single._(origin),
                src
            )
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">The lists to join together</param>
        public Joined(params IList<T>[] src) : this(
            AsEnumerable._(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">The lists to join together</param>
        public Joined(IEnumerable<IList<T>> src) : base(
            AsList._(
                Enumerable.Joined._<T>(src)
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
        public static IList<T> _<T>(IList<T> origin, params IList<T>[] src)
            => new Joined<T>(origin, src);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">The lists to join together</param>
        public static IList<T> _<T>(params IList<T>[] src)
            => new Joined<T>(src);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">The lists to join together</param>
        public static IList<T> _<T>(IEnumerable<IList<T>> src)
            => new Joined<T>(src);
    }
}
