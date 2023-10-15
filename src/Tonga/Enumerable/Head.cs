using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> limited to an item maximum.
    /// </summary>
    /// <typeparam name="T">type of elements</typeparam>
    public sealed class Head<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> enumerable;
        private readonly IScalar<int> limit;
        private readonly Ternary<T> result;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="enumerable">enumerable to limit</param>
        public Head(IEnumerable<T> enumerable, bool live = false) : this(enumerable, 1, live)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="enumerable">enumerable to limit</param>
        /// <param name="limit">maximum item count</param>
        public Head(IEnumerable<T> enumerable, int limit, bool live = false) : this(
            enumerable,
            new Scalar.Live<int>(limit),
            live
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> limited to an item maximum.
        /// </summary>
        /// <param name="enumerable">enumerable to limit</param>
        /// <param name="limit">maximum item count</param>
        public Head(IEnumerable<T> enumerable, IScalar<int> limit, bool live = false)
        {
            this.enumerable = enumerable;
            this.limit = limit;
            this.result =
                Ternary.Pipe(
                    EnumerableOf.Pipe(Produced),
                    Sticky.New(Produced),
                    live
                );
        }

        public IEnumerator<T> GetEnumerator() => this.result.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private IEnumerator<T> Produced()
        {
            var limit = this.limit.Value();
            var taken = 0;
            var enumerator = this.enumerable.GetEnumerator();
            while (enumerator.MoveNext() && taken < limit)
            {
                taken++;
                yield return enumerator.Current;
            }
        }
    }

    /// <summary>
    /// A <see cref="IEnumerable{T}"/> limited to an item maximum.
    /// </summary>
    public static class Head
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="enumerable">enumerable to limit</param>
        public static IEnumerable<T> New<T>(IEnumerable<T> enumerable) => new Head<T>(enumerable);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="enumerable">enumerable to limit</param>
        /// <param name="limit">maximum item count</param>
        public static IEnumerable<T> New<T>(IEnumerable<T> enumerable, int limit) => new Head<T>(enumerable, limit);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> limited to an item maximum.
        /// </summary>
        /// <param name="enumerable">enumerable to limit</param>
        /// <param name="limit">maximum item count</param>
        public static IEnumerable<T> New<T>(IEnumerable<T> enumerable, IScalar<int> limit) => new Head<T>(enumerable, limit);
    }
}