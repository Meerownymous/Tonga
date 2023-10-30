

using System;

namespace Tonga.Func
{
    /// <summary>
    /// Action with input but no output as runnable.
    /// </summary>
    public sealed class ActionOf : IAction
    {
        private readonly System.Action func;

        /// <summary>
        /// Action with input but no output as runnable.
        /// </summary>
        /// <param name="fnc"></param>
        public ActionOf(System.Action fnc)
        {
            this.func = fnc;
        }

        /// <summary>
        /// Run the runnable.
        /// </summary>
        public void Invoke()
        {
            new FuncOf<bool, bool>((input) =>
                {
                    this.func.Invoke();
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
    public sealed class ActionOf<In> : IAction<In>
    {
        /// <summary>
        /// the action
        /// </summary>
        private readonly System.Action<In> func;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="action">action to execute</param>
        public ActionOf(Action action) : this((b) => { action.Invoke(); })
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc">action to execute</param>
        public ActionOf(Action<In> fnc)
        {
            this.func = fnc;
        }

        /// <summary>
        /// Execute the action.
        /// </summary>
        /// <param name="input">input argument</param>
        public void Invoke(In input)
        {
            this.func.Invoke(input);
        }
    }
}
