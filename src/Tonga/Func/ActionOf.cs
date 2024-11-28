

using System;

namespace Tonga.Func
{
    /// <summary>
    /// Action with input but no output as runnable.
    /// </summary>
    public sealed class ActionOf(Action fnc) : IAction
    {
        /// <summary>
        /// Run the runnable.
        /// </summary>
        public void Invoke()
        {
            new FuncOf<bool, bool>((input) =>
                {
                    fnc.Invoke();
                    return true;
                }).Invoke(true);
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="action">action to execute</param>
        public static IAction<T> _<T>(Action<T> action) => new ActionOf<T>(action);
    }

    /// <summary>
    /// Action<typeparamref name="In"/> as IAction<typeparamref name="In"/>
    /// </summary>
    /// <typeparam name="In"></typeparam>
    public sealed class ActionOf<In>(Action<In> fnc) : IAction<In>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="action">action to execute</param>
        public ActionOf(Action action) : this(_ => { action(); })
        { }

        /// <summary>
        /// Execute the action.
        /// </summary>
        /// <param name="input">input argument</param>
        public void Invoke(In input) => fnc.Invoke(input);
    }
}
