

using System;

namespace Tonga.Scalar
{
    /// <summary>
    /// Logical negative.
    /// </summary>
    public sealed class Not : ScalarEnvelope<Boolean>
    {
        /// <summary>
        /// Logical negative.
        /// </summary>
        /// <param name="scalar">scalar to negate</param>
        public Not(IScalar<Boolean> scalar) : base(() => !scalar.Value())
        { }
    }
}
