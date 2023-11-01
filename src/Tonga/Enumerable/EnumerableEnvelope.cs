

using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Envelope for Enumerable of strings.
    /// It bundles the methods offered by IEnumerable and enables scalar based ctors.
    /// </summary>
    public abstract class EnumerableEnvelope : IEnumerable<string>
    {
        private readonly IEnumerable<string> items;

        /// <summary>
        /// Envelope for Enumerable.
        /// </summary>
        public EnumerableEnvelope(Func<IEnumerable<string>> origin) : this(() =>
            origin().GetEnumerator()
        )
        { }

        /// <summary>
        /// Envelope for Enumerable.
        /// </summary>
        public EnumerableEnvelope(Func<IEnumerator<string>> origin)
        {
            this.items = new EnumeratorAsEnumerable<string>(origin);
        }

        /// <summary>
        /// Enumerator for this envelope.
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<string> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        /// <summary>
        /// Enumerator for this envelope.
        /// </summary>
        /// <returns>The enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// Envelope for Enumerable.
    /// It bundles the methods offered by IEnumerable and enables scalar based ctors.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EnumerableEnvelope<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> content;

        /// <summary>
        /// Envelope for Enumerables.
        /// </summary>
        public EnumerableEnvelope(IEnumerable<T> origin)
        {
            this.content = origin;
        }

        /// <summary>
        /// Enumerator for this envelope.
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.content.GetEnumerator();
        }

        /// <summary>
        /// Enumerator for this envelope.
        /// </summary>
        /// <returns>The enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
