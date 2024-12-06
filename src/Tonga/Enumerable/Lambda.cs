using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Enumerable which executes a given lambda function when advancing.
    /// </summary>
    public sealed class Lambda<T>(Action<T> lambda, IEnumerable<T> origin) : IEnumerable<T>
    {
        /// <summary>
        /// Enumerable which executes a given lambda function when advancing.
        /// </summary>
        public Lambda(Action lambda, IEnumerable<T> origin) : this(_ => lambda(), origin)
        { }

        public IEnumerator<T> GetEnumerator()
        {
            foreach(var item in origin)
            {
                lambda(item);
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    /// <summary>
    /// Enumerable which executes a given lambda function when advancing.
    /// </summary>
    public static class Lambda
    {
        /// <summary>
        /// Enumerable which executes a given lambda function when advancing.
        /// </summary>
        public static Lambda<T> _<T>(Action lambda, IEnumerable<T> origin) =>
            new(lambda, origin);

        /// <summary>
        /// Enumerable which executes a given lambda function when advancing.
        /// </summary>
        public static Lambda<T> _<T>(Action<T> lambda, IEnumerable<T> origin) =>
            new(lambda, origin);
    }

    public static class LambdaSmarts
    {
        /// <summary>
        /// Enumerable which executes a given lambda function when advancing.
        /// </summary>
        public static IEnumerable<T> Lambda<T>(this IEnumerable<T> origin, Action lambda) =>
            new Lambda<T>(lambda, origin);

        /// <summary>
        /// Enumerable which executes a given lambda function when advancing.
        /// </summary>
        public static IEnumerable<T> Lambda<T>(this IEnumerable<T> origin, Action<T> lambda) =>
            new Lambda<T>(lambda, origin);
    }
}

