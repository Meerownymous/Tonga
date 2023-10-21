using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Enumerable which executes a given lambda function when advancing.
    /// </summary>
    public sealed class Lambda<T> : IEnumerable<T>
    {
        private readonly Action<T> lambda;
        private readonly IEnumerable<T> origin;

        /// <summary>
        /// Enumerable which executes a given lambda function when advancing.
        /// </summary>
        public Lambda(Action lambda, IEnumerable<T> origin) : this(item => lambda(), origin)
        { }

        /// <summary>
        /// Enumerable which executes a given lambda function when advancing.
        /// </summary>
        public Lambda(Action<T> lambda, IEnumerable<T> origin)
        {
            this.lambda = lambda;
            this.origin = origin;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach(var item in origin)
            {
                this.lambda.Invoke(item);
                yield return item;
            }
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    /// <summary>
    /// Enumerable which executes a given lambda function when advancing.
    /// </summary>
    public static class Lambda
    {
        /// <summary>
        /// Enumerable which executes a given lambda function when advancing.
        /// </summary>
        public static Lambda<T> From<T>(Action lambda, IEnumerable<T> origin) =>
            new Lambda<T>(lambda, origin);

        /// <summary>
        /// Enumerable which executes a given lambda function when advancing.
        /// </summary>
        public static Lambda<T> From<T>(Action<T> lambda, IEnumerable<T> origin) =>
            new Lambda<T>(lambda, origin);
    }
}

