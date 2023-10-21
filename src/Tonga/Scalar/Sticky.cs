using System;
namespace Tonga.Scalar
{
    /// <summary>
    /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Sticky<T> : IScalar<T>
    {
        private readonly Lazy<T> origin;

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache as long the reload condition is false.
        /// </summary>
        /// <param name="src">scalar to cache result from</param>
        /// <param name="shouldReload">reload condition func</param>
        public Sticky(IScalar<T> src) : this(src.Value)
        { }

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache as long the reload condition is false.
        /// </summary>
        /// <param name="src">scalar to cache result from</param>
        /// <param name="shouldReload">reload condition func</param>
        public Sticky(Func<T> src)
        {
            this.origin = new Lazy<T>(src);
        }

        /// <summary>
        /// Get the value.
        /// </summary>
        /// <returns>the value</returns>
        public T Value()
        {
            return this.origin.Value;
        }
    }

    public static class Sticky
    {
        public static Sticky<T> _<T>(IScalar<T> src) => new Sticky<T>(src);

        public static Sticky<T> _<T>(Func<T> src) => new Sticky<T>(src);
    }
}

