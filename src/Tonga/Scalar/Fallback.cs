

using System;

namespace Tonga.Scalar
{
    /// <summary>
    /// Scalar which calls a fallback function if Value() fails.
    /// </summary>
    /// <typeparam name="Out">Type of output value</typeparam>
    public class Fallback<Out> : ScalarEnvelope<Out>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="origin">Scalar which can fail</param>
        /// <param name="fallback">Fallback if scalar fails</param>
        public Fallback(IScalar<Out> origin, Out fallback) : this(
            origin,
            (ex) => fallback)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="origin">Scalar which can fail</param>
        /// <param name="fallback">Fallback if scalar fails</param>
        public Fallback(IScalar<Out> origin, Func<Out> fallback) : this(
            origin,
            (ex) => fallback.Invoke())
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="origin">scalar which can fail</param>
        /// <param name="fallback">fallback to apply when fails</param>
        public Fallback(IScalar<Out> origin, Func<Exception, Out> fallback)
            : base(() =>
            {
                try
                {
                    return origin.Value();
                }
                catch (Exception ex)
                {
                    return fallback.Invoke(ex);
                }
            })
        { }
    }

    public static class Fallback
    {

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="origin">Scalar which can fail</param>
        /// <param name="fallback">Fallback if scalar fails</param>
        public static Fallback<Out> New<Out>(IScalar<Out> origin, Out fallback)
            => new Fallback<Out>(origin, fallback);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="origin">Scalar which can fail</param>
        /// <param name="fallback">Fallback if scalar fails</param>
        public static Fallback<Out> New<Out>(IScalar<Out> origin, Func<Out> fallback)
            => new Fallback<Out>(origin, fallback);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="origin">scalar which can fail</param>
        /// <param name="fallback">fallback to apply when fails</param>
        public static Fallback<Out> New<Out>(IScalar<Out> origin, Func<Exception, Out> fallback)
            => new Fallback<Out>(origin, fallback);
    }
}
