

using System;
using System.Net;
using System.Text;
using Tonga.Text;

namespace Tonga.IO;

/// <summary>
/// Decoded url from a string.
/// </summary>
public sealed class DecodedUrl(TextMorph source) : TextEnvelope(
    () => WebUtility.UrlDecode(source.Str())
)
{
    /// <summary>
    /// Decoded url from a string.
    /// </summary>
    /// <param name="url">url as text</param>
    public DecodedUrl(IText url) : this(new TextMorph(url))
    { }
}
