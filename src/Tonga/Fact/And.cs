using System;
using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.Fact
{
    /// <summary> Logical and. Returns true if all contents return true. </summary>
    /// <typeparam name="In"></typeparam>
    public sealed class And<In> : FactEnvelope
    {
        /// <summary> Logical and. Returns true if all calls to <see cref="IFunc{In, Out}"/> were true. </summary>
        /// <param name="check"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public And(Func<In, Boolean> check, params In[] src) : this(check, AsEnumerable._(src))
        { }

        /// <summary> ctor </summary>
        /// <param name="check"> the condition to apply </param>
        /// <param name="items"> list of items </param>
        public And(Func<In, Boolean> check, IEnumerable<In> items) : this(
                Mapped._(
                    item => new AsFact(() => check.Invoke(item)),
                    items
                )
            )
        { }

        /// <summary> True if all functions return true with given input value </summary>
        /// <param name="value"> Input value wich will executed by all given functions </param>
        /// <param name="checks"> Functions wich will executed with given input value </param>
        public And(In value, params Func<In, bool>[] checks) : this(
            tValue =>
                new And(
                    Mapped._(
                        check => check.Invoke(tValue),
                        checks
                    )
                ).IsTrue(),
            value
        )
        { }

        /// <summary></summary>
        /// <param name="src"></param>
        private And(IEnumerable<IFact> src) : base(
            new AsFact(() =>
            {
                Boolean result = true;
                foreach (IFact fact in src)
                {
                    if (fact.IsFalse())
                    {
                        result = false;
                        break;
                    }
                }
                return result;
            })
        )
        { }
    }

    /// <summary> Logical and. Returns true if all contents return true. </summary>
    public sealed class And : FactEnvelope
    {
        /// <summary> Logical and. Returns true if all calls to <see cref="Func{In, Out}"/> were true. </summary>
        /// <param name="funcs"> the conditions to apply </param>
        public And(params Func<bool>[] funcs) : this(AsEnumerable._(funcs))
        { }

        /// <summary> Logical and. Returns true if all calls to <see cref="Func{Out}"/> were true. </summary>
        /// <param name="funcs"> the conditions to apply </param>
        public And(IEnumerable<Func<bool>> funcs) : this(
            Mapped._(
                func => new AsFact(func),
                funcs
            )
        )
        { }

        /// <summary> ctor </summary>
        /// <param name="src"> list of items </param>
        public And(params IFact[] src) : this(
           AsEnumerable._(src)
        )
        { }

        /// <summary> ctor </summary>
        /// <param name="src"> list of items </param>
        public And(params bool[] src) : this(
            Mapped._(
                tBool => new AsFact(tBool),
                src
            )
        )
        { }

        /// <summary> ctor </summary>
        /// <param name="src"> list of items </param>
        public And(IEnumerable<bool> src) : this(
            Mapped._(
                tBool => new AsFact(tBool),
                src
            )
        )
        { }

        /// <summary> ctor </summary>
        /// <param name="src"> list of items </param>
        public And(IEnumerable<IFact> src) : base(
            new AsFact(() =>
            {
                Boolean result = true;
                foreach (IFact item in src)
                {
                    if (item.IsFalse())
                    {
                        result = false;
                        break;
                    }
                }
                return result;
            })
        )
        { }

        public static IFact _(params IFact[] src) => new And(src);

        /// <summary> Logical and. Returns true if all calls to <see cref="Func{In, Out}"/> were true. </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public static IFact _<In>(Func<In, bool> func, params In[] src) => new And<In>(func, src);

        /// <summary> ctor </summary>
        /// <param name="func"> the condition to apply </param>
        /// <param name="src"> list of items </param>
        public static IFact _<In>(Func<In, Boolean> func, IEnumerable<In> src) => new And<In>(func, src);

        /// <summary> True if all functions return true with given input value </summary>
        /// <param name="value"> Input value wich will executed by all given functions </param>
        /// <param name="functions"> Functions wich will executed with given input value </param>
        public static IFact _<In>(In value, params Func<In, bool>[] functions) => new And<In>(value, functions);
    }
}
