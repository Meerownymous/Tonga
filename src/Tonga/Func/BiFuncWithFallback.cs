

using System;

namespace Tonga.Func
{
    /// <summary>
    /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
    /// </summary>
    /// <typeparam name="In1">First argument type</typeparam>
    /// <typeparam name="In2">Second argument type</typeparam>
    /// <typeparam name="Out">Return type</typeparam>
    public sealed class BiFuncWithFallback<In1, In2, Out> : IFunc<In1, In2, Out>
    {
        /// <summary>
        /// Func to call
        /// </summary>
        private readonly Func<In1, In2, Out> func;

        /// <summary>
        /// Fallback to call wehen func fails
        /// </summary>
        private readonly IFunc<Exception, Out> fallback;

        /// <summary>
        /// A follow function
        /// </summary>
        private readonly IFunc<Out, Out> followUp;

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        public BiFuncWithFallback(Func<In1, In2, Out> fnc, Func<Exception, Out> fbk) : this(
            fnc,
            new AsFunc<Exception, Out>(fbk),
            new AsFunc<Out, Out>((input) => input))
        { }

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        public BiFuncWithFallback(Func<In1, In2, Out> fnc, IFunc<Exception, Out> fbk) : this(
            fnc,
            fbk,
            new AsFunc<Out, Out>((input) => input))
        { }

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        /// <param name="flw">Func to call aferwards</param>
        public BiFuncWithFallback(Func<In1, In2, Out> fnc, Func<Exception, Out> fbk, IFunc<Out, Out> flw) : this(
            fnc,
            new AsFunc<Exception, Out>(fbk),
            flw)
        { }

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        /// <param name="flw">Func to call aferwards</param>
        public BiFuncWithFallback(Func<In1, In2, Out> fnc, IFunc<Exception, Out> fbk, Func<Out, Out> flw) : this(
            fnc,
            fbk,
            new AsFunc<Out, Out>(flw))
        { }

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        /// <param name="flw">Func to call aferwards</param>
        public BiFuncWithFallback(Func<In1, In2, Out> fnc, Func<Exception, Out> fbk, Func<Out, Out> flw) : this(
            fnc,
            new AsFunc<Exception, Out>(fbk),
            new AsFunc<Out, Out>(flw))
        { }

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        /// <param name="flw">Func to call aferwards</param>
        public BiFuncWithFallback(Func<In1, In2, Out> fnc, IFunc<Exception, Out> fbk, IFunc<Out, Out> flw)
        {
            this.func = fnc;
            this.fallback = fbk;
            this.followUp = flw;
        }

        /// <summary>
        /// Invoke bi-function with input and retrieve output.
        /// </summary>
        /// <param name="first">First input argument</param>
        /// <param name="second">Second input argument</param>
        /// <returns>The reault</returns>
        public Out Invoke(In1 first, In2 second)
        {
            Out result;
            try
            {
                result = this.func.Invoke(first, second);
            }
            catch (Exception ex)
            {
                result = this.fallback.Invoke(ex);
            }
            return this.followUp.Invoke(result);
        }
    }

    public abstract class BiFuncWithFallback
    {
        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        public static IFunc<In1, In2, Out> _<In1, In2, Out>(Func<In1, In2, Out> fnc, Func<Exception, Out> fbk)
            => new BiFuncWithFallback<In1, In2, Out>(fnc, fbk);

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        public static IFunc<In1, In2, Out> _<In1, In2, Out>(Func<In1, In2, Out> fnc, IFunc<Exception, Out> fbk) =>
            new BiFuncWithFallback<In1, In2, Out>(fnc, fbk);

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        /// <param name="flw">Func to call aferwards</param>
        public static IFunc<In1, In2, Out> _<In1, In2, Out>(Func<In1, In2, Out> fnc, Func<Exception, Out> fbk, IFunc<Out, Out> flw) =>
            new BiFuncWithFallback<In1, In2, Out>(fnc, fbk, flw);

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        /// <param name="flw">Func to call aferwards</param>
        public static IFunc<In1, In2, Out> _<In1, In2, Out>(Func<In1, In2, Out> fnc, IFunc<Exception, Out> fbk, Func<Out, Out> flw) =>
            new BiFuncWithFallback<In1, In2, Out>(fnc, fbk, flw);

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        /// <param name="flw">Func to call aferwards</param>
        public static IFunc<In1, In2, Out> _<In1, In2, Out>(Func<In1, In2, Out> fnc, Func<Exception, Out> fbk, Func<Out, Out> flw) =>
            new BiFuncWithFallback<In1, In2, Out>(fnc, fbk, flw);

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        /// <param name="flw">Func to call aferwards</param>
        public static BiFuncWithFallback<In1, In2, Out> _<In1, In2, Out>(Func<In1, In2, Out> fnc, IFunc<Exception, Out> fbk, IFunc<Out, Out> flw) =>
            new(fnc, fbk, flw);
    }
}
