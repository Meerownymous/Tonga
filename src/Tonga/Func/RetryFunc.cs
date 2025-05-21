

using System;

namespace Tonga.Func
{
    /// <summary>
    /// Function that will retry if it fails.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class RetryFunc<In, Out> : IFunc<In, Out>
    {
        /// <summary>
        /// func to retry
        /// </summary>
        private readonly IFunc<In, Out> func;

        /// <summary>
        /// exit condition
        /// </summary>
        private readonly IFunc<Int32, Boolean> exit;

        /// <summary>
        /// Function that will retry if it fails.
        /// </summary>
        /// <param name="fnc">func to retry</param>
        public RetryFunc(Func<In, Out> fnc) : this(new AsFunc<In, Out>((X) => fnc(X)), 3)
        { }

        /// <summary>
        /// Function that will retry if it fails.
        /// </summary>
        /// <param name="fnc">func to retry</param>
        public RetryFunc(IFunc<In, Out> fnc) : this(fnc, 3)
        { }

        /// <summary>
        /// Function that will retry if it fails.
        /// </summary>
        /// <param name="fnc">func to retry</param>
        /// <param name="attempts">how often to retry</param>
        public RetryFunc(Func<In, Out> fnc, int attempts = 3) :
            this(new AsFunc<In, Out>(fnc), attempts)
        { }

        /// <summary>
        /// Function that will retry if it fails.
        /// </summary>
        /// <param name="fnc">func to retry</param>
        /// <param name="attempts">how often to retry</param>
        public RetryFunc(IFunc<In, Out> fnc, int attempts = 3) :
            this(fnc, new AsFunc<Int32, Boolean>(attempt => attempt >= attempts))
        { }

        /// <summary>
        /// Function that will retry if it fails.
        /// </summary>
        /// <param name="fnc">func to retry</param>
        /// <param name="ext">exit condition</param>
        public RetryFunc(IFunc<In, Out> fnc, Func<Int32, Boolean> ext) :
            this(fnc, new AsFunc<Int32, Boolean>((i) => ext(i)))
        { }

        /// <summary>
        /// Function that will retry if it fails.
        /// </summary>
        /// <param name="fnc">func to retry</param>
        /// <param name="ext">exit condition</param>
        public RetryFunc(Func<In, Out> fnc, Func<Int32, Boolean> ext) :
            this(new AsFunc<In, Out>((X) => fnc(X)), new AsFunc<Int32, Boolean>((i) => ext(i)))
        { }

        /// <summary>
        /// Function that will retry if it fails.
        /// </summary>
        /// <param name="fnc">func to retry</param>
        /// <param name="ext">exit condition</param>
        public RetryFunc(IFunc<In, Out> fnc, IFunc<Int32, Boolean> ext)
        {
            this.func = fnc;
            this.exit = ext;
        }

        /// <summary>
        /// Invoke the function and retrieve the output.
        /// </summary>
        /// <param name="input">the input argument</param>
        /// <returns>the output</returns>
        public Out Invoke(In input)
        {
            int attempt = 0;
            Exception error = new ArgumentException(
                "An immediate exit, didn't have a chance to try at least once");

            while (!this.exit.Invoke(attempt))
            {
                try
                {
                    return this.func.Invoke(input);
                }
                catch (Exception ex)
                {
                    error = ex;
                }
                ++attempt;
            }
            throw error;
        }
    }

    public static class RetryFunc
    {
        /// <summary>
        /// Function that will retry if it fails.
        /// </summary>
        /// <param name="fnc">func to retry</param>
        public static IFunc<In, Out> _<In, Out>(Func<In, Out> fnc) =>
            new RetryFunc<In, Out>(fnc);

        /// <summary>
        /// Function that will retry if it fails.
        /// </summary>
        /// <param name="fnc">func to retry</param>
        public static IFunc<In, Out> _<In, Out>(IFunc<In, Out> fnc) =>
            new RetryFunc<In, Out>(fnc);

        /// <summary>
        /// Function that will retry if it fails.
        /// </summary>
        /// <param name="fnc">func to retry</param>
        /// <param name="attempts">how often to retry</param>
        public static IFunc<In, Out> _<In, Out>(Func<In, Out> fnc, int attempts = 3) =>
            new RetryFunc<In, Out>(fnc);

        /// <summary>
        /// Function that will retry if it fails.
        /// </summary>
        /// <param name="fnc">func to retry</param>
        /// <param name="attempts">how often to retry</param>
        public static IFunc<In, Out> _<In, Out>(IFunc<In, Out> fnc, int attempts = 3) =>
            new RetryFunc<In, Out>(fnc, attempts);

        /// <summary>
        /// Function that will retry if it fails.
        /// </summary>
        /// <param name="fnc">func to retry</param>
        /// <param name="stopCondition">exit condition</param>
        public static IFunc<In, Out> _<In, Out>(IFunc<In, Out> fnc, Func<Int32, Boolean> stopCondition) =>
            new RetryFunc<In, Out>(fnc, stopCondition);

        /// <summary>
        /// Function that will retry if it fails.
        /// </summary>
        /// <param name="fnc">func to retry</param>
        /// <param name="stopCondition">exit condition</param>
        public static IFunc<In, Out> _<In, Out>(Func<In, Out> fnc, Func<Int32, Boolean> stopCondition) =>
            new RetryFunc<In, Out>(fnc, stopCondition);

        /// <summary>
        /// Function that will retry if it fails.
        /// </summary>
        /// <param name="fnc">func to retry</param>
        /// <param name="stopCondition">exit condition</param>
        public static IFunc<In, Out> _<In, Out>(IFunc<In, Out> fnc, IFunc<Int32, Boolean> stopCondition) =>
            new RetryFunc<In, Out>(fnc, stopCondition);
    }
}
