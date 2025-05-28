

using System.Security.Cryptography;
using Tonga.Scalar;

namespace Tonga.IO;

/// <summary>
/// SHA-256 checksum calculation
/// </summary>
public sealed class Sha256DigestOf(IConduit source) : DigestEnvelope(source, SHA256.Create);
