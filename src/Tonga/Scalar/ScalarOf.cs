

using System;

namespace Tonga.Scalar
{
    /// <summary>
    /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class AsScalar<T> : IScalar<T>
    {
        private readonly Func<T> origin;

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
        /// </summary>
        /// <param name="src">func to cache result from</param>
        public AsScalar(T src) : this(() => src)
        { }

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache as long the reload condition is false.
        /// </summary>
        /// <param name="srcFunc">func to cache result from</param>
        /// <param name="shouldReload">reload condition func</param>
        public AsScalar(IFunc<T> srcFunc) : this(() => srcFunc.Invoke())
        { }


        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache as long the reload condition is false.
        /// </summary>
        /// <param name="src">scalar to cache result from</param>
        /// <param name="shouldReload">reload condition func</param>
        public AsScalar(Func<T> src)
        {
            this.origin = src;
        }

        /// <summary>
        /// Get the value.
        /// </summary>
        /// <returns>the value</returns>
        public T Value()
        {
            return this.origin();
        }
    }

    public static class AsScalar
    {
        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
        /// </summary>
        /// <param name="src">func to cache result from</param>
        public static IScalar<T> _<T>(Func<T> src) => new AsScalar<T>(src);

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
        /// </summary>
        /// <param name="src">func to cache result from</param>
        public static IScalar<T> _<T>(T src) => new AsScalar<T>(src);

        /// <summary>
        /// A s<see cref="ScalarEnvelope{T}"/> that will return the same value from a cache as long the reload condition is false.
        /// </summary>
        /// <param name="srcFunc">func to cache result from</param>
        /// <param name="shouldReload">reload condition func</param>
        public static IScalar<T> _<T>(IFunc<T> srcFunc) => new AsScalar<T>(srcFunc);
    }
}
