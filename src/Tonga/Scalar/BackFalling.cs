

using System;

namespace Tonga.Scalar
{
    /// <summary>
    /// Scalar which calls a fallback function if Value() fails.
    /// </summary>
    /// <typeparam name="Out">Type of output value</typeparam>
    public class BackFalling<Out> : ScalarEnvelope<Out>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="origin">Scalar which can fail</param>
        /// <param name="fallback">Fallback if scalar fails</param>
        public BackFalling(IScalar<Out> origin, Out fallback) : this(
            origin,
            _ => fallback
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="origin">Scalar which can fail</param>
        /// <param name="fallback">Fallback if scalar fails</param>
        public BackFalling(IScalar<Out> origin, Func<Out> fallback) : this(
            origin,
            _ => fallback.Invoke()
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="origin">scalar which can fail</param>
        /// <param name="fallback">fallback to apply when fails</param>
        public BackFalling(IScalar<Out> origin, Func<Exception, Out> fallback) : base(() =>
            {
                try
                {
                    return origin.Value();
                }
                catch (Exception ex)
                {
                    return fallback.Invoke(ex);
                }
            }
        )
        { }
    }

    public static partial class ScalarSmarts
    {

        /// <summary>
        /// ctor
        /// </summary>
        public static IScalar<Out> AsBackFalling<Out>(this IScalar<Out> origin, Out fallback)
            => new BackFalling<Out>(origin, fallback);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="origin">Scalar which can fail</param>
        /// <param name="fallback">Fallback if scalar fails</param>
        public static IScalar<Out> AsBackFalling<Out>(this IScalar<Out> origin, Func<Out> fallback)
            => new BackFalling<Out>(origin, fallback);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="origin">scalar which can fail</param>
        /// <param name="fallback">fallback to apply when fails</param>
        public static IScalar<Out> AsBackFalling<Out>(this IScalar<Out> origin, Func<Exception, Out> fallback)
            => new BackFalling<Out>(origin, fallback);
    }
}
