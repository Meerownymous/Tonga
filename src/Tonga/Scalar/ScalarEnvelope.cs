

using System;

namespace Tonga.Scalar
{
    /// <summary>
    /// Envelope for scalars.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ScalarEnvelope<T>(Func<T> origin) : IScalar<T>
    {
        /// <summary>
        /// Envelope for scalars.
        /// </summary>
        public ScalarEnvelope(IScalar<T> origin) : this(() => origin.Value())
        { }

        /// <summary>
        /// Get the result.
        /// </summary>
        /// <returns>the result</returns>
        public T Value() => origin();
    }
}
