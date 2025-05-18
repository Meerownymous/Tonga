

using System;
using System.IO;
using Tonga.IO;

namespace Tonga;

/// <summary>
/// Input.
///
/// <para>Here is for example how <see cref="IConduit"/>  can be used
/// in order to read the content of a text file:</para>
///
/// <code>String content = new BytesAsText(
/// new InputAsBytes(
///    new InputOf(new Uri("file:///C:/tmp/names.txt")
///   )
/// ).asString();</code>
///
/// <para>Here <see cref="AsConduit"/>
/// implements <see cref="IConduit"/> and behaves like
/// one, providing read-only access to the encapsulated <see cref="Uri"/> pointing to a file.</para>
/// </summary>
public interface IConduit
{
    /// <summary>
    /// Get the stream.
    /// </summary>
    /// <returns>the stream</returns>
    Stream Stream();
}
