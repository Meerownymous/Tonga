

using System.Collections.Generic;
using System.Threading.Tasks;
using Tonga.Enumerable;
using Tonga.Func;

namespace Tonga.Scalar
{
    /// <summary>
    /// Logical conjunction, in multiple threads. Returns true if all contents return true.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ParallelAnd<T> : IScalar<bool>
    {
        private IEnumerable<IScalar<bool>> iterable;

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="src"></param>
        public ParallelAnd(params IScalar<bool>[] src) : this(
            Enumerable.EnumerableOf.Pipe(src)
        )
        { }

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="src"></param>
        public ParallelAnd(IAction<T> proc, params T[] src) : this(
            new FuncOf<T, bool>(proc, true), src
        )
        { }

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="src"></param>
        public ParallelAnd(IFunc<T, bool> func, params T[] src) : this(
            func, Enumerable.EnumerableOf.Pipe(src)
        )
        { }

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="src"></param>
        public ParallelAnd(IAction<T> proc, IEnumerable<T> src) : this(
            new FuncOf<T, bool>(proc, true), src
        )
        { }

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="src"></param>
        public ParallelAnd(IFunc<T, bool> func, IEnumerable<T> src) : this(
            Mapped.New(
                i => new ScalarOf<bool>(func.Invoke(i)),
                src,
                live: false
            )
        )
        { }

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="src"></param>
        public ParallelAnd(IEnumerable<IScalar<bool>> src)
        {
            this.iterable = src;
        }

        public bool Value()
        {
            var result = true;

            Parallel.ForEach(this.iterable, test =>
            {
                if (!test.Value())
                {
                    result = false;
                }
            });

            return result;
        }
    }

    public static class ParallelAnd
    {
        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="act"></param>
        /// <param name="src"></param>
        public static IScalar<bool> New<T>(IAction<T> act, params T[] src)
            => new ParallelAnd<T>(act, src);

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="src"></param>
        public static IScalar<bool> New<T>(IFunc<T, bool> func, params T[] src)
            => new ParallelAnd<T>(func, src);

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="src"></param>
        public static IScalar<bool> New<T>(IAction<T> proc, IEnumerable<T> src)
            => new ParallelAnd<T>(proc, src);

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="src"></param>
        public static IScalar<bool> New<T>(IFunc<T, bool> func, IEnumerable<T> src)
            => new ParallelAnd<T>(func, src);

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="src"></param>
        public static IScalar<bool> New<T>(IEnumerable<IScalar<bool>> src)
            => new ParallelAnd<T>(src);
    }
}
