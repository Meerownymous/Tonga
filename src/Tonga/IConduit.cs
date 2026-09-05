using System;
using System.IO;
using Tonga.IO;

namespace Tonga;

/// <summary>
/// A source of a <see cref="Stream"/>.
///
/// <para>One interface covers reading and writing, because
/// <see cref="Stream"/> covers both. What a conduit permits is told by
/// the stream it hands out, through <see cref="Stream.CanRead"/> and
/// <see cref="Stream.CanWrite"/>.</para>
///
/// <para>Here is for example how a <see cref="IConduit"/> can be used
/// to read the content of a text file:</para>
///
/// <code>string content =
///     new AsConduit(new Uri("file:///C:/tmp/names.txt"))
///         .AsText()
///         .Str();</code>
///
/// <para><see cref="AsConduit"/> implements <see cref="IConduit"/> and
/// provides access to the encapsulated <see cref="Uri"/>.</para>
/// </summary>
public interface IConduit
{
    /// <summary>
    /// Get the stream.
    /// </summary>
    /// <returns>the stream</returns>
    Stream Stream();
}
