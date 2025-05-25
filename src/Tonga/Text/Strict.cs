

using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.Text;

/// <summary>
/// A strict text which can only be one of the specified valid texts.
/// </summary>
public sealed class Strict(IText candidate, IEnumerable<IText> valid, Func<StringComparison> stringComparer) : TextEnvelope(
    () => {
        var result = false;
        var str = candidate.Str();
        foreach (var txt in valid)
        {
            if (txt.Str().Equals(str, stringComparer()))
            {
                result = true;
                break;
            }
        }
        if (!result)
        {
            throw new ArgumentException($"'{str}' is not valid here - expected: {new Joined(", ", valid).Str()}");
        }
        return str;
    }
)
{
    /// <summary>
    /// A strict text which can only be one of the specified valid texts, ignoring case.
    /// </summary>
    /// <param name="candidate">The canidate to check for valid texts</param>
    /// <param name="valid">The valid texts</param>
    public Strict(string candidate, params string[] valid) : this(
        candidate,
        AsEnumerable._(valid)
    )
    { }

    /// <summary>
    /// A strict text which can only be one of the specified valid texts, ignoring case.
    /// </summary>
    /// <param name="candidate">The canidate to check for valid texts</param>
    /// <param name="valid">The valid texts</param>
    public Strict(string candidate, IEnumerable<string> valid) : this(
        candidate,
        true,
        valid
    )
    { }

    /// <summary>
    /// A strict text which can only be one of the specified valid texts.
    /// </summary>
    public Strict(string candidate, bool ignoreCase, params string[] valid) : this(
        candidate, ignoreCase, valid.AsEnumerable()
    )
    { }

    /// <summary>
    /// A strict text which can only be one of the specified valid texts.
    /// </summary>
    public Strict(string candidate, bool ignoreCase, IEnumerable<string> valid) : this(
        candidate.AsText(),
        ignoreCase,
        valid.AsMapped(str => str.AsText())
    )
    { }

    /// <summary>
    /// A strict text which can only be one of the specified valid texts, ignoring case.
    /// </summary>
    /// <param name="candidate">The canidate to check for valid texts</param>
    /// <param name="valid">The valid texts</param>
    public Strict(IText candidate, params string[] valid) : this(
        candidate, true, valid
    )
    { }

    /// <summary>
    /// A strict text which can only be one of the specified valid texts.
    /// </summary>
    /// <param name="candidate">The canidate to check for valid texts</param>
    /// <param name="ignoreCase">Ignore case in the canidate and valid texts</param>
    /// <param name="valid">The valid texts</param>
    public Strict(IText candidate, bool ignoreCase, params string[] valid) : this(
        candidate,
        ignoreCase,
        valid.AsMapped(str => str.AsText())
    )
    { }

    /// <summary>
    /// A strict text which can only be one of the specified valid texts, ignoring case.
    /// </summary>
    /// <param name="candidate">The canidate to check for valid texts</param>
    /// <param name="valid">The valid texts</param>
    public Strict(IText candidate, params IText[] valid) : this(candidate, true, valid)
    { }

    /// <summary>
    /// A strict text which can only be one of the specified valid texts.
    /// </summary>
    /// <param name="candidate">The canidate to check for valid texts</param>
    /// <param name="ignoreCase">Ignore case in the canidate and valid texts</param>
    /// <param name="valid">The valid texts</param>
    public Strict(IText candidate, bool ignoreCase, params IText[] valid) : this(
        candidate,
        ignoreCase,
        valid.AsEnumerable()
    )
    { }

    /// <summary>
    /// A strict text which can only be one of the specified valid texts, ignoring case.
    /// </summary>
    /// <param name="candidate">The canidate to check for valid texts</param>
    /// <param name="valid">The valid texts</param>
    public Strict(IText candidate, IEnumerable<IText> valid) : this(
        candidate,
        true,
        valid
    )
    { }

    /// <summary>
    /// A strict text which can only be one of the specified valid texts.
    /// </summary>
    /// <param name="candidate">The canidate to check for valid texts</param>
    /// <param name="ignoreCase">Ignore case in the canidate and valid texts</param>
    /// <param name="valid">The valid texts</param>
    public Strict(IText candidate, bool ignoreCase, IEnumerable<IText> valid) : this(
        candidate,
        valid,
        () => ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal
    )
    { }
}

