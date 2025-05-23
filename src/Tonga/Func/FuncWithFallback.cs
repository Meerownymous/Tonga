

using System;

namespace Tonga.Func
{
    /// <summary>
    /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
    /// </summary>
    /// <typeparam name="In"></typeparam>
    /// <typeparam name="Out"></typeparam>
    public sealed class FuncWithFallback<In, Out> : IFunc<In, Out>
    {
        /// <summary>
        /// func to call
        /// </summary>
        private readonly IFunc<In, Out> fund;

        /// <summary>
        /// fallback to call when necessary
        /// </summary>
        private readonly IFunc<Exception, Out> fallback;

        /// <summary>
        /// a followup function
        /// </summary>
        private readonly IFunc<Out, Out> follow;

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="func">func to call</param>
        /// <param name="fallback">fallback func</param>
        public FuncWithFallback(Func<In, Out> func, Func<Exception, Out> fallback) : this(
            new AsFunc<In, Out>((X) => func(X)),
            new AsFunc<Exception, Out>((e) => fallback(e)))
        { }

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="func">func to call</param>
        /// <param name="fallback">fallback func</param>
        public FuncWithFallback(Func<In, Out> func, IFunc<Exception, Out> fallback) : this(
            new AsFunc<In, Out>((X) => func(X)),
            fallback)
        { }

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">func to call</param>
        /// <param name="fbk">fallback func</param>
        public FuncWithFallback(IFunc<In, Out> fnc, IFunc<Exception, Out> fbk) : this(
            fnc,
            fbk,
            new AsFunc<Out, Out>((input) => input))
        { }

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="func">func to call</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="flw">func to call afterwards</param>
        public FuncWithFallback(Func<In, Out> func, Func<Exception, Out> fallback, Func<Out, Out> flw) : this(
            new AsFunc<In, Out>((X) => func(X)),
            new AsFunc<Exception, Out>((e) => fallback(e)),
            new AsFunc<Out, Out>(flw))
        { }

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">func to call</param>
        /// <param name="fbk">fallback func</param>
        /// <param name="flw">func to call afterwards</param>
        public FuncWithFallback(IFunc<In, Out> fnc, IFunc<Exception, Out> fbk, IFunc<Out, Out> flw)
        {
            this.fund = fnc;
            this.fallback = fbk;
            this.follow = flw;
        }

        /// <summary>
        /// invoke function with input and retrieve output.
        /// </summary>
        /// <param name="input">input argument</param>
        /// <returns>the result</returns>
        public Out Invoke(In input)
        {
            Out result;
            try
            {
                result = this.fund.Invoke(input);
            }
            catch (Exception ex)
            {
                result = this.fallback.Invoke(ex);
            }
            return this.follow.Invoke(result);
        }
    }

    /// <summary>
    /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
    /// </summary>
    /// <typeparam name="Out">Return type</typeparam>
    public sealed class FuncWithFallback<Out> : IFunc<Out>
    {
        /// <summary>
        /// func to call
        /// </summary>
        private readonly IFunc<Out> func;

        /// <summary>
        /// fallback to call when necessary
        /// </summary>
        private readonly IFunc<Exception, Out> fallback;

