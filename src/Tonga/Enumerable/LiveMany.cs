

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
    public sealed class Transit<T> : IEnumerable<T>
    {
        private readonly Func<IEnumerator<T>> origin;

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of an array.
        /// </summary>
        /// <param name="items"></param>
        public Transit(params T[] items) : this(
            () => new Params<T>(items).GetEnumerator()
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="fnc">function which retrieves enumerator</param>
        public Transit(IEnumerator<T> origin) : this(() => origin)
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="fnc">function which retrieves enumerator</param>
        public Transit(Func<IEnumerator<T>> origin)
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

    public static class Transit
    {
        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="fnc">function which retrieves enumerator</param>
        public static IEnumerable<T> Of<T>(IEnumerator<T> fnc) => new Transit<T>(fnc);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="fnc">function which retrieves enumerator</param>
        public static IEnumerable<T> Of<T>(Func<IEnumerator<T>> fnc) => new Transit<T>(fnc);
    }
}
