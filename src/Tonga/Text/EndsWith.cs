using System.Text.RegularExpressions;
using Tonga.Fact;

namespace Tonga.Text;

/// <summary>
/// Checks if a text ends with a given content.
/// </summary>
public sealed class EndsWith(IText text, IText tail) : FactEnvelope(
    () => new Regex(Regex.Escape(tail.Str()) + "$").IsMatch(text.Str())
)
{
    /// <summary>
    /// Checks if a <see cref="IText"/> ends with a given <see cref="string"/>
    /// </summary>
    /// <param name="text">Text to test</param>
    /// <param name="tail">Ending content to use in the test</param>
    public EndsWith(IText text, string tail) : this(
        text,
        tail.AsText()
    )
    { }
}

public static partial class TextSmarts
{
    /// <summary>
    /// Checks if a text ends with a given content.
    /// </summary>
    public static IFact EndsWith(this IText text, string tail) => new EndsWith(text, tail);

    /// <summary>
    /// Checks if a text ends with a given content.
    /// </summary>
    public static IFact EndsWith(this IText text, IText tail) => new EndsWith(text, tail);
}
