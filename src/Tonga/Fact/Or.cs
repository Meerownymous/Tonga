using System;
using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Func;
using Tonga.Scalar;

namespace Tonga.Fact
{
    /// <summary>
    /// Logical or. Returns true if any contents return true.
    /// </summary>
    /// <typeparam name="In"></typeparam>
    public sealed class Or<In> : FactEnvelope
    {
        private readonly Or or;

        /// <summary>
        /// Logical or. Returns true if any calls to <see cref="Func{In, Out}"/>
        /// were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Or(Func<In, bool> func, params In[] src) : this(new FuncOf<In, bool>(func), AsEnumerable._(src))
        { }

        /// <summary>
        /// Logical or. Returns true if any calls to <see cref="Func{In, Out}"/>
        /// were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Or(Func<In, bool> func, IEnumerable<In> src) : this(new FuncOf<In, bool>(func), src)
        { }

        /// <summary>
        /// Logical or. Returns true if any calls to <see cref="IFunc{In, Out}"/>
        /// were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Or(IFunc<In, Boolean> func, params In[] src) : this(func, AsEnumerable._(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Or(IFunc<In, Boolean> func, IEnumerable<In> src) : this(
            new Mapped<In, IFact>(
                new FuncOf<In, IFact>(
                    item => new AsFact(func.Invoke(item))
                ),
                src
            )
        )
        { }

        /// <summary>
        /// True if any functions return true with given input value
        /// </summary>
        /// <param name="value">
        /// Input value wich will executed by all given functions
        /// </param>
        /// <param name="functions">
        /// Functions wich will executed with given input value
        /// </param>
        public Or(In value, params Func<In, bool>[] functions) : this(
            item =>
            new Or(
                new Mapped<Func<In, bool>, bool>(
                    func => func.Invoke(item),
                    functions
                )
            ).IsTrue(),
            value
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">list of items</param>
        private Or(IEnumerable<IFact> src) : this(new Or(src))
        { }

        /// <summary>
        /// Private primary ctor
        /// </summary>
        /// <param name="or">Non generic or</param>
        private Or(Or or) : base(or)
        { }
    }

    /// <summary>
    /// Logical or. Returns true if any contents return true.
    /// </summary>
    public sealed class Or : FactEnvelope
    {
        /// <summary>
        /// Logical or. Returns true if any calls to <see cref="Func{In, Out}"/>
        /// were true.
        /// </summary>
        /// <param name="funcs">the conditions to apply</param>
        public Or(params Func<bool>[] funcs) : this(AsEnumerable._(funcs))
        { }

        /// <summary>
        /// Logical or. Returns true if any calls to <see cref="Func{Out}"/> were
        /// true.
        /// </summary>
        /// <param name="funcs">the conditions to apply</param>
        public Or(IEnumerable<Func<bool>> funcs) : this(
            Mapped._(
                func => new AsFact(func),
                funcs
            )
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">list of items</param>
        public Or(params IFact[] src) : this(
            AsEnumerable._(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">list of items</param>
        public Or(params bool[] src) : this(
            Mapped._(
                item => new AsFact(item),
                src
            )
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">list of items</param>
        public Or(IEnumerable<bool> src) : this(
            Mapped._(
                item => new AsFact(item),
                src
            )
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">list of items</param>
        public Or(IEnumerable<IFact> src) : base(new AsFact(() =>
            {
                bool foundTrue = false;
                foreach (var item in src)
                {
                    if (item.IsTrue())
                    {
                        foundTrue = true;
                        break;
                    }
                }
                return foundTrue;
            })
        )
        { }

        /// <summary>
        /// Logical or. Returns true if any calls to <see cref="Func{In, Out}"/>
        /// were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public static Or<In> _<In>(Func<In, bool> func, params In[] src)
            => new Or<In>(func, src);

        /// <summary>
        /// Logical or. Returns true if any calls to <see cref="Func{In, Out}"/>
        /// were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public static Or<In> _<In>(Func<In, bool> func, IEnumerable<In> src)
            => new Or<In>(func, src);

        /// <summary>
        /// Logical or. Returns true if any calls to <see cref="IFunc{In, Out}"/>
        /// were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public static Or<In> _<In>(IFunc<In, Boolean> func, params In[] src)
            => new Or<In>(func, src);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public static Or<In> _<In>(IFunc<In, Boolean> func, IEnumerable<In> src)
            => new Or<In>(func, src);

        /// <summary>
        /// True if any functions return true with given input value
        /// </summary>
        /// <param name="value">
        /// Input value wich will executed by all given functions
        /// </param>
        /// <param name="functions">
        /// Functions wich will executed with given input value
        /// </param>
        public static Or<In> _<In>(In value, params Func<In, bool>[] functions)
            => new Or<In>(value, functions);
    }
}
