

using System;
using System.IO;
using Tonga.Bytes;

namespace Tonga.IO;

/// <summary>
/// <see cref="IConduit"/> which will be copied to another conduit while reading.
/// </summary>
public sealed class TeeOnReadConduit(IConduit source, IConduit output) : IConduit
{
    /// <summary>
    /// <see cref="IConduit"/> out of a file <see cref="Uri"/> which will be copied to <see cref="IConduit"/> while reading.
    /// </summary>
    public TeeOnReadConduit(Uri input, Uri output) : this(
        new AsConduit(input), new AsConduit(output)
    )
    { }

    /// <summary>
    /// <see cref="IConduit"/> out of a <see cref="string"/> which will be copied to <see cref="IConduit"/> while reading.
    /// </summary>
    public TeeOnReadConduit(String input, Uri file) : this(
        new BytesAsConduit(input), new AsConduit(file)
    )
    { }

    /// <summary>
    /// <see cref="IConduit"/> out of a <see cref="byte"/> array which will be copied to <see cref="IConduit"/> while reading.
    /// </summary>
    public TeeOnReadConduit(byte[] input, Uri file) : this(
        new BytesAsConduit(input), new AsConduit(file)
    )
    { }

    /// <summary>
    /// <see cref="IConduit"/> out of a <see cref="string"/>  which will be copied to <see cref="IConduit"/> while reading.
    /// </summary>
    public TeeOnReadConduit(String input, IConduit output) : this(
        new BytesAsConduit(input),
        output
    )
    { }

    /// <summary>
    /// Get the stream
    /// </summary>
    /// <returns></returns>
    public Stream Stream() =>
        new TeeInputStream(source.Stream(), output.Stream());
}
