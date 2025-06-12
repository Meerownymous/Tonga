

using System.Text.RegularExpressions;
using Tonga.Fact;
using Tonga.Scalar;

namespace Tonga.Text;

/// <summary>
/// Tells if a text starts with a given content.
/// </summary>
public sealed class StartsWith(IText text, IText start) : FactEnvelope(() =>
{
    var regex = new Regex("^" + Regex.Escape(start.Str()));
    return regex.IsMatch(text.Str());
})
{
    /// <summary>
    /// Tells if a <see cref="IText"/> starts with a given <see cref="string"/>
    /// </summary>
    public StartsWith(IText text, string start) : this(
        text,
        start.AsText()
    )
    { }
}

public static partial class TextSmarts
{
    /// <summary>
    /// Tells if a <see cref="IText"/> starts with a given <see cref="string"/>
    /// </summary>
    public static IFact AsStartsWith(this IText text, string start) =>
        new StartsWith(text, start);

    /// <summary>
    /// Tells if a <see cref="IText"/> starts with a given <see cref="string"/>
    /// </summary>
    public static IFact AsStartsWith(this IText text, IText start) =>
        new StartsWith(text, start);
}