public static partial class TextSmarts
{
    public static IText AsStrict(this IText candidate, IEnumerable<IText> valid, StringComparison stringComparer)
        => new Strict(candidate, valid, () => stringComparer);

    public static IText AsStrict(this IText candidate, IEnumerable<IText> valid, Func<StringComparison> stringComparer)
        => new Strict(candidate, valid, stringComparer);

    /// <summary>
    /// A strict text which can only be one of the specified valid texts, ignoring case.
    /// </summary>
    /// <param name="candidate">The canidate to check for valid texts</param>
    /// <param name="valid">The valid texts</param>
    public static IText AsStrict(this string candidate, params string[] valid) =>
        new Strict(candidate, valid);

    /// <summary>
    /// A strict text which can only be one of the specified valid texts, ignoring case.
    /// </summary>
    /// <param name="candidate">The canidate to check for valid texts</param>
    /// <param name="valid">The valid texts</param>
    public static IText AsStrict(this string candidate, IEnumerable<string> valid) =>
        new Strict(candidate, valid);

    /// <summary>
    /// A strict text which can only be one of the specified valid texts.
    /// </summary>
    public static IText AsStrict(this string candidate, bool ignoreCase, params string[] valid) =>
        new Strict(candidate, ignoreCase, valid);

    /// <summary>
    /// A strict text which can only be one of the specified valid texts.
    /// </summary>
    public static IText AsStrict(this string candidate, bool ignoreCase, IEnumerable<string> valid) =>
        new Strict(candidate, ignoreCase, valid);

    /// <summary>
    /// A strict text which can only be one of the specified valid texts, ignoring case.
    /// </summary>
    /// <param name="candidate">The canidate to check for valid texts</param>
    /// <param name="valid">The valid texts</param>
    public static IText AsStrict(this IText candidate, params string[] valid) =>
        new Strict(candidate, valid);

    /// <summary>
    /// A strict text which can only be one of the specified valid texts.
    /// </summary>
    /// <param name="candidate">The canidate to check for valid texts</param>
    /// <param name="ignoreCase">Ignore case in the canidate and valid texts</param>
    /// <param name="valid">The valid texts</param>
    public static IText AsStrict(this IText candidate, bool ignoreCase, params string[] valid) =>
        new Strict(candidate, ignoreCase, valid);

    /// <summary>
    /// A strict text which can only be one of the specified valid texts, ignoring case.
    /// </summary>
    /// <param name="candidate">The canidate to check for valid texts</param>
    /// <param name="valid">The valid texts</param>
    public static IText AsStrict(this IText candidate, params IText[] valid) =>
        new Strict(candidate, valid);

    /// <summary>
    /// A strict text which can only be one of the specified valid texts.
    /// </summary>
    /// <param name="candidate">The canidate to check for valid texts</param>
    /// <param name="ignoreCase">Ignore case in the canidate and valid texts</param>
    /// <param name="valid">The valid texts</param>
    public static IText AsStrict(this IText candidate, bool ignoreCase, params IText[] valid) =>
        new Strict(candidate, ignoreCase, valid);

    /// <summary>
    /// A strict text which can only be one of the specified valid texts, ignoring case.
    /// </summary>
    /// <param name="candidate">The canidate to check for valid texts</param>
    /// <param name="valid">The valid texts</param>
    public static IText AsStrict(this IText candidate, IEnumerable<IText> valid) =>
        new Strict(candidate, valid);

    /// <summary>
    /// A strict text which can only be one of the specified valid texts.
    /// </summary>
    /// <param name="candidate">The canidate to check for valid texts</param>
    /// <param name="ignoreCase">Ignore case in the canidate and valid texts</param>
    /// <param name="valid">The valid texts</param>
    public static IText AsStrict(this IText candidate, bool ignoreCase, IEnumerable<IText> valid) =>
        new Strict(candidate, ignoreCase, valid);
}
