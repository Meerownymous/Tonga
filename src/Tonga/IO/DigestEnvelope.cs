

using System.Security.Cryptography;

namespace Tonga.IO
{
    /// <summary>
    /// Digest Envelope
    /// </summary>
    public abstract class DigestEnvelope : IBytes
    {
        private readonly IInput source;
        private readonly IScalar<HashAlgorithm> algorithmFactory;

        /// <summary>
        /// Digest Envelope of Input
        /// </summary>
        /// <param name="source">Input</param>
        /// <param name="algorithmFactory">Factory to create Hash Algorithm</param>
        public DigestEnvelope(IInput source, IScalar<HashAlgorithm> algorithmFactory)
        {
            this.source = source;
            this.algorithmFactory = algorithmFactory;
        }

        /// <summary>
        /// Digest
        /// </summary>
        public byte[] AsBytes()
        {
            using (var sha = algorithmFactory.Value())
            {
                return sha.ComputeHash(source.Stream());
            }
        }
    }
}
