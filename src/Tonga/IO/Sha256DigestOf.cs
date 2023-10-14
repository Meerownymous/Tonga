

using System.Security.Cryptography;
using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// SHA-256 checksum calculation
    /// </summary>
    public sealed class Sha256DigestOf : DigestEnvelope
    {
        /// <summary>
        /// SHA-256 checksum calculation of IInput.
        /// </summary>
        /// <param name="source">Input</param>
        public Sha256DigestOf(IInput source) :
            base(source, new Live<HashAlgorithm>(() => new SHA256CryptoServiceProvider()))
        { }
    }
}
