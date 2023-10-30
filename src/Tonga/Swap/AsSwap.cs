namespace Tonga.Swap
{
    /// <summary>
    /// Swaps input to output
    /// </summary>
    /// <typeparam name="In">input</typeparam>
    /// <typeparam name="Out">output</typeparam>
    public sealed class AsSwap<In, Out> : ISwap<In, Out>
    {
        private readonly System.Func<In, Out> func;

        /// <summary>
        /// Swaps input and output
        /// </summary>
        /// <param name="func">function to execute</param>
        public AsSwap(System.Func<In, Out> func)
        {
            this.func = func;
        }

        /// <summary>
        /// Swap the input to an output
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Out Flip(In input)
        {
            return func.Invoke(input);
        }
    }

    /// <summary>
    /// Swap two inputs to an output
    /// </summary>
    /// <typeparam name="In1">type of first input</typeparam>
    /// <typeparam name="In2">type of second input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class AsSwap<In1, In2, Out> : ISwap<In1, In2, Out>
    {
        private readonly System.Func<In1, In2, Out> func;

        /// <summary>
        /// Function that has two inputs and an output.
        /// </summary>
        /// <param name="func"></param>
        public AsSwap(System.Func<In1, In2, Out> func)
        {
            this.func = func;
        }

        public Out Flip(In1 input1, In2 input2)
        {
            return func.Invoke(input1, input2);
        }
    }

    /// <summary>
    /// Swap three inputs to an output
    /// </summary>
    /// <typeparam name="In1"></typeparam>
    /// <typeparam name="In2"></typeparam>
    /// <typeparam name="In3"></typeparam>
    /// <typeparam name="Out"></typeparam>
    public sealed class AsSwap<In1, In2, In3, Out> : ISwap<In1, In2, In3, Out>
    {
        private readonly System.Func<In1, In2, In3, Out> func;

        /// <summary>
        /// Swap three inputs to an output
        /// </summary>
        /// <param name="func"></param>
        public AsSwap(System.Func<In1, In2, In3, Out> func)
        {
            this.func = func;
        }

        /// <summary>
        ///  /// Swap three inputs to an output
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <returns></returns>
        public Out Flip(In1 arg1, In2 arg2, In3 arg3)
        {
            return func.Invoke(arg1, arg2, arg3);
        }
    }

    /// Swap three inputs to an output
    public static class AsSwap
    {
        /// <summary>
        /// Swap an input to one output.
        /// </summary>
        public static AsSwap<In, Out> _<In, Out>(System.Func<In, Out> func) =>
            new AsSwap<In, Out>(func);

        /// <summary>
        /// Swap two inputs to one output.
        /// </summary>
        public static AsSwap<In1, In2, Out> _<In1, In2, Out>(System.Func<In1, In2, Out> func) =>
            new AsSwap<In1, In2, Out>(func);

        /// Swap three inputs to an output
        public static AsSwap<In1, In2, In3, Out> _<In1, In2, In3, Out>(
            System.Func<In1, In2, In3, Out> conversion) =>
            new AsSwap<In1, In2, In3, Out>(conversion);
    }
}
