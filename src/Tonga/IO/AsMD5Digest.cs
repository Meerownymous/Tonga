

using System.Security.Cryptography;

namespace Tonga.IO;

/// <summary>
/// MD5 checksum calculation
/// </summary>
public sealed class AsMD5Digest(IConduit source) : DigestEnvelope(source, MD5.Create);
