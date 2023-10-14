

using System;

namespace Tonga.Func
{
    /// <summary>
    /// Function that is threadsafe.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class SyncFunc<In, Out> : IFunc<In, Out>
    {
        /// <summary>
        /// original func
        /// </summary>
        private readonly IFunc<In, Out> func;

        /// <summary>
        /// threadsafe-lock
        /// </summary>
        private readonly Object lck;

        /// <summary>
        /// Function that is threadsafe.
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        public SyncFunc(Func<In, Out> fnc) : this(new FuncOf<In, Out>((X) => fnc(X)))
        { }

        /// <summary>
        /// Function that is threadsafe.
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        public SyncFunc(IFunc<In, Out> fnc) : this(fnc, fnc)
        { }

        /// <summary>
        /// Function that is threadsafe.
        /// </summary>
        /// <param name="fnc">func to cache result from</param>
        /// <param name="lck">object that will be locked</param>
        public SyncFunc(IFunc<In, Out> fnc, object lck)
        {
            this.func = fnc;
            this.lck = lck;
        }

        /// <summary>
        /// Invoke function with given input and retrieve output.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Out Invoke(In input)
        {
            lock (this.lck)
            {
                return this.func.Invoke(input);
            }
        }
    }

    public static class SyncFunc
    {
        /// <summary>
        /// Function that is threadsafe.
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        public static IFunc<In, Out> New<In, Out>(Func<In, Out> fnc) => new SyncFunc<In, Out>(fnc);

        /// <summary>
        /// Function that is threadsafe.
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        public static IFunc<In, Out> New<In, Out>(IFunc<In, Out> fnc) => new SyncFunc<In, Out>(fnc);

        /// <summary>
        /// Function that is threadsafe.
        /// </summary>
        /// <param name="fnc">func to cache result from</param>
        /// <param name="lck">object that will be locked</param>
        public static IFunc<In, Out> New<In, Out>(IFunc<In, Out> fnc, object lck) => new SyncFunc<In, Out>(fnc, lck);
    }
}
