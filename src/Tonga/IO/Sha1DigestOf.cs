

using System.Security.Cryptography;
using Tonga.Scalar;

namespace Tonga.IO;

/// <summary>
/// SHA-1 checksum calculation
/// </summary>
public sealed class Sha1DigestOf(IConduit source) : DigestEnvelope(source, SHA1.Create);
