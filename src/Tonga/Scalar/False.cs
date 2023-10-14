

using System;

namespace Tonga.Scalar
{
    /// <summary>
    /// Logical false.
    /// </summary>
    public sealed class False : IScalar<Boolean>
    {
        /// <summary>
        /// Logical false.
        /// </summary>
        public False()
        { }

        /// <summary>
        /// Get the value.
        /// </summary>
        /// <returns>the value</returns>
        public Boolean Value()
        {
            return false;
        }
    }
}
