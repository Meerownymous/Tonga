

using System;
using System.Collections.Generic;
using System.Linq;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.Func
{
    /// <summary>
    /// Func that caches the result and returns from cache.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class StickyFunc<In, Out>(IFunc<In, Out> fnc, IFunc<Out, bool> reloadCondition) : IFunc<In, Out>
    {
        /// <summary>
        /// cache
        /// </summary>
        private readonly Dictionary<In, Out> cache = new();

        /// <summary>
        /// Func that caches the result and returns from cache.
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        public StickyFunc(Func<In, Out> fnc) : this(new AsFunc<In, Out>(fnc))
        { }

        /// <summary>
        /// Func that caches the result and returns from cache.
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        public StickyFunc(IFunc<In, Out> fnc) : this(fnc, _ => false)
        { }

        /// <summary>
        /// Func that caches the result and returns from cache with reload condition func
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        /// <param name="reloadCondition">reload condition func</param>
        public StickyFunc(Func<In, Out> fnc, Func<Out, bool> reloadCondition) : this(
            new AsFunc<In, Out>(fnc), new AsFunc<Out, bool>(reloadCondition)
        )
        { }

        /// <summary>
        /// Func that caches the result and returns from cache with reload condition func
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        /// <param name="reloadCondition">reload condition func</param>
        public StickyFunc(IFunc<In, Out> fnc, Func<Out, bool> reloadCondition) : this(
            fnc, new AsFunc<Out, bool>(reloadCondition)
        )
        { }

        /// <summary>
        /// Func that caches the result and returns from cache with reload condition func
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        /// <param name="reloadConditionFnc">reload condition func</param>
        public StickyFunc(Func<In, Out> fnc, IFunc<Out, bool> reloadConditionFnc) : this(new AsFunc<In, Out>((X) => fnc(X)), reloadConditionFnc)
        { }

        /// <summary>
        /// Invoke the function and retrieve the output.
        /// </summary>
        /// <param name="input">input argument</param>
        /// <returns>output</returns>
        public Out Invoke(In input)
        {
            if (!this.cache.ContainsKey(input) || reloadCondition.Invoke(cache[input]))
            {
                this.cache[input] = fnc.Invoke(input);
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
        public static IFunc<In, Out> _<In, Out>(Func<In, Out> fnc) =>
            new StickyFunc<In, Out>(fnc);

        /// <summary>
        /// Func that caches the result and returns from cache.
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        public static IFunc<In, Out> _<In, Out>(IFunc<In, Out> fnc) =>
            new StickyFunc<In, Out>(fnc);

        /// <summary>
        /// Func that caches the result and returns from cache with reload condition func
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        /// <param name="reloadCondition">reload condition func</param>
        public static IFunc<In, Out> _<In, Out>(Func<In, Out> fnc, Func<Out, bool> reloadCondition) =>
            new StickyFunc<In, Out>(fnc, reloadCondition);

        /// <summary>
        /// Func that caches the result and returns from cache with reload condition func
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        /// <param name="reloadCondition">reload condition func</param>
        public static IFunc<In, Out> _<In, Out>(IFunc<In, Out> fnc, Func<Out, bool> reloadCondition) =>
            new StickyFunc<In, Out>(fnc, reloadCondition);

        /// <summary>
        /// Func that caches the result and returns from cache with reload condition func
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        /// <param name="reloadCondition">reload condition func</param>
        public static IFunc<In, Out> _<In, Out>(Func<In, Out> fnc, IFunc<Out, bool> reloadCondition) =>
            new StickyFunc<In, Out>(fnc, reloadCondition);

        /// <summary>
        /// Func that caches the result and returns from cache with reload condition func
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        /// <param name="reloadCondition">reload condition func</param>
        public static IFunc<In, Out> _<In, Out>(IFunc<In, Out> fnc, IFunc<Out, bool> reloadCondition) =>
            new StickyFunc<In, Out>(fnc, reloadCondition);
    }

    /// <summary>
    /// Function with two inputs which returns the output from cache.
    /// </summary>
    /// <typeparam name="In1">type of first argument</typeparam>
    /// <typeparam name="In2">type of second argument</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class StickyFunc<In1, In2, Out> : IFunc<In1, In2, Out>
    {
        /// <summary>
        /// original func
        /// </summary>
        private readonly IFunc<In1, In2, Out> func;

        /// <summary>
        /// cache
        /// </summary>
        private readonly Dictionary<Dictionary<In1, In2>, Out> cache;

        private readonly KeyMapComparer comparer;

        /// <summary>
        /// Function with two inputs which returns the output from cache.
        /// </summary>
        /// <param name="fnc">func to cache result from</param>
        public StickyFunc(Func<In1, In2, Out> fnc) : this(new AsFunc<In1, In2, Out>(fnc))
        { }

        /// <summary>
        /// Function with two inputs which returns the output from cache.
        /// </summary>
        /// <param name="fnc">func to cache result from</param>
        public StickyFunc(IFunc<In1, In2, Out> fnc)
        {
            this.func = fnc;
            this.comparer = new KeyMapComparer();
            this.cache = new Dictionary<Dictionary<In1, In2>, Out>(this.comparer);
        }

        /// <summary>
        /// Invoke the function and get the output.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public Out Invoke(In1 first, In2 second)
        {
            var keymap = new Dictionary<In1, In2>
            {
                [first] = second
            };

            Out output;
            var km = new Filtered<Dictionary<In1, In2>>(key => this.comparer.Equals(keymap, key), this.cache.Keys);
            if (!km.Any())
            {
                output = this.func.Invoke(first, second);
                this.cache.Add(keymap, output);
                km = new Filtered<Dictionary<In1, In2>>(key => this.comparer.Equals(keymap, key), this.cache.Keys);
            }
            return this.cache[new ItemAt<Dictionary<In1, In2>>(km).Value()];
        }

        private sealed class KeyMapComparer : IEqualityComparer<Dictionary<In1, In2>>
        {
            public bool Equals(Dictionary<In1, In2> x, Dictionary<In1, In2> y)
            {
                var equal = x.Keys.Count == y.Keys.Count;
                if (equal)
                {
                    for (var i = 0; i < x.Keys.Count; i++)
                    {
                        if (!x.Keys.ElementAt(i).Equals(y.Keys.ElementAt(i)))
                        {
                            equal = false;
                            break;
                        }
                    }
                }

                if (equal)
                {
                    foreach (var key in x.Keys)
                    {
                        if (!x[key].Equals(y[key]))
                        {
                            equal = false;
                            break;
                        }
                    }
                }
                return equal;
            }

            public int GetHashCode(Dictionary<In1, In2> obj) =>
                obj.GetHashCode();
        }

        /// <summary>
        /// Function with two inputs which returns the output from cache.
        /// </summary>
        /// <param name="fnc">func to cache result from</param>
        public static IFunc<In1, In2, Out> _(Func<In1, In2, Out> fnc) =>
            new StickyFunc<In1, In2, Out>(fnc);

        /// <summary>
        /// Function with two inputs which returns the output from cache.
        /// </summary>
        /// <param name="fnc">func to cache result from</param>
        public static IFunc<In1, In2, Out> _(IFunc<In1, In2, Out> fnc) =>
            new StickyFunc<In1, In2, Out>(fnc);
    }
}
