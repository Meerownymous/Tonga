

using System;

namespace Tonga.Func
{
    /// <summary>
    /// Function that repeats its calculation a few times before returning the result.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class RepeatedFunc<In, Out> : IFunc<In, Out>
    {
        /// <summary>
        /// function to call
        /// </summary>
        private readonly IFunc<In, Out> func;

        /// <summary>
        /// how often to run
        /// </summary>
        private readonly int times;

        /// <summary>
        /// Function that repeats its calculation a few times before returning the result.
        /// </summary>
        /// <param name="fnc">function to call</param>
        /// <param name="max">how often it repeats</param>
        public RepeatedFunc(Func<In, Out> fnc, int max) : this(new AsFunc<In, Out>((X) => fnc(X)), max)
        { }

        /// <summary>
        /// Function that repeats its calculation a few times before returning the result.
        /// </summary>
        /// <param name="fnc">function to call</param>
        /// <param name="max">how often it repeats</param>
        public RepeatedFunc(IFunc<In, Out> fnc, int max)
        {
            this.func = fnc;
            this.times = max;
        }

        /// <summary>
        /// Invoke the function and retrieve the output.
        /// </summary>
        /// <param name="input">the input argument</param>
        /// <returns>the output</returns>
        public Out Invoke(In input)
        {
            if (this.times <= 0)
            {
                throw new ArgumentException("The number of repetitions must be at least 1");
            }

            Out result;
            result = this.func.Invoke(input);
            for (int idx = 0; idx < this.times - 1; ++idx)
            {
                result = this.func.Invoke(input);
            }
            return result;
        }
    }

    public static class RepeatedFunc
    {
        /// <summary>
        /// Function that repeats its calculation a few times before returning the result.
        /// </summary>
        /// <param name="fnc">function to call</param>
        /// <param name="max">how often it repeats</param>
        public static IFunc<In, Out> _<In, Out>(Func<In, Out> fnc, int max) => new RepeatedFunc<In, Out>(fnc, max);

        /// <summary>
        /// Function that repeats its calculation a few times before returning the result.
        /// </summary>
        /// <param name="fnc">function to call</param>
        /// <param name="max">how often it repeats</param>
        public static IFunc<In, Out> _<In, Out>(IFunc<In, Out> fnc, int max) => new RepeatedFunc<In, Out>(fnc, max);
    }
}
