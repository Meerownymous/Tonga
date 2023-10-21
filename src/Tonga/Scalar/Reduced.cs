

using System;
using System.Collections.Generic;
using Tonga.Scalar;

namespace Tonga.Scalar
{
    /// <summary>
    /// <see cref="IEnumerable{T}"/> whose items are reduced to one item using the given function.
    /// </summary>
    /// <typeparam name="T">type of elements in a list to reduce</typeparam>
    public sealed class Reduced<T> : ScalarEnvelope<T>
    {
        /// <summary>
        /// <see cref="IEnumerable{Element}"/> whose items are folded to one item using the given function.
        /// </summary>
        /// <param name="elements">enumerable to reduce</param>
        /// <param name="fnc">reducing function</param>
        public Reduced(IEnumerable<T> elements, IBiFunc<T, T, T> fnc) : this(
            elements,
            fnc.Invoke
        )
        { }

        /// <summary>
        /// <see cref="IEnumerable{Element}"/> whose items are reduced to one item using the given function.
        /// </summary>
        /// <param name="elements">enumerable to reduce</param>
        /// <param name="fnc">reducing function</param>
        public Reduced(IEnumerable<T> elements, Func<T, T, T> fnc) : base(() =>
            {
                var enm = elements.GetEnumerator();

                if (!enm.MoveNext()) throw new ArgumentException($"Cannot reduce, at least one element is needed but the enumerable is empty.");
                T result = enm.Current;
                while (enm.MoveNext())
                {
                    result = fnc.Invoke(result, enm.Current);
                }
                return result;
            }
        )
        { }
    }

    /// <summary>
    /// <see cref="IEnumerable{T}"/> whose items are reduced to one item using the given function.
    /// </summary>
    public static class Reduced
    {
        /// <summary>
        /// <see cref="IEnumerable{Element}"/> whose items are folded to one item using the given function.
        /// </summary>
        /// <param name="elements">enumerable to reduce</param>
        /// <param name="fnc">reducing function</param>
        public static ScalarEnvelope<T> _<T>(IEnumerable<T> elements, IBiFunc<T, T, T> fnc) => new Reduced<T>(elements, fnc);

        /// <summary>
        /// <see cref="IEnumerable{Element}"/> whose items are reduced to one item using the given function.
        /// </summary>
        /// <param name="elements">enumerable to reduce</param>
        /// <param name="fnc">reducing function</param>
        public static ScalarEnvelope<T> _<T>(IEnumerable<T> elements, Func<T, T, T> fnc) => new Reduced<T>(elements, fnc);
    }
}
