using System;
using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Func;

namespace Tonga.Scalar
{
    /// <summary> Logical and. Returns true if all contents return true. </summary>
    /// <typeparam name="In"></typeparam>
    public sealed class And<In> : ScalarEnvelope<Boolean>
    {
        /// <summary> Logical and. Returns true if all calls to <see cref="Func{In, Out}"/> were true. </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public And(Func<In, bool> func, params In[] src) : this(new FuncOf<In, bool>(func), Enumerable.EnumerableOf.Pipe(src))
        { }

        /// <summary> Logical and. Returns true if all calls to <see cref="Func{In, Out}"/> were true. </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public And(Func<In, bool> func, IEnumerable<In> src) : this(new FuncOf<In, bool>(func), src)
        { }

        /// <summary> Logical and. Returns true if all calls to <see cref="IFunc{In, Out}"/> were true. </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public And(IFunc<In, Boolean> func, params In[] src) : this(func, Enumerable.EnumerableOf.Pipe(src))
        { }

        /// <summary> ctor </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public And(IFunc<In, Boolean> func, IEnumerable<In> src) :
            this(
                Mapped.Pipe(
                    new FuncOf<In, IScalar<Boolean>>((item) =>
                        new Live<Boolean>(func.Invoke(item))),
                    src
                )
            )
        { }

        /// <summary> True if all functions return true with given input value </summary>
        /// <param name="value"> Input value wich will executed by all given functions </param>
        /// <param name="functions"> Functions wich will executed with given input value </param>
        public And(In value, params Func<In, bool>[] functions)
            : this(tValue => new And(new Mapped<Func<In, bool>, bool>(tFunc => tFunc.Invoke(tValue), functions)).Value(), value)
        { }

        /// <summary></summary>
        /// <param name="src"></param>
        private And(IEnumerable<IScalar<bool>> src)
            : base(() =>
            {
                Boolean result = true;
                foreach (IScalar<Boolean> item in src)
                {
                    if (!item.Value())
                    {
                        result = false;
                        break;
                    }
                }
                return result;
            })
        { }
    }

    /// <summary> Logical and. Returns true if all contents return true. </summary>
    public sealed class And : ScalarEnvelope<bool>
    {
        /// <summary> Logical and. Returns true if all calls to <see cref="Func{In, Out}"/> were true. </summary>
        /// <param name="funcs"> the conditions to apply </param>
        public And(params Func<bool>[] funcs) : this(Enumerable.EnumerableOf.Pipe(funcs))
        { }

        /// <summary> Logical and. Returns true if all calls to <see cref="Func{Out}"/> were true. </summary>
        /// <param name="funcs"> the conditions to apply </param>
        public And(IEnumerable<Func<bool>> funcs) : this(
            new Mapped<Func<bool>, IScalar<bool>>(
                func => new Live<bool>(func),
                funcs
            )
        )
        { }

        /// <summary> ctor </summary>
        /// <param name="src"> list of items </param>
        public And(params IScalar<Boolean>[] src) : this(
            Enumerable.EnumerableOf.Pipe(src))
        { }

        /// <summary> ctor </summary>
        /// <param name="src"> list of items </param>
        public And(params bool[] src) : this(
            Mapped.Pipe(
                tBool => new Live<bool>(tBool),
                src
            )
        )
        { }

        /// <summary> ctor </summary>
        /// <param name="src"> list of items </param>
        public And(IEnumerable<bool> src) : this(
            Mapped.Pipe(
                tBool => new Live<bool>(tBool),
                src
            )
        )
        { }

        /// <summary> ctor </summary>
        /// <param name="src"> list of items </param>
        public And(IEnumerable<IScalar<bool>> src)
            : base(() =>
            {
                Boolean result = true;
                foreach (IScalar<Boolean> item in src)
                {
                    if (!item.Value())
                    {
                        result = false;
                        break;
                    }
                }
                return result;
            })
        { }

        /// <summary> Logical and. Returns true if all calls to <see cref="Func{In, Out}"/> were true. </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public static IScalar<bool> New<In>(Func<In, bool> func, params In[] src)
            => new And<In>(func, src);

        /// <summary> Logical and. Returns true if all calls to <see cref="Func{In, Out}"/> were true. </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public static IScalar<bool> New<In>(Func<In, bool> func, IEnumerable<In> src)
            => new And<In>(func, src);

        /// <summary> Logical and. Returns true if all calls to <see cref="IFunc{In, Out}"/> were true. </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public static IScalar<bool> New<In>(IFunc<In, Boolean> func, params In[] src)
            => new And<In>(func, src);

        /// <summary> ctor </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public static IScalar<bool> New<In>(IFunc<In, Boolean> func, IEnumerable<In> src)
            => new And<In>(func, src);

        /// <summary> True if all functions return true with given input value </summary>
        /// <param name="value"> Input value wich will executed by all given functions </param>
        /// <param name="functions"> Functions wich will executed with given input value </param>
        public static IScalar<bool> New<In>(In value, params Func<In, bool>[] functions)
            => new And<In>(value, functions);
    }
}
