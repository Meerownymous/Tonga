

using Tonga;
using Tonga.Func;

namespace Tonga.Func
{
    /// <summary>
    /// Function that has only output.
    /// </summary>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class FuncOf<Out>(System.Func<Out> fnc) : IFunc<Out>
    {
        /// <summary>
        /// Call function and retrieve output.
        /// </summary>
        /// <returns>the output</returns>
        public Out Invoke() => fnc();
    }


    /// <summary>
    /// Function that has input and output
    /// </summary>
    /// <typeparam name="In">input</typeparam>
    /// <typeparam name="Out">output</typeparam>
    public sealed class FuncOf<In, Out>(System.Func<In, Out> func) : IFunc<In, Out>
    {

        /// <summary>
        /// ctor
        /// </summary>
        public FuncOf(System.Func<Out> fnc) : this(_ => fnc.Invoke())
        {
        }

        /// <summary>
        /// Function that has input and output
        /// </summary>
        public FuncOf(IAction<In> proc, Out result) : this(
            input =>
            {
                proc.Invoke(input);
                return result;
            }
        )
        {
        }

        /// <summary>
        /// Generate the Output from the input
        /// </summary>
        /// <param name="input"></param>
        public Out Invoke(In input) => func.Invoke(input);
    }

    /// <summary>
    /// Function that has two inputs and an output
    /// </summary>
    /// <typeparam name="In1">type of first input</typeparam>
    /// <typeparam name="In2">type of second input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class FuncOf<In1, In2, Out>(System.Func<In1, In2, Out> func) : IBiFunc<In1, In2, Out>
    {
        /// <summary>
        /// Invoke the function with arguments and retrieve th output.
        /// </summary>
        /// <param name="arg1">first argument</param>
        /// <param name="arg2">second argument</param>
        /// <returns>the output</returns>
        public Out Invoke(In1 arg1, In2 arg2) => func.Invoke(arg1, arg2);

        /// <summary>
        /// Function that has two inputs and an output.
        /// </summary>
        /// <param name="func"></param>
        public static FuncOf<In1, In2, Out> _(System.Func<In1, In2, Out> func) =>
            new(func);
    }

    /// <summary>
    /// Function that has two inputs and an output
    /// </summary>
    public sealed class FuncOf<In1, In2, In3, Out>(System.Func<In1, In2, In3, Out> func) : IFunc<In1, In2, In3, Out>
    {

        /// <summary>
        /// Invoke the function with arguments and retrieve th output.
        /// </summary>
        /// <param name="arg1">first argument</param>
        /// <param name="arg2">second argument</param>
        /// <returns>the output</returns>
        public Out Invoke(In1 arg1, In2 arg2, In3 arg3) => func(arg1, arg2, arg3);

        /// <summary>
        /// Function that has two inputs and an output.
        /// </summary>
        /// <param name="func"></param>
        public static FuncOf<In1, In2, In3, Out> _(System.Func<In1, In2, In3, Out> func) =>
            new(func);
    }

    public static class FuncOf
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc"></param>
        public static IFunc<Out> _<Out>(System.Func<Out> fnc) => new FuncOf<Out>(fnc);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc"></param>
        public static IFunc<In, Out> _<In, Out>(System.Func<In, Out> fnc) =>
            new FuncOf<In, Out>(fnc);

        /// <summary>
        /// Function that has input and output
        /// </summary>
        /// <param name="proc">procedure to execute</param>
        /// <param name="result"></param>
        public static IFunc<In, Out> _<In, Out>(IAction<In> proc, Out result) =>
            new FuncOf<In, Out>(proc, result);

        public static IBiFunc<In1, In2, Out> _<In1, In2, Out>(System.Func<In1, In2, Out> func) =>
            new FuncOf<In1, In2, Out>(func);
    }
}
