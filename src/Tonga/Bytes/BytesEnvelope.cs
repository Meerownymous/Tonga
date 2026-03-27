using System;

namespace Tonga.Bytes;

public abstract class BytesEnvelope(Func<byte[]> raw) : IBytes
{
    public byte[] Raw() => raw();
}
