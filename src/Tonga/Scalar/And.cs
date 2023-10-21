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
        /// <param name="check"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public And(Func<In, bool> check, params In[] src) : this(new FuncOf<In, bool>(check), AsEnumerable._(src))
        { }

        /// <summary> Logical and. Returns true if all calls to <see cref="Func{In, Out}"/> were true. </summary>
        /// <param name="check"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public And(Func<In, bool> check, IEnumerable<In> src) : this(new FuncOf<In, bool>(check), src)
        { }

        /// <summary> Logical and. Returns true if all calls to <see cref="IFunc{In, Out}"/> were true. </summary>
        /// <param name="check"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public And(IFunc<In, Boolean> check, params In[] src) : this(check, AsEnumerable._(src))
        { }

        /// <summary> ctor </summary>
        /// <param name="check"> the condition to apply </param>
        /// <param name="items"> list of items </param>
        public And(IFunc<In, Boolean> check, IEnumerable<In> items) :
            this(
                Mapped._(
                    new FuncOf<In, IScalar<Boolean>>((item) =>
                        AsScalar._(check.Invoke(item))
                    ),
                    items
                )
            )
        { }

        /// <summary> True if all functions return true with given input value </summary>
        /// <param name="value"> Input value wich will executed by all given functions </param>
        /// <param name="functions"> Functions wich will executed with given input value </param>
        public And(In value, params Func<In, bool>[] functions) : this(
            tValue =>
            new And(
                Mapped._(
                    tFunc => tFunc.Invoke(tValue),
                    functions
                )
            ).Value(),
            value
        )
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
        public And(params Func<bool>[] funcs) : this(AsEnumerable._(funcs))
        { }

        /// <summary> Logical and. Returns true if all calls to <see cref="Func{Out}"/> were true. </summary>
        /// <param name="funcs"> the conditions to apply </param>
        public And(IEnumerable<Func<bool>> funcs) : this(
            Mapped._(
                func => AsScalar._(func),
                funcs
            )
        )
        { }

        /// <summary> ctor </summary>
        /// <param name="src"> list of items </param>
        public And(params IScalar<Boolean>[] src) : this(
           AsEnumerable._(src)
        )
        { }

        /// <summary> ctor </summary>
        /// <param name="src"> list of items </param>
        public And(params bool[] src) : this(
            Mapped._(
                tBool => AsScalar._(tBool),
                src
            )
        )
        { }

        /// <summary> ctor </summary>
        /// <param name="src"> list of items </param>
        public And(IEnumerable<bool> src) : this(
            Mapped._(
                tBool => AsScalar._(tBool),
                src
            )
        )
        { }

        /// <summary> ctor </summary>
        /// <param name="src"> list of items </param>
        public And(IEnumerable<IScalar<bool>> src) : base(() =>
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
        public static IScalar<bool> _<In>(Func<In, bool> func, params In[] src)
            => new And<In>(func, src);

        /// <summary> Logical and. Returns true if all calls to <see cref="Func{In, Out}"/> were true. </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public static IScalar<bool> _<In>(Func<In, bool> func, IEnumerable<In> src)
            => new And<In>(func, src);

        /// <summary> Logical and. Returns true if all calls to <see cref="IFunc{In, Out}"/> were true. </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public static IScalar<bool> _<In>(IFunc<In, Boolean> func, params In[] src)
            => new And<In>(func, src);

        /// <summary> ctor </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public static IScalar<bool> _<In>(IFunc<In, Boolean> func, IEnumerable<In> src)
            => new And<In>(func, src);

        /// <summary> True if all functions return true with given input value </summary>
        /// <param name="value"> Input value wich will executed by all given functions </param>
        /// <param name="functions"> Functions wich will executed with given input value </param>
        public static IScalar<bool> _<In>(In value, params Func<In, bool>[] functions)
            => new And<In>(value, functions);
    }
}
