

using System;
using System.Globalization;
using System.Linq;
using Tonga.Enumerable;
using Tonga.Scalar;
using Tonga.Text;

namespace Tonga.Text;

/// <summary>
/// A <see cref="IText"/> formatted with arguments.
/// Use C# formatting syntax: new FormattedText("{0} is {1}", "OOP", "great").AsString() will be "OOP is great"
/// </summary>
public sealed class Formatted : TextEnvelope
{
    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern to put arguments in</param>
    /// <param name="arguments">arguments to apply</param>
    public Formatted(String ptn, params IText[] arguments) : this(
        ptn.AsText(),
        CultureInfo.InvariantCulture,
        () =>
            Mapped._(
                txt => txt.Str(),
                arguments
            ).ToArray()
    )
    {
    }

    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern to put arguments in</param>
    /// <param name="arguments">arguments to apply</param>
    public Formatted(String ptn, params object[] arguments) : this(
        ptn.AsText(),
        CultureInfo.InvariantCulture,
        arguments.AsMapped(a => a.ToString())
    )
    {
    }

    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern to put arguments in</param>
    /// <param name="arguments">arguments to apply</param>
    public Formatted(IText ptn, params object[] arguments) : this(
        ptn,
        CultureInfo.InvariantCulture,
        arguments
    )
    {
    }

    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern</param>
    /// <param name="local">CultureInfo</param>
    /// <param name="arguments">arguments to apply</param>
    public Formatted(IText ptn, CultureInfo local, params object[] arguments) : this(
        ptn, local, () => arguments
    )
    {
    }

    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern to put arguments in</param>
    /// <param name="locale">a specific culture</param>
    /// <param name="arguments">arguments to apply</param>
    public Formatted(String ptn, CultureInfo locale, params object[] arguments) : this(
        ptn.AsText(), locale, arguments)
    {
    }

    /// <summary>
    ///  A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern to put arguments in</param>
    /// <param name="locale">a specific culture</param>
    /// <param name="arguments">arguments as <see cref="IText"/> to apply</param>
    public Formatted(string ptn, CultureInfo locale, params IText[] arguments) : this(
        ptn.AsText(),
        locale,
        () =>
        {
            object[] strings = new object[Length._(arguments).Value()];
            for (int i = 0; i < arguments.Length; i++)
            {
                strings[i] = arguments[i].Str();
            }

            return strings;
        }
    )
    {
    }

    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern to put arguments in</param>
    /// <param name="locale">a specific culture</param>
    /// <param name="arguments">arguments to apply</param>
    public Formatted(
        IText ptn,
        CultureInfo locale,
        IScalar<object[]> arguments
    ) : this(
        ptn,
        locale,
        arguments.Value
    )
    {
    }

    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern to put arguments in</param>
    /// <param name="locale">a specific culture</param>
    /// <param name="arguments">arguments to apply</param>
    public Formatted(
        IText ptn,
        CultureInfo locale,
        Func<object[]> arguments
    ) : base(
        new AsText(
            () => String.Format(locale, ptn.Str(), arguments())
        )
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
    public static Formatted AsFormatted(this String ptn, params IText[] arguments) =>
        new(ptn, arguments);

    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern to put arguments in</param>
    /// <param name="arguments">arguments to apply</param>
    public static Formatted AsFormatted(this String ptn, params object[] arguments) =>
        new(ptn, arguments);


    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern to put arguments in</param>
    /// <param name="arguments">arguments to apply</param>
    public static Formatted AsFormatted(this IText ptn, params object[] arguments) =>
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
    public static Formatted AsFormatted(this String ptn, CultureInfo locale, params object[] arguments) =>
        new(ptn, locale, arguments);

    /// <summary>
    ///  A <see cref="IText"/> formatted with arguments.
    /// </summary>
    /// <param name="ptn">pattern to put arguments in</param>
    /// <param name="locale">a specific culture</param>
    /// <param name="arguments">arguments as <see cref="IText"/> to apply</param>
    public static Formatted AsFormatted(this string ptn, CultureInfo locale, params IText[] arguments) =>
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
