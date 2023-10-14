using System;
using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Func;

namespace Tonga.Scalar
{
    /// <summary>
    /// Logical or. Returns true if any contents return true.
    /// </summary>
    /// <typeparam name="In"></typeparam>
    public sealed class Or<In> : IScalar<Boolean>
    {
        private readonly Or or;

        /// <summary>
        /// Logical or. Returns true if any calls to <see cref="Func{In, Out}"/>
        /// were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Or(Func<In, bool> func, params In[] src) : this(new FuncOf<In, bool>(func), Params.Of(src))
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
        public Or(IFunc<In, Boolean> func, params In[] src) : this(func, Params.Of(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Or(IFunc<In, Boolean> func, IEnumerable<In> src) : this(
            new Enumerable.Mapped<In, IScalar<Boolean>>(
                new FuncOf<In, IScalar<Boolean>>(
                    (item) => new Live<Boolean>(func.Invoke(item))
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
            item => new Or(
                new Mapped<Func<In, bool>, bool>(
                    func => func.Invoke(item),
                    functions
                )
            ).Value(),
            value
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">list of items</param>
        private Or(IEnumerable<IScalar<bool>> src) : this(new Or(src))
        { }

        /// <summary>
        /// Private primary ctor
        /// </summary>
        /// <param name="or">Non generic or</param>
        private Or(Or or)
        {
            this.or = or;
        }

        /// <summary>
        /// Get the value.
        /// </summary>
        /// <returns>the value</returns>
        public Boolean Value()
        {
            return or.Value();
        }
    }

    /// <summary>
    /// Logical or. Returns true if any contents return true.
    /// </summary>
    public sealed class Or : ScalarEnvelope<bool>
    {
        /// <summary>
        /// Logical or. Returns true if any calls to <see cref="Func{In, Out}"/>
        /// were true.
        /// </summary>
        /// <param name="funcs">the conditions to apply</param>
        public Or(params Func<bool>[] funcs) : this(Params.Of(funcs))
        { }

        /// <summary>
        /// Logical or. Returns true if any calls to <see cref="Func{Out}"/> were
        /// true.
        /// </summary>
        /// <param name="funcs">the conditions to apply</param>
        public Or(IEnumerable<Func<bool>> funcs) : this(
            new Mapped<Func<bool>, IScalar<bool>>(
                func => new Live<bool>(func),
                funcs))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">list of items</param>
        public Or(params IScalar<Boolean>[] src) : this(
            Params.Of(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">list of items</param>
        public Or(params bool[] src) : this(
            new Mapped<bool, IScalar<bool>>(
                item => new Live<bool>(item),
                src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">list of items</param>
        public Or(IEnumerable<bool> src) : this(
            new Mapped<bool, IScalar<bool>>(
                item => new Live<bool>(item),
                src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">list of items</param>
        public Or(IEnumerable<IScalar<bool>> src)
            : base(() =>
            {
                bool foundTrue = false;
                foreach (var item in src)
                {
                    if (item.Value())
                    {
                        foundTrue = true;
                        break;
                    }
                }
                return foundTrue;
            })
        { }

        /// <summary>
        /// Logical or. Returns true if any calls to <see cref="Func{In, Out}"/>
        /// were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public static Or<In> New<In>(Func<In, bool> func, params In[] src)
            => new Or<In>(func, src);

        /// <summary>
        /// Logical or. Returns true if any calls to <see cref="Func{In, Out}"/>
        /// were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public static Or<In> New<In>(Func<In, bool> func, IEnumerable<In> src)
            => new Or<In>(func, src);

        /// <summary>
        /// Logical or. Returns true if any calls to <see cref="IFunc{In, Out}"/>
        /// were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public static Or<In> New<In>(IFunc<In, Boolean> func, params In[] src)
            => new Or<In>(func, src);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public static Or<In> New<In>(IFunc<In, Boolean> func, IEnumerable<In> src)
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
        public static Or<In> New<In>(In value, params Func<In, bool>[] functions)
            => new Or<In>(value, functions);
    }
}