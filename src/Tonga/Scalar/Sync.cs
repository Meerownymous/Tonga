

using System;

namespace Tonga.Scalar
{
    /// <summary>
    /// A <see cref="IScalar{T}"/> that is threadsafe.
    /// </summary>
    /// <typeparam name="T">type of value</typeparam>
    public sealed class Sync<T> : IScalar<T>
    {
        private readonly IScalar<T> src;
        private readonly Object lck;

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        public Sync(Func<T> src) : this(src, src)
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        /// <param name="lck">the object to lock</param>
        public Sync(Func<T> src, object lck) : this(AsScalar._(src), lck)
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        public Sync(IScalar<T> src) : this(src, src)
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        /// <param name="lck">object to lock while using scalar</param>
        public Sync(IScalar<T> src, Object lck)
        {
            this.src = src;
            this.lck = lck;
        }

        /// <summary>
        /// Get the value.
        /// </summary>
        /// <returns>the value</returns>
        public T Value()
        {
            lock (this.lck)
            {
                return this.src.Value();
            }
        }
    }

    public static class Sync
    {
        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        public static IScalar<T> New<T>(System.Func<T> src)
            => new Sync<T>(src);

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        /// <param name="lck">the object to lock</param>
        public static IScalar<T> New<T>(System.Func<T> src, object lck)
            => new Sync<T>(src, lck);

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        public static IScalar<T> New<T>(IScalar<T> src)
            => new Sync<T>(src);

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        /// <param name="lck">object to lock while using scalar</param>
        public static IScalar<T> New<T>(IScalar<T> src, Object lck)
            => new Sync<T>(src, lck);
    }
}
