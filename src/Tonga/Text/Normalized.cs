

using System;
using System.Text.RegularExpressions;

namespace Tonga.Text;

/// <summary>
/// Normalized A <see cref="IText"/> (whitespaces replaced with one single space)
/// </summary>
public sealed class Normalized(IText text) : TextEnvelope(
    () => Regex.Replace(new Trimmed(text).Str(), "\\s+", " ")
)
{
    /// <summary>
    /// Normalized A <see cref="IText"/>  (whitespaces replaced with one single space)
    /// </summary>
    /// <param name="text">text to normalize</param>
    public Normalized(String text) : this(text.AsText())
    { }
}
