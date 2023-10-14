

using System;
using Tonga.Map;

namespace Tonga.Func
{
    /// <summary>
    /// Conditional Action as part of an <see cref="ActionSwitch{In}"/>
    /// </summary>
    public sealed class ActionIf<In> : KvpEnvelope<Action<In>>
    {
        /// <summary>
        /// Conditional Action as part of an <see cref="ActionSwitch{In}"/>
        /// </summary>
        public ActionIf(string condition, Action<In> consequence) : base(
            new KvpOf<Action<In>>(condition, consequence)
        )
        { }
    }

    /// <summary>
    /// Conditional Action as part of an <see cref="ActionSwitch{In1, In2}"/>
    /// </summary>
    public sealed class ActionIf<In1, In2> : KvpEnvelope<Action<In1, In2>>
    {
        /// <summary>
        /// Conditional Action as part of an <see cref="ActionSwitch{In1, In2}"/>
        /// </summary>
        public ActionIf(string condition, Action<In1, In2> consequence) : base(
            new KvpOf<Action<In1, In2>>(condition, consequence)
        )
        { }
    }

    public static class ActionIf
    {
        /// <summary>
        /// ctor
        /// </summary>
        public static IKvp<Action<T>> New<T>(string condition, Action<T> consequence) => new ActionIf<T>(condition, consequence);

        /// <summary>
        /// ctor
        /// </summary>
        public static IKvp<Action<In1, In2>> New<In1, In2>(string condition, Action<In1, In2> consequence) => new ActionIf<In1, In2>(condition, consequence);
    }
}
