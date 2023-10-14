

using System;

namespace Tonga.Scalar
{
    /// <summary>
    /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class LazyOf<T> : IScalar<T>
    {
        private readonly Lazy<T> origin;

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache as long the reload condition is false.
        /// </summary>
        /// <param name="src">scalar to cache result from</param>
        /// <param name="shouldReload">reload condition func</param>
        public LazyOf(Func<T> src, Func<T, bool> shouldReload)
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

    /// <summary>
    /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ScalarOf<T> : IScalar<T>
    {
        private readonly IScalar<T> origin;
        private readonly Func<T, bool> shouldReload;
        private readonly T[] cache;
        private readonly bool[] filled; //this not-readonly flag is a compromise due to performance issues when using StickyFunc.

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
        /// </summary>
        /// <param name="src">func to cache result from</param>
        public ScalarOf(T src) : this(new Live<T>(src))
        { }

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
        /// </summary>
        /// <param name="src">func to cache result from</param>
        public ScalarOf(Func<T> src) : this(new Live<T>(src))
        { }

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
        /// </summary>
        /// <param name="src">scalar to cache result from</param>
        public ScalarOf(IScalar<T> src) : this(src, input => false)
        { }

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache as long the reload condition is false.
        /// </summary>
        /// <param name="srcFunc">func to cache result from</param>
        /// <param name="shouldReload">reload condition func</param>
        public ScalarOf(Func<T> srcFunc, Func<T, bool> shouldReload) : this(new Live<T>(srcFunc), shouldReload)
        { }

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache as long the reload condition is false.
        /// </summary>
        /// <param name="srcFunc">func to cache result from</param>
        /// <param name="shouldReload">reload condition func</param>
        public ScalarOf(IFunc<T> srcFunc, Func<T, bool> shouldReload) : this(new Live<T>(srcFunc), shouldReload)
        { }

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache as long the reload condition is false.
        /// </summary>
        /// <param name="src">scalar to cache result from</param>
        /// <param name="shouldReload">reload condition func</param>
        public ScalarOf(IScalar<T> src, Func<T, bool> shouldReload)
        {
            this.origin = src;
            this.shouldReload = shouldReload;
            this.cache = new T[1];
            this.filled = new bool[1];
        }

        /// <summary>
        /// Get the value.
        /// </summary>
        /// <returns>the value</returns>
        public T Value()
        {
            if (this.filled[0] != true)
            {
                this.cache.SetValue(this.origin.Value(), 0);
                this.filled[0] = true;
            }
            else if (this.shouldReload(this.cache[0]))
            {
                this.cache[0] = this.origin.Value();
            }
            return this.cache[0];
        }
    }

    public static class ScalarOf
    {
        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
        /// </summary>
        /// <param name="src">func to cache result from</param>
        public static IScalar<T> New<T>(T src) => new ScalarOf<T>(src);

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
        /// </summary>
        /// <param name="src">func to cache result from</param>
        public static IScalar<T> New<T>(Func<T> src) => new ScalarOf<T>(src);

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
        /// </summary>
        /// <param name="src">scalar to cache result from</param>
        public static IScalar<T> New<T>(IScalar<T> src) => new ScalarOf<T>(src);

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache as long the reload condition is false.
        /// </summary>
        /// <param name="srcFunc">func to cache result from</param>
        /// <param name="shouldReload">reload condition func</param>
        public static IScalar<T> New<T>(IFunc<T> srcFunc, Func<T, bool> shouldReload) => new ScalarOf<T>(srcFunc, shouldReload);

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache as long the reload condition is false.
        /// </summary>
        /// <param name="srcFunc">func to cache result from</param>
        /// <param name="shouldReload">reload condition func</param>
        public static IScalar<T> New<T>(Func<T> srcFunc, Func<T, bool> shouldReload) => new ScalarOf<T>(srcFunc, shouldReload);

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache as long the reload condition is false.
        /// </summary>
        /// <param name="src">scalar to cache result from</param>
        /// <param name="shouldReload">reload condition func</param>
        public static IScalar<T> New<T>(IScalar<T> src, Func<T, bool> shouldReload) => new ScalarOf<T>(src, shouldReload);
    }
}
