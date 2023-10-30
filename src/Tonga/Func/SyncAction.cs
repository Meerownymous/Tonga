

using System;

namespace Tonga.Func
{
    /// <summary>
    /// Proc that is threadsafe.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    public sealed class SyncAction<In> : IAction<In>
    {
        /// <summary>
        /// original proc
        /// </summary>
        private readonly IAction<In> act;

        /// <summary>
        /// threadsafe-lock
        /// </summary>
        private readonly Object lck;

        /// <summary>
        /// Proc that is threadsafe.
        /// </summary>
        /// <param name="prc">proc to make threadsafe</param>
        public SyncAction(IAction<In> prc) : this(prc, prc)
        { }

        /// <summary>
        /// Proc that is threadsafe.
        /// </summary>
        /// <param name="prc">proc to make threadsafe</param>
        /// <param name="lck">object to lock threadsafe</param>
        public SyncAction(IAction<In> prc, object lck)
        {
            this.act = prc;
            this.lck = lck;
        }

        /// <summary>
        /// Execute procedure with given input.
        /// </summary>
        /// <param name="input"></param>
        public void Invoke(In input)
        {
            lock (this.lck)
            {
                this.act.Invoke(input);
            }
        }

    }

    /// <summary>
    /// Action that is threadsafe.
    /// </summary>
    public sealed class SyncAction : IAction
    {
        /// <summary>
        /// original proc
        /// </summary>
        private readonly IAction proc;

        /// <summary>
        /// threadsafe-lock
        /// </summary>
        private readonly Object lck;

        /// <summary>
        /// Proc that is threadsafe.
        /// </summary>
        /// <param name="prc">proc to make threadsafe</param>
        public SyncAction(IAction prc) : this(prc, prc)
        { }

        /// <summary>
        /// Proc that is threadsafe.
        /// </summary>
        /// <param name="prc">proc to make threadsafe</param>
        /// <param name="lck">object to lock threadsafe</param>
        public SyncAction(IAction prc, object lck)
        {
            this.proc = prc;
            this.lck = lck;
        }

        /// <summary>
        /// Execute procedure with given input.
        /// </summary>
        public void Invoke()
        {
            lock (this.lck)
            {
                this.proc.Invoke();
            }
        }

        /// <summary>
        /// Proc that is threadsafe.
        /// </summary>
        /// <param name="act">proc to make threadsafe</param>
        public static IAction<In> _<In>(IAction<In> act)
            => new SyncAction<In>(act);

        /// <summary>
        /// Proc that is threadsafe.
        /// </summary>
        /// <param name="act">proc to make threadsafe</param>
        /// <param name="lck">object to lock threadsafe</param>
        public static IAction<In> _<In>(IAction<In> act, object lck)
            => new SyncAction<In>(act, lck);
    }
}
