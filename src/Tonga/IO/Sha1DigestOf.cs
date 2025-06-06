

using System.Security.Cryptography;
using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// SHA-1 checksum calculation
    /// </summary>
    public sealed class Sha1DigestOf : DigestEnvelope
    {

        /// <summary>
        /// SHA-1 checksum calculation of IInput.
        /// </summary>
        /// <param name="source">Input</param>
        public Sha1DigestOf(IConduit source) : base(
            source, AsScalar._<HashAlgorithm>(SHA1.Create)
        )
        { }
    }
}
