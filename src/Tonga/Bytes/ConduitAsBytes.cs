

using System;
using System.IO;
using Tonga.IO;

namespace Tonga.Bytes;

/// <summary>
/// Input as bytes. Disposes input.
/// </summary>
public sealed class ConduitAsBytes(IConduit src, int max = 16 << 10) : BytesEnvelope(() =>
    {
        var baos = new MemoryStream();

        using var source = src.Stream();
        using var stream = new TeeOnRead(source, baos).Stream();
        byte[] readBuffer = new byte[max];
        while (stream.Read(readBuffer, 0, readBuffer.Length) > 0)
        {
        }

        var output = baos.ToArray();
        return output;
    }
);

