

using System.Security.Cryptography;

namespace Tonga.IO
{
    /// <summary>
    /// Digest Envelope
    /// </summary>
    public abstract class DigestEnvelope(IConduit source, System.Func<HashAlgorithm> algorithmFactory) : IBytes
    {
        /// <summary>
        /// Digest
        /// </summary>
        public byte[] Bytes()
        {
            using var sha = algorithmFactory();
            return sha.ComputeHash(source.Stream());
        }
    }
}
