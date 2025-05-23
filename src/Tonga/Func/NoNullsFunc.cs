

using System;
using System.IO;


namespace Tonga.Func
{
    /// <summary>
    /// Function that does not allow null as input.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class NoNullsFunc<In, Out> : IFunc<In, Out>
    {
        /// <summary>
        /// The function
        /// </summary>
        private readonly IFunc<In, Out> func;

        /// <summary>
        /// Function that does not allow null as input.
        /// </summary>
        /// <param name="func">the function</param>
        public NoNullsFunc(IFunc<In, Out> func)
        {
            this.func = func;
        }

        /// <summary>
        /// Apply it
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>the output</returns>
        public Out Invoke(In input)
        {
            if(input == null) throw new ArgumentException("got NULL instead of a valid function");

            Out result = func.Invoke(input);
            if (result == null)
            {
                throw new IOException("got NULL instead of a valid result");
            }
            return result;
        }
    }

    /// <summary>
    /// Check whether a func returns null. React with Exception or fallback value / function.
    /// </summary>
    /// <typeparam name="Out">The type of output</typeparam>
    public sealed class NoNullsFunc<Out> : IFunc<Out>
    {
        /// <summary>
        /// fnc to call
        /// </summary>
        private readonly IFunc<Out> func;

        /// <summary>
        /// error to raise
        /// </summary>
        private readonly IFunc<Out> fallback;

        /// <summary>
        /// Raises an <see cref="IOException"/> when the return value is null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        public NoNullsFunc(Func<Out> fnc) : this(new AsFunc<Out>(fnc), new IOException("Return value is null"))
        { }

        /// <summary>
        /// Raises an <see cref="IOException"/> when the return value is null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        public NoNullsFunc(IFunc<Out> fnc) : this(fnc, new IOException("Return value is null"))
        { }

        /// <summary>
        /// Raises given <see cref="Exception"/> when the return value is null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        /// <param name="ex">Exception to throw</param>
        public NoNullsFunc(Func<Out> fnc, Exception ex) : this(new AsFunc<Out>(fnc), new AsFunc<Out>(() => throw ex))
        { }

        /// <summary>
        /// Raises given <see cref="Exception"/> when the return value is null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        /// <param name="ex">Exception to throw</param>
        public NoNullsFunc(IFunc<Out> fnc, Exception ex) : this(fnc, new AsFunc<Out>(() => throw ex))
        { }

        /// <summary>
        /// Returns the fallback if the func returns null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        /// <param name="fallback">fallback value</param>
        public NoNullsFunc(Func<Out> fnc, Out fallback) : this(new AsFunc<Out>(fnc), new AsFunc<Out>(() => fallback))
        { }

        /// <summary>
        /// Returns the fallback if the func returns null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        /// <param name="fallback">fallback value</param>
        public NoNullsFunc(IFunc<Out> fnc, Out fallback) : this(fnc, new AsFunc<Out>(() => fallback))
        { }

        /// <summary>
        /// Calls the fallback function if the func return null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        /// <param name="fallback">fallback value</param>
        public NoNullsFunc(Func<Out> fnc, Func<Out> fallback) : this(new AsFunc<Out>(fnc), new AsFunc<Out>(fallback))
        { }

        /// <summary>
        /// Calls the fallback function if the func return null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        /// <param name="fallback">fallback value</param>
        public NoNullsFunc(IFunc<Out> fnc, IFunc<Out> fallback)
        {
            func = fnc;
            this.fallback = fallback;
        }

        /// <summary>
        /// Call the function to get the value
        /// </summary>
        /// <returns>The value or the fallback value (if any)</returns>
        public Out Invoke()
        {
            Out ret = func.Invoke();

            if (ret == null) ret = fallback.Invoke();

            return ret;
        }
    }

    public static class NoNullsFunc
    {
        /// <summary>
        /// Raises an <see cref="IOException"/> when the return value is null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        public static IFunc<In, Out> _<In, Out>(IFunc<In, Out> fnc) => new NoNullsFunc<In, Out>(fnc);

        /// <summary>
        /// Raises an <see cref="IOException"/> when the return value is null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        public static IFunc<Out> _<Out>(Func<Out> fnc) => new NoNullsFunc<Out>(fnc);

        /// <summary>
        /// Raises an <see cref="IOException"/> when the return value is null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        public static IFunc<Out> _<Out>(IFunc<Out> fnc) => new NoNullsFunc<Out>(fnc);

        /// <summary>
        /// Raises given <see cref="Exception"/> when the return value is null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        /// <param name="ex">Exception to throw</param>
        public static IFunc<Out> _<Out>(Func<Out> fnc, Exception ex) => new NoNullsFunc<Out>(fnc, ex);

        /// <summary>
        /// Raises given <see cref="Exception"/> when the return value is null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        /// <param name="ex">Exception to throw</param>
        public static IFunc<Out> _<Out>(IFunc<Out> fnc, Exception ex) => new NoNullsFunc<Out>(fnc);

        /// <summary>
        /// Returns the fallback if the func returns null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        /// <param name="fallback">fallback value</param>
        public static IFunc<Out> _<Out>(Func<Out> fnc, Out fallback) => new NoNullsFunc<Out>(fnc, fallback);

        /// <summary>
        /// Returns the fallback if the func returns null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        /// <param name="fallback">fallback value</param>
        public static IFunc<Out> _<Out>(IFunc<Out> fnc, Out fallback) => new NoNullsFunc<Out>(fnc, fallback);

        /// <summary>
        /// Calls the fallback function if the func return null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        /// <param name="fallback">fallback value</param>
        public static IFunc<Out> _<Out>(Func<Out> fnc, Func<Out> fallback) => new NoNullsFunc<Out>(fnc, fallback);

        /// <summary>
        /// Calls the fallback function if the func return null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        /// <param name="fallback">fallback value</param>
        public static IFunc<Out> _<Out>(IFunc<Out> fnc, IFunc<Out> fallback) => new NoNullsFunc<Out>(fnc, fallback);
    }
}
