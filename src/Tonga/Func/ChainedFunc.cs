

using System.Collections.Generic;

namespace Tonga.Func
{
    /// <summary>
    /// Chains functions together.
    /// </summary>
    /// <typeparam name="In"></typeparam>
    /// <typeparam name="Between"></typeparam>
    /// <typeparam name="Out"></typeparam>
    public sealed class ChainedFunc<In, Between, Out> : IFunc<In, Out>
    {
        /// <summary>
        /// first function
        /// </summary>
        private readonly IFunc<In, Between> before;

        /// <summary>
        /// chained functions
        /// </summary>
        private readonly IEnumerable<IFunc<Between, Between>> funcs;

        /// <summary>
        /// last function
        /// </summary>
        private readonly IFunc<Between, Out> after;

        /// <summary>
        /// Chains functions together.
        /// </summary>
        /// <param name="before">first function</param>
        /// <param name="after">last function</param>
        public ChainedFunc(System.Func<In, Between> before, System.Func<Between, Out> after) : this(
            new FuncOf<In, Between>(input => before(input)),
            new List<IFunc<Between, Between>>(),
            new FuncOf<Between, Out>((bet) => after(bet)))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="before">first function</param>
        /// <param name="after">last function</param>
        public ChainedFunc(IFunc<In, Between> before, IFunc<Between, Out> after) : this(
            before,
            new List<IFunc<Between, Between>>(),
            after)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="before">first function</param>
        /// <param name="funcs">functions to chain</param>
        /// <param name="after">last function</param>
        public ChainedFunc(System.Func<In, Between> before, IEnumerable<IFunc<Between, Between>> funcs, System.Func<Between, Out> after) : this(
            new FuncOf<In, Between>(before),
            funcs,
            new FuncOf<Between, Out>(after))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="before">first function</param>
        /// <param name="funcs">functions to chain</param>
        /// <param name="after">last function</param>
        public ChainedFunc(System.Func<In, Between> before, IEnumerable<System.Func<Between, Between>> funcs, System.Func<Between, Out> after
        ) : this(
                new FuncOf<In, Between>(before),
                    new Enumerable.Mapped<System.Func<Between, Between>, IFunc<Between, Between>>(
                        f => new FuncOf<Between, Between>(f),
                        funcs),
                    new FuncOf<Between, Out>(after))
        { }


        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="before">first function</param>
        /// <param name="funcs">functions to chain</param>
        /// <param name="after">last function</param>
        public ChainedFunc(
            IFunc<In, Between> before,
            IEnumerable<IFunc<Between, Between>> funcs,
            IFunc<Between, Out> after
        )
        {
            this.before = before;
            this.funcs = funcs;
            this.after = after;
        }

        /// <summary>
        /// applys input to the chain
        /// </summary>
        /// <param name="input">input to apply</param>
        /// <returns>output</returns>
        public Out Invoke(In input)
        {
            Between temp = this.before.Invoke(input);
            foreach (IFunc<Between, Between> func in this.funcs)
            {
                temp = func.Invoke(temp);
            }
            return this.after.Invoke(temp);
        }
    }

    public static class ChainedFunc
    {
        /// <summary>
        /// Chains functions together.
        /// </summary>
        /// <param name="before">first function</param>
        /// <param name="after">last function</param>
        public static IFunc<In, Out> New<In, Between, Out>(System.Func<In, Between> before, System.Func<Between, Out> after)
            => new ChainedFunc<In, Between, Out>(before, after);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="before">first function</param>
        /// <param name="after">last function</param>
        public static IFunc<In, Out> New<In, Between, Out>(IFunc<In, Between> before, IFunc<Between, Out> after)
            => new ChainedFunc<In, Between, Out>(before, after);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="before">first function</param>
        /// <param name="funcs">functions to chain</param>
        /// <param name="after">last function</param>
        public static IFunc<In, Out> New<In, Between, Out>(System.Func<In, Between> before, IEnumerable<IFunc<Between, Between>> funcs, System.Func<Between, Out> after)
            => new ChainedFunc<In, Between, Out>(before, funcs, after);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="before">first function</param>
        /// <param name="funcs">functions to chain</param>
        /// <param name="after">last function</param>
        public static IFunc<In, Out> New<In, Between, Out>(System.Func<In, Between> before, IEnumerable<System.Func<Between, Between>> funcs, System.Func<Between, Out> after)
            => new ChainedFunc<In, Between, Out>(before, funcs, after);


        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="before">first function</param>
        /// <param name="funcs">functions to chain</param>
        /// <param name="after">last function</param>
        public static IFunc<In, Out> New<In, Between, Out>(IFunc<In, Between> before, IEnumerable<IFunc<Between, Between>> funcs, IFunc<Between, Out> after)
            => new ChainedFunc<In, Between, Out>(before, funcs, after);
    }
}
