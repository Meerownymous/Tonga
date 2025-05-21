

using System.Threading.Tasks;

namespace Tonga.Func
{
    /// <summary>
    /// Func that runs in the background.
    /// If you want your piece of code to be executed in the background, use
    /// <see cref="AsyncFunc{In, Out}"/> as following:
    /// int length = new AsyncFunc(
    ///     input => input.length()
    /// ).Apply("Hello, world!").Length;
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class AsyncFunc<In, Out> : IFunc<In, Task<Out>>
        where Out : class
    {
        /// <summary>
        /// func to call
        /// </summary>
        private readonly IFunc<In, Out> func;

        /// <summary>
        /// Func that runs in the background.
        /// </summary>
        /// <param name="act">procedure to call</param>
        public AsyncFunc(IAction<In> act) : this(new AsFunc<In, Out>(act, null)) //@TODO eliminate null passing
        { }

        /// <summary>
        /// Func that runs in the background.
        /// If you want your piece of code to be executed in the background, use
        /// <see cref="AsyncFunc{In, Out}"/> as following:
        /// int length = new AsyncFunc(
        ///     input => input.length()
        /// ).Apply("Hello, world!").Length;
        /// </summary>
        /// <param name="func">func to call</param>
        public AsyncFunc(System.Func<In, Out> func) : this(new AsFunc<In, Out>((X) => func(X)))
        { }

        /// <summary>
        /// Func that runs in the background.
        /// If you want your piece of code to be executed in the background, use
        /// <see cref="AsyncFunc{In, Out}"/> as following:
        /// int length = new AsyncFunc(
        ///     input => input.length()
        /// ).Apply("Hello, world!").Length;
        /// </summary>
        /// <param name="fnc">func to call</param>
        public AsyncFunc(IFunc<In, Out> fnc)
        {
            this.func = fnc;
        }

        /// <summary>
        /// Invoke the function and retrieve the output.
        /// </summary>
        /// <param name="input">the input argument</param>
        /// <returns>the output</returns>
        public async Task<Out> Invoke(In input)
        {
            return await Task.Run(() => this.func.Invoke(input));
        }
    }

    /// <summary>
    /// Func that runs in the background.
    /// If you want your piece of code to be executed in the background, use
    /// <see cref="AsyncFunc{In, Out}"/> as following:
    /// int length = new AsyncFunc(
    ///     input => input.length()
    /// ).Apply("Hello, world!").Length;
    /// </summary>
    public static class AsyncFunc
    {
        /// <summary>
        /// Func that runs in the background.
        /// If you want your piece of code to be executed in the background, use
        /// <see cref="AsyncFunc{In, Out}"/> as following:
        /// int length = new AsyncFunc(
        ///     input => input.length()
        /// ).Apply("Hello, world!").Length;
        /// </summary>
        /// <param name="func">func to call</param>
        public static IFunc<In, Task<Out>> _<In, Out>(System.Func<In, Out> func)
            where Out : class
            => new AsyncFunc<In, Out>(func);

        /// <summary>
        /// Func that runs in the background.
        /// If you want your piece of code to be executed in the background, use
        /// <see cref="AsyncFunc{In, Out}"/> as following:
        /// int length = new AsyncFunc(
        ///     input => input.length()
        /// ).Apply("Hello, world!").Length;
        /// </summary>
        /// <param name="fnc">func to call</param>
        public static IFunc<In, Task<Out>> _<In, Out>(IFunc<In, Out> fnc)
            where Out : class
            => new AsyncFunc<In, Out>(fnc);
    }
}
