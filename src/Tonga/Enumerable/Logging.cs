

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Enumerable that logs object T when it is iterated.
    /// T is logged right after the underlying enumerator is moved.
    /// </summary>
    public sealed class Logging<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> origin;
        private readonly Action<T> log;

        /// <summary>
        /// Enumerable that logs object T to debug console when it is iterated.
        /// T is logged right after the underlying enumerator is moved.
        /// </summary>
        public Logging(IEnumerable<T> origin) : this(origin, (item) => Debug.WriteLine(item.ToString()))
        { }

        /// <summary>
        /// Enumerable that logs object T when it is iterated.
        /// T is logged right after the underlying enumerator is moved.
        /// </summary>
        public Logging(IEnumerable<T> origin, Action<T> log)
        {
            this.origin = origin;
            this.log = log;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in this.origin)
            {
                this.log.Invoke(item);
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    /// <summary>
    /// Enumerable that logs object T when it is iterated.
    /// T is logged right after the underlying enumerator is moved.
    /// </summary>
    public static class Logging
    {
        /// <summary>
        /// Enumerable that logs object T to debug console when it is iterated.
        /// T is logged right after the underlying enumerator is moved.
        /// </summary>
        public static IEnumerable<T> _<T>(IEnumerable<T> origin) => new Logging<T>(origin);

        /// <summary>
        /// Enumerable that logs object T when it is iterated.
        /// T is logged right after the underlying enumerator is moved.
        /// </summary>
        public static IEnumerable<T> _<T>(IEnumerable<T> origin, Action<T> log) => new Logging<T>(origin, log);
    }
}
