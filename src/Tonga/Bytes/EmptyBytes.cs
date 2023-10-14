

namespace Tonga.Bytes
{
    /// <summary>
    /// Bytes without data.
    /// </summary>
    public sealed class EmptyBytes : IBytes
    {
        /// <summary>
        /// Bytes without data.
        /// </summary>        
        public EmptyBytes()
        { }

        /// <summary>
        /// Get the content as byte array.
        /// </summary>
        /// <returns>content as byte array</returns>
        public byte[] AsBytes()
        {
            return new byte[] { };
        }
    }
}
