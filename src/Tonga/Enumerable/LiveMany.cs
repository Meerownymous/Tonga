

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tonga.Scalar;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> out of other objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class LiveMany<T> : IEnumerable<T>
    {
        private readonly IScalar<IEnumerator<T>> origin;

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of an array.
        /// </summary>
        /// <param name="items"></param>
        public LiveMany(params T[] items) : this(
            () => new Params<T>(items).GetEnumerator()
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="fnc">function which retrieves enumerator</param>
        public LiveMany(Func<IEnumerator<T>> fnc) : this(new Live<IEnumerator<T>>(fnc))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="fnc">function which retrieves enumerator</param>
        public LiveMany(Func<IEnumerable<T>> fnc) : this(new Live<IEnumerator<T>>(() => fnc.Invoke().GetEnumerator()))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> encapsulated in a <see cref="IScalar{T}"/>"/>.
        /// </summary>
        /// <param name="origin">scalar to return the IEnumerator</param>
        private LiveMany(IScalar<IEnumerator<T>> origin)
        {
            this.origin = origin;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var enumerator = this.origin.Value();
            while(enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Makes enumerable from an enumerator
        /// </summary>
        /// <typeparam name="X"></typeparam>
        private class LiveEnumeratorAsEnumerable<X> : IEnumerable<X>
        {
            private readonly IEnumerator<X> origin;

            public LiveEnumeratorAsEnumerable(IEnumerator<X> enumerator)
            {
                origin = enumerator;
            }

            public IEnumerator<X> GetEnumerator()
            {
                return origin;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }

    public static class LiveMany
    {
        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of an array.
        /// </summary>
        /// <param name="items"></param>
        public static IEnumerable<T> New<T>(params T[] items) => new LiveMany<T>(items);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="fnc">function which retrieves enumerator</param>
        public static IEnumerable<T> New<T>(Func<IEnumerator<T>> fnc) => new LiveMany<T>(fnc);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="fnc">function which retrieves enumerator</param>
        public static IEnumerable<T> New<T>(Func<IEnumerable<T>> fnc) => new LiveMany<T>(fnc);
    }
}
