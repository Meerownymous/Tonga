

namespace Tonga.Func
{
    /// <summary>
    /// Function that has two inputs and an output
    /// </summary>
    /// <typeparam name="In1">type of first input</typeparam>
    /// <typeparam name="In2">type of second input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class BiFuncOf<In1, In2, Out> : IBiFunc<In1, In2, Out>
    {
        private readonly System.Func<In1, In2, Out> func;

        /// <summary>
        /// Function that has two inputs and an output.
        /// </summary>
        /// <param name="func"></param>
        public BiFuncOf(System.Func<In1, In2, Out> func)
        {
            this.func = func;
        }

        /// <summary>
        /// Invoke the function with arguments and retrieve th output.
        /// </summary>
        /// <param name="arg1">first argument</param>
        /// <param name="arg2">second argument</param>
        /// <returns>the output</returns>
        public Out Invoke(In1 arg1, In2 arg2)
        {
            return func.Invoke(arg1, arg2);
        }
    }

    public static class BiFuncOf
    {
        /// <summary>
        /// Function that has two inputs and an output.
        /// </summary>
        /// <param name="func"></param>
        public static IBiFunc<In1, In2, Out> _<In1, In2, Out>(System.Func<In1, In2, Out> func) =>
            new BiFuncOf<In1, In2, Out>(func);
    }
}
