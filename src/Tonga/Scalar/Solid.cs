

using System;

namespace Tonga.Scalar
{
    /// <summary>
    /// A Scalar that is both threadsafe and sticky.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Solid<T> : IScalar<T>
    {
        private readonly IScalar<T> src;
        private readonly object lck;
        private volatile object cache;

        /// <summary>
        /// A <see cref="ScalarEnvelope{T}"/> that is threadsafe.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        public Solid(Func<T> src) : this(src, src)
        { }

        /// <summary>
        /// A <see cref="ScalarEnvelope{T}"/> that is threadsafe and sticky.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        /// <param name="lck">the object to lock</param>
        public Solid(Func<T> src, object lck) : this(AsScalar._(src), lck)
        { }

        /// <summary>
        /// A <see cref="ScalarEnvelope{T}"/> that is threadsafe and sticky.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        public Solid(IScalar<T> src) : this(src, src)
        { }

        /// <summary>
        /// A <see cref="ScalarEnvelope{T}"/> that is threadsafe and sticky.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        /// <param name="lck">object to lock while using scalar</param>
        public Solid(IScalar<T> src, Object lck)
        {
            this.src = src;
            this.lck = lck;
        }

        public T Value()
        {
            if (this.cache == null)
            {
                lock (this.lck)
                {
                    if (this.cache == null)
                    {
                        this.cache = this.src.Value();
                    }
                }
            }
            return (T)this.cache;
        }
    }

    public static class Solid
    {
        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        public static IScalar<T> New<T>(Func<T> src)
            => new Solid<T>(src);

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe and sticky.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        /// <param name="lck">the object to lock</param>
        public static IScalar<T> New<T>(Func<T> src, object lck)
            => new Solid<T>(src, lck);

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe and sticky.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        public static IScalar<T> New<T>(IScalar<T> src)
            => new Solid<T>(src);

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe and sticky.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        /// <param name="lck">object to lock while using scalar</param>
        public static IScalar<T> New<T>(IScalar<T> src, Object lck)
            => new Solid<T>(src, lck);
    }
}
