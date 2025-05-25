using System;
namespace Tonga.Scalar
{
    /// <summary>
    /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Sticky<T>(Func<T> origin) : IScalar<T>
    {
        private readonly Lazy<T> cache = new (origin);

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache.
        /// </summary>
        public Sticky(IScalar<T> src) : this(src.Value)
        { }

        /// <summary>
        /// The value.
        /// </summary>
        public T Value() => this.cache.Value;
    }

    public static partial class ScalarSmarts
    {
        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache.
        /// </summary>
        public static IScalar<T> AsSticky<T>(this IScalar<T> src) => new Sticky<T>(src);

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache.
        /// </summary>
        public static IScalar<T> AsSticky<T>(this Func<T> src) => new Sticky<T>(src);
    }
}

