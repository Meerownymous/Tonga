

using System;
using Tonga.Map;

namespace Tonga.Func
{
    /// <summary>
    /// Conditional Action as part of an <see cref="ActionSwitch{In}"/>
    /// </summary>
    public sealed class ActionIf<In>(string condition, Action<In> consequence) : PairEnvelope<string, Action<In>>(
        AsPair._(condition, consequence)
    );

    /// <summary>
    /// Conditional Action as part of an <see cref="ActionSwitch{In1, In2}"/>
    /// </summary>
    public sealed class ActionIf<In1, In2>(string condition, Action<In1, In2> consequence) :
        PairEnvelope<string, Action<In1, In2>>(
            AsPair._(condition, consequence)
        );

    public static class ActionIf
    {
        /// <summary>
        /// ctor
        /// </summary>
        public static IPair<string, Action<T>> _<T>(string condition, Action<T> consequence) => new ActionIf<T>(condition, consequence);

        /// <summary>
        /// ctor
        /// </summary>
        public static IPair<string, Action<In1, In2>> _<In1, In2>(string condition, Action<In1, In2> consequence) => new ActionIf<In1, In2>(condition, consequence);
    }
}
