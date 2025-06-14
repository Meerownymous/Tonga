

using System;
using System.Net;
using System.Text;
using Tonga.Text;

namespace Tonga.IO;

/// <summary>
/// Decoded url from a string.
/// </summary>
public sealed class DecodedUrl(Func<string> source) : TextEnvelope(
    () => WebUtility.UrlDecode(source())
)
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="url">url as string</param>
    public DecodedUrl(String url) : this(url, Encoding.UTF8)
    { }

    /// <summary>
    /// Decoded url from a string.
    /// </summary>
    /// <param name="url">url as string</param>
    /// <param name="enc">encoding of the string</param>
    public DecodedUrl(String url, Encoding enc) : this(new AsText(url, enc))
    { }

    /// <summary>
    /// Decoded url from a string.
    /// </summary>
    /// <param name="url">url as text</param>
    public DecodedUrl(IText url) : this(url.Str)
    { }
}
