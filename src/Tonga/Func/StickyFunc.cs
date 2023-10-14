

using System;
using System.Collections.Generic;

namespace Tonga.Func
{
    /// <summary>
    /// Func that caches the result and returns from cache.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class StickyFunc<In, Out> : IFunc<In, Out>
    {
        /// <summary>
        /// original func
        /// </summary>
        private readonly IFunc<In, Out> func;

        /// <summary>
        /// cache
        /// </summary>
        private readonly Dictionary<In, Out> cache;

        /// <summary>
        /// Reload Condition Func
        /// </summary>
        private readonly IFunc<Out, bool> reloadCondition;

        /// <summary>
        /// Func that caches the result and returns from cache.
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        public StickyFunc(Func<In, Out> fnc) :
            this(new FuncOf<In, Out>((X) => fnc(X)))
        { }

        /// <summary>
        /// Func that caches the result and returns from cache.
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        public StickyFunc(IFunc<In, Out> fnc) : this(fnc, new Func<Out, bool>(input => false))
        { }

        /// <summary>
        /// Func that caches the result and returns from cache with reload condition func
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        /// <param name="reloadConditionFnc">reload condition func</param>
        public StickyFunc(Func<In, Out> fnc, Func<Out, bool> reloadConditionFnc) : this(new FuncOf<In, Out>((X) => fnc(X)), new FuncOf<Out, bool>((output) => reloadConditionFnc(output)))
        { }

        /// <summary>
        /// Func that caches the result and returns from cache with reload condition func
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        /// <param name="reloadConditionFnc">reload condition func</param>
        public StickyFunc(IFunc<In, Out> fnc, Func<Out, bool> reloadConditionFnc) : this(fnc, new FuncOf<Out, bool>((output) => reloadConditionFnc(output)))
        { }

        /// <summary>
        /// Func that caches the result and returns from cache with reload condition func
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        /// <param name="reloadConditionFnc">reload condition func</param>
        public StickyFunc(Func<In, Out> fnc, IFunc<Out, bool> reloadConditionFnc) : this(new FuncOf<In, Out>((X) => fnc(X)), reloadConditionFnc)
        { }


        /// <summary>
        /// Func that caches the result and returns from cache with reload condition func
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        /// <param name="reloadConditionFnc">reload condition func</param>
        public StickyFunc(IFunc<In, Out> fnc, IFunc<Out, bool> reloadConditionFnc)
        {
            this.func = fnc;
            this.cache = new Dictionary<In, Out>();
            reloadCondition = reloadConditionFnc;
        }

        /// <summary>
        /// Invoke the function and retrieve the output.
        /// </summary>
        /// <param name="input">input argument</param>
        /// <returns>output</returns>
        public Out Invoke(In input)
        {
            if (!this.cache.ContainsKey(input) || reloadCondition.Invoke(cache[input]))
            {
                this.cache[input] = func.Invoke(input);
            }
            return this.cache[input];
        }
    }

    public static class StickyFunc
    {
        /// <summary>
        /// Func that caches the result and returns from cache.
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        public static IFunc<In, Out> New<In, Out>(Func<In, Out> fnc) =>
            new StickyFunc<In, Out>(fnc);

        /// <summary>
        /// Func that caches the result and returns from cache.
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        public static IFunc<In, Out> New<In, Out>(IFunc<In, Out> fnc) =>
            new StickyFunc<In, Out>(fnc);

        /// <summary>
        /// Func that caches the result and returns from cache with reload condition func
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        /// <param name="reloadCondition">reload condition func</param>
        public static IFunc<In, Out> New<In, Out>(Func<In, Out> fnc, Func<Out, bool> reloadCondition) =>
            new StickyFunc<In, Out>(fnc, reloadCondition);

        /// <summary>
        /// Func that caches the result and returns from cache with reload condition func
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        /// <param name="reloadCondition">reload condition func</param>
        public static IFunc<In, Out> New<In, Out>(IFunc<In, Out> fnc, Func<Out, bool> reloadCondition) =>
            new StickyFunc<In, Out>(fnc, reloadCondition);

        /// <summary>
        /// Func that caches the result and returns from cache with reload condition func
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        /// <param name="reloadCondition">reload condition func</param>
        public static IFunc<In, Out> New<In, Out>(Func<In, Out> fnc, IFunc<Out, bool> reloadCondition) =>
            new StickyFunc<In, Out>(fnc, reloadCondition);

        /// <summary>
        /// Func that caches the result and returns from cache with reload condition func
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        /// <param name="reloadCondition">reload condition func</param>
        public static IFunc<In, Out> New<In, Out>(IFunc<In, Out> fnc, IFunc<Out, bool> reloadCondition) =>
            new StickyFunc<In, Out>(fnc, reloadCondition);
    }
}
