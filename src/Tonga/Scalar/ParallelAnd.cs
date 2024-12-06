

using System.Collections.Generic;
using System.Threading.Tasks;
using Tonga.Enumerable;
using Tonga.Fact;
using Tonga.Func;

namespace Tonga.Scalar
{
    /// <summary>
    /// Logical conjunction, in multiple threads. Returns true if all contents return true.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ParallelAnd<T> : FactEnvelope
    {
        private IEnumerable<IFact> iterable;

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="src"></param>
        public ParallelAnd(params IFact[] src) : this(
            AsEnumerable._(src)
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
            func, AsEnumerable._(src)
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
            Mapped._(
                i => new AsFact(func.Invoke(i)),
                src
            )
        )
        { }

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="src"></param>
        public ParallelAnd(IEnumerable<IFact> src) : base(
            new AsFact(() =>
                {
                    var result = true;

                    Parallel.ForEach(src, test =>
                    {
                        if (test.IsFalse())
                        {
                            result = false;
                        }
                    });

                    return result;
                }

            )
        )
        { }
    }

    public static class ParallelAnd
    {
        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="act"></param>
        /// <param name="src"></param>
        public static IFact _<T>(IAction<T> act, params T[] src)
            => new ParallelAnd<T>(act, src);

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="src"></param>
        public static IFact _<T>(IFunc<T, bool> func, params T[] src)
            => new ParallelAnd<T>(func, src);

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="src"></param>
        public static IFact _<T>(IAction<T> proc, IEnumerable<T> src)
            => new ParallelAnd<T>(proc, src);

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="src"></param>
        public static IFact _<T>(IFunc<T, bool> func, IEnumerable<T> src)
            => new ParallelAnd<T>(func, src);

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="src"></param>
        public static IFact _(params IFact[] src)
            => new ParallelAnd<object>(src);

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="src"></param>
        public static IFact _(IEnumerable<IFact> src)
            => new ParallelAnd<object>(src);
    }
}
