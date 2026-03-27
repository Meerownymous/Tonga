

using System;
using System.Globalization;
using System.Linq;
using Tonga.Enumerable;

namespace Tonga.Text;

/// <summary>
/// A <see cref="IText"/> formatted with arguments.
/// Use C# formatting syntax: new FormattedText("{0} is {1}", "OOP", "great").AsString() will be "OOP is great"
/// </summary>
public sealed class Formatted(
    TextMorph ptn,
    CultureInfo locale,
    Func<object[]> arguments
) : TextEnvelope(
    () => String.Format(locale, ptn.Str(), arguments())
)
{
    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern to put arguments in</param>
    /// <param name="arguments">arguments to apply</param>
    public Formatted(TextMorph ptn, params string[] arguments) : this(
        ptn,
        CultureInfo.InvariantCulture,
        arguments
    )
    { }

    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern to put arguments in</param>
    /// <param name="arguments">arguments to apply</param>
    public Formatted(TextMorph ptn, params IText[] arguments) : this(
        ptn,
        CultureInfo.InvariantCulture,
        arguments.AsMapped(txt => txt.Str())
    )
    { }

    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern to put arguments in</param>
    /// <param name="arguments">arguments to apply</param>
    public Formatted(TextMorph ptn, params TextMorph[] arguments) : this(
        ptn,
        CultureInfo.InvariantCulture,
        arguments
    )
    { }

    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern</param>
    /// <param name="local">CultureInfo</param>
    /// <param name="arguments">arguments to apply</param>
    public Formatted(TextMorph ptn, CultureInfo local, params object[] arguments) : this(
        ptn, local, () => arguments
    )
    { }

    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern</param>
    /// <param name="local">CultureInfo</param>
    /// <param name="arguments">arguments to apply</param>
    public Formatted(IText ptn, CultureInfo local, params object[] arguments) : this(
        new TextMorph(ptn), local, () => arguments
    )
    { }

    /// <summary>
    ///  A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern to put arguments in</param>
    /// <param name="locale">a specific culture</param>
    /// <param name="arguments">arguments as <see cref="IText"/> to apply</param>
    public Formatted(TextMorph ptn, CultureInfo locale, params TextMorph[] arguments) : this(
        ptn,
        locale,
        () => arguments.AsMapped(txt => txt.Str()).ToArray()
    )
    { }
}


public static partial class TextSmarts
{
    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern to put arguments in</param>
    /// <param name="arguments">arguments to apply</param>
    public static Formatted AsFormatted(this TextMorph ptn, params TextMorph[] arguments) =>
        new(ptn, arguments);

    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern to put arguments in</param>
    /// <param name="arguments">arguments to apply</param>
    public static Formatted AsFormatted(this TextMorph ptn, params string[] arguments) =>
        new(ptn, arguments);

    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern</param>
    /// <param name="local">CultureInfo</param>
    /// <param name="arguments">arguments to apply</param>
    public static Formatted AsFormatted(this IText ptn, CultureInfo local, params object[] arguments) =>
        new(ptn, local, arguments);

    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern to put arguments in</param>
    /// <param name="locale">a specific culture</param>
    /// <param name="arguments">arguments to apply</param>
    public static Formatted AsFormatted(this String ptn, CultureInfo locale, params string[] arguments) =>
        new(ptn, locale, arguments);

    /// <summary>
    ///  A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern to put arguments in</param>
    /// <param name="locale">a specific culture</param>
    /// <param name="arguments">arguments as <see cref="IText"/> to apply</param>
    public static Formatted AsFormatted(this string ptn, CultureInfo locale, params TextMorph[] arguments) =>
        new(ptn, locale, arguments);

    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern to put arguments in</param>
    /// <param name="locale">a specific culture</param>
    /// <param name="arguments">arguments to apply</param>
    public static Formatted AsFormatted(this IText ptn, CultureInfo locale, IScalar<object[]> arguments) =>
        new(ptn, locale, arguments);

    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern to put arguments in</param>
    /// <param name="locale">a specific culture</param>
    /// <param name="arguments">arguments to apply</param>
    public static Formatted AsFormatted(this IText ptn, CultureInfo locale, Func<object[]> arguments) =>
        new(ptn, locale, arguments);
}
