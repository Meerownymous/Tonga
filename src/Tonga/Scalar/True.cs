

using System;

namespace Tonga.Scalar
{
    /// <summary>
    /// Logical truth.
    /// </summary>
    public sealed class True : IScalar<bool>
    {
        /// <summary>
        /// Logical truth.
        /// </summary>
        public True()
        { }

        /// <summary>
        /// True or not.
        /// </summary>
        /// <returns>True or not</returns>
        public Boolean Value()
        {
            return true;
        }
    }
}
