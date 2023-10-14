

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A reversed <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="X">type of items in enumerable</typeparam>
    public sealed class Reversed<X> : IEnumerable<X>
    {
        private readonly IEnumerable<X> src;
        private readonly Ternary<X> result;

        /// <summary>
        /// A reversed <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="src">enumerable to reverse</param>
        public Reversed(IEnumerable<X> src, bool live = false)
        {
            this.src = src;
            this.result =
                Ternary.New(
                    LiveMany.New(Produced),
                    Sticky.By(Produced),
                    live
                );
        }

        public IEnumerator<X> GetEnumerator() => this.result.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private IEnumerable<X> Produced()
        {
            foreach(var item in this.src.Reverse())
            {
                yield return item;
            }
        }
    }

    /// <summary>
    /// A reversed <see cref="IEnumerable{T}"/>
    /// </summary>
    public static class Reversed
    {
        /// <summary>
        /// A reversed <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="src">enumerable to reverse</param>
        public static IEnumerable<T> New<T>(IEnumerable<T> src) => new Reversed<T>(src);
    }
}
