

using System;

namespace Tonga
{
    /// <summary>
    /// A capsule for anything.
    /// </summary>
    /// <typeparam name="OutValue"></typeparam>
    public interface IScalar<OutValue>
    {
        /// <summary>
        /// Get the value.
        /// </summary>
        /// <returns>the value</returns>
        OutValue Value();
    }
}
