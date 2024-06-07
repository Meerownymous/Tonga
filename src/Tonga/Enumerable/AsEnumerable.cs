using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> out of other objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class AsEnumerable<T> : IEnumerable<T>
    {
        private readonly Func<IEnumerator<T>> origin;

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of an array.
        /// </summary>
        /// <param name="items"></param>
        public AsEnumerable(params T[] items) : this(
            () => new Enumerator.Array<T>(items)
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="origin">function which retrieves enumerator</param>
        public AsEnumerable(IEnumerable<T> origin) : this(() => origin.GetEnumerator())
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="origin">function which retrieves enumerator</param>
        public AsEnumerable(Func<IEnumerable<T>> origin) : this(
            () => origin().GetEnumerator()
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="origin">function which retrieves enumerator</param>
        public AsEnumerable(IEnumerator<T> origin) : this(() => origin)
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="origin">function which retrieves enumerator</param>
        public AsEnumerable(Func<IEnumerator<T>> origin)
        {
            this.origin = origin;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var enumerator = this.origin();
            while(enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public static class AsEnumerable
    {
        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="items">enumerated content</param>
        public static IEnumerable<T> _<T>(params T[] items) => new AsEnumerable<T>(items);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> from a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="fnc">function which retrieves enumerator</param>
        public static IEnumerable<T> _<T>(IEnumerable<T> fnc) => new AsEnumerable<T>(fnc);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> form a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="fnc">function which retrieves enumerator</param>
        public static IEnumerable<T> _<T>(Func<IEnumerable<T>> fnc) => new AsEnumerable<T>(fnc);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="fnc">function which retrieves enumerator</param>
        public static IEnumerable<T> _<T>(IEnumerator<T> fnc) => new AsEnumerable<T>(fnc);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="fnc">function which retrieves enumerator</param>
        public static IEnumerable<T> _<T>(Func<IEnumerator<T>> fnc) => new AsEnumerable<T>(fnc);
    }
}