        /// <summary>
        /// a followup function
        /// </summary>
        private readonly IFunc<Out, Out> follow;

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="func">func to call</param>
        /// <param name="fallback">fallback func</param>
        public FuncWithFallback(Func<Out> func, Func<Exception, Out> fallback) : this(
            new AsFunc<Out>(() => func()),
            new AsFunc<Exception, Out>((e) => fallback(e)))
        { }

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="func">func to call</param>
        /// <param name="fallback">fallback func</param>
        public FuncWithFallback(Func<Out> func, IFunc<Exception, Out> fallback) : this(
            new AsFunc<Out>(() => func()),
            fallback)
        { }

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">func to call</param>
        /// <param name="fbk">fallback func</param>
        public FuncWithFallback(IFunc<Out> fnc, IFunc<Exception, Out> fbk) : this(
            fnc,
            fbk,
            new AsFunc<Out, Out>((input) => input))
        { }

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="func">func to call</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="flw">func to call afterwards</param>
        public FuncWithFallback(Func<Out> func, Func<Exception, Out> fallback, Func<Out, Out> flw) : this(
            new AsFunc<Out>(() => func()),
            new AsFunc<Exception, Out>((e) => fallback(e)),
            new AsFunc<Out, Out>(flw))
        { }

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">func to call</param>
        /// <param name="fbk">fallback func</param>
        /// <param name="flw">func to call afterwards</param>
        public FuncWithFallback(IFunc<Out> fnc, IFunc<Exception, Out> fbk, IFunc<Out, Out> flw)
        {
            this.func = fnc;
            this.fallback = fbk;
            this.follow = flw;
        }

        /// <summary>
        /// Get output
        /// </summary>
        /// <returns></returns>
        public Out Invoke()
        {
            Out result;
            try
            {
                result = this.func.Invoke();
            }
            catch (Exception ex)
            {
                result = this.fallback.Invoke(ex);
            }
            return this.follow.Invoke(result);
        }
    }

    public static class FuncWithFallback
    {
        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="func">func to call</param>
        /// <param name="fallback">fallback func</param>
        public static IFunc<In, Out> _<In, Out>(Func<In, Out> func, Func<Exception, Out> fallback) =>
            new FuncWithFallback<In, Out>(func, fallback);

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="func">func to call</param>
        /// <param name="fallback">fallback func</param>
        public static IFunc<In, Out> _<In, Out>(Func<In, Out> func, IFunc<Exception, Out> fallback) =>
            new FuncWithFallback<In, Out>(func, fallback);

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">func to call</param>
        /// <param name="fbk">fallback func</param>
        public static IFunc<In, Out> _<In, Out>(IFunc<In, Out> fnc, IFunc<Exception, Out> fbk) =>
            new FuncWithFallback<In, Out>(fnc, fbk);

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="func">func to call</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="flw">func to call afterwards</param>
        public static IFunc<In, Out> _<In, Out>(Func<In, Out> func, Func<Exception, Out> fallback, Func<Out, Out> flw) =>
            new FuncWithFallback<In, Out>(func, fallback, flw);

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">func to call</param>
        /// <param name="fbk">fallback func</param>
        /// <param name="flw">func to call afterwards</param>
        public static IFunc<In, Out> _<In, Out>(IFunc<In, Out> fnc, IFunc<Exception, Out> fbk, IFunc<Out, Out> flw) =>
            new FuncWithFallback<In, Out>(fnc, fbk, flw);

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="func">func to call</param>
        /// <param name="fallback">fallback func</param>
        public static IFunc<Out> _<Out>(Func<Out> func, Func<Exception, Out> fallback) =>
            new FuncWithFallback<Out>(func, fallback);

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="func">func to call</param>
        /// <param name="fallback">fallback func</param>
        public static IFunc<Out> _<Out>(Func<Out> func, IFunc<Exception, Out> fallback) =>
            new FuncWithFallback<Out>(func, fallback);

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">func to call</param>
        /// <param name="fbk">fallback func</param>
        public static IFunc<Out> _<Out>(IFunc<Out> fnc, IFunc<Exception, Out> fbk) =>
            new FuncWithFallback<Out>(fnc, fbk);

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="func">func to call</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="flw">func to call afterwards</param>
        public static IFunc<Out> _<Out>(Func<Out> func, Func<Exception, Out> fallback, Func<Out, Out> flw) =>
            new FuncWithFallback<Out>(func, fallback, flw);

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">func to call</param>
        /// <param name="fbk">fallback func</param>
        /// <param name="flw">func to call afterwards</param>
        public static IFunc<Out> _<Out>(IFunc<Out> fnc, IFunc<Exception, Out> fbk, IFunc<Out, Out> flw) =>
            new FuncWithFallback<Out>(fnc, fbk, flw);

    }
}
