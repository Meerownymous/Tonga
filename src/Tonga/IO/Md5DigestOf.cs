

using System.Security.Cryptography;
using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// MD5 checksum calculation
    /// </summary>
    public sealed class Md5DigestOf : DigestEnvelope
    {

        /// <summary>
        /// MD5 checksum calculation of IInput.
        /// </summary>
        /// <param name="source">Input</param>
        public Md5DigestOf(IInput source) : base(
            source,
            AsScalar._<HashAlgorithm>(MD5.Create)
        )
        { }
    }
}
