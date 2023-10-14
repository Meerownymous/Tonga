

using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Envelope for Enumerable of strings.
    /// It bundles the methods offered by IEnumerable and enables scalar based ctors.
    /// </summary>
    public abstract class ManyEnvelope : IEnumerable<string>
    {
        private readonly IEnumerable<string> items;

        /// <summary>
        /// Envelope for Enumerable.
        /// </summary>
        public ManyEnvelope(Func<IEnumerable<string>> origin) : this(() =>
            origin().GetEnumerator()
        )
        { }

        /// <summary>
        /// Envelope for Enumerable.
        /// </summary>
        public ManyEnvelope(Func<IEnumerator<string>> origin)
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
    public abstract class ManyEnvelope<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> content;

        /// <summary>
        /// Envelope for Enumerable.
        /// </summary>
        public ManyEnvelope(Func<IEnumerable<T>> origin) : this(() => origin().GetEnumerator())
        { }

        /// <summary>
        /// Envelope for Enumerables.
        /// </summary>
        public ManyEnvelope(Func<IEnumerator<T>> origin)
        {
            this.content = new EnumeratorAsEnumerable<T>(origin);
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
