using System;
namespace Tonga.Text;

/// <summary>
/// Text which is sticky - it remembers its output instead of regenerating every time.
/// </summary>
public sealed class Sticky(Func<string> origin) : IText
{
    private readonly Lazy<string> result = new(origin);

    /// <summary>
    /// Text which is sticky - it remembers its output instead of regenerating every time.
    /// </summary>
    public Sticky(IText origin) : this(origin.Str){ }

    public string Str() => result.Value;
}

public static partial class TextSmarts
{
    /// <summary>
    /// Text which is sticky - it remembers its output instead of regenerating every time.
    /// </summary>
    public static IText AsSticky(this IText origin) => new Sticky(origin);

    /// <summary>
    /// Text which is sticky - it remembers its output instead of regenerating every time.
    /// </summary>
    public static IText AsSticky(this Func<string> origin) => new Sticky(origin);
}

