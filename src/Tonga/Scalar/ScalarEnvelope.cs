

using System;

namespace Tonga.Scalar
{
    /// <summary>
    /// Envelope for scalars.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ScalarEnvelope<T> : IScalar<T>
    {
        private readonly Func<T> result;

        /// <summary>
        /// Envelope for scalars.
        /// </summary>
        public ScalarEnvelope(ScalarEnvelope<T> result) : this(result.Value)
        { }

        /// <summary>
        /// Envelope for scalars.
        /// </summary>
        public ScalarEnvelope(Func<T> result)
        {
            this.result = result;
        }

        /// <summary>
        /// Get the result.
        /// </summary>
        /// <returns>the result</returns>
        public T Value()
        {
            return result();
        }
    }
}
