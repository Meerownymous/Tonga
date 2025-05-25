

using System;
using Tonga.Fact;
using Tonga.Scalar;

namespace Tonga.Text;

/// <summary> Check if a text contains a pattern </summary>
public sealed class Contains(
    Func<string> str,
    Func<string> strToSearch,
    Func<StringComparison> stringComparison
) : FactEnvelope(
    () => str().IndexOf(strToSearch(), stringComparison()) >= 0
)
{
    /// <summary> Checks if a text contains a pattern using strings </summary>
    /// <param name="inputStr"> text as string </param>
    /// <param name="patternStr"> pattern as string </param>
    /// <param name="ignoreCase"> Enables case sensitivity </param>
    public Contains(string inputStr, string patternStr, bool ignoreCase = false) : this(
        () => inputStr,
        () => patternStr,
        () => ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture
    )
    { }

    /// <summary> Checks if a text contains a pattern using IText </summary>
    /// <param name="inputText"> text as IText </param>
    /// <param name="patternText"> pattern as IText </param>
    /// <param name="ignoreCase"> Enables case sensitivity </param>
    public Contains(IText inputText, IText patternText, bool ignoreCase = false) : this(
        inputText.Str,
        patternText.Str,
        () => ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture
    )
    { }

    /// <summary> Checks if a text contains a pattern using IScalar </summary>
    /// <param name="text"> text as IScalar of string </param>
    /// <param name="textToSearch"> pattern as IScalar of string </param>
    public Contains(IScalar<string> text, IScalar<string> textToSearch) : this(
        text.Value,
        textToSearch.Value,
        () => StringComparison.CurrentCulture
    )
    { }

    /// <summary> Checks if a text contains a pattern using IScalar </summary>
    /// <param name="text"> text as IScalar of string </param>
    /// <param name="textToSearch"> pattern as IScalar of string </param>
    /// <param name="stringComparison"> Enables case sensitivity (as IScalar of bool) </param>
    public Contains(IScalar<string> text, IScalar<string> textToSearch, IScalar<StringComparison> stringComparison) : this(
        text.Value,
        textToSearch.Value,
        stringComparison.Value
    )
    { }
}

/// <summary> Check if a text contains a pattern </summary>
public static partial class TextSmarts
{
    /// <summary> Checks if a text contains a pattern using strings </summary>
    public static IFact AsContains(this string inputStr, string patternStr, bool ignoreCase = false) =>
        new Contains(inputStr, patternStr, ignoreCase
        );


    /// <summary> Checks if a text contains a pattern using IText </summary>
    /// <param name="inputText"> text as IText </param>
    /// <param name="patternText"> pattern as IText </param>
    /// <param name="ignoreCase"> Enables case sensitivity </param>
    public static IFact AsContains(this IText inputText, IText patternText, bool ignoreCase = false) =>
        new Contains(inputText, patternText, ignoreCase);

    /// <summary> Checks if a text contains a pattern using IScalar </summary>
    /// <param name="inputValue"> text as IScalar of string </param>
    /// <param name="pattern"> pattern as IScalar of string </param>
    public static IFact AsContains(this IScalar<string> inputValue, IScalar<string> pattern) =>
        new Contains(inputValue, pattern);

    /// <summary> Checks if a text contains a pattern using IScalar </summary>
    /// <param name="inputValue"> text as IScalar of string </param>
    /// <param name="pattern"> pattern as IScalar of string </param>
    /// <param name="stringComparison"> Enables case sensitivity (as IScalar of bool) </param>
    public static IFact AsContains(IScalar<string> inputValue, IScalar<string> pattern,
        IScalar<StringComparison> stringComparison) =>
        new Contains(inputValue, pattern, stringComparison);

    /// <summary> Checks if a text contains a pattern using IScalar </summary>
    /// <param name="inputValue"> text as IScalar of string </param>
    /// <param name="pattern"> pattern as IScalar of string </param>
    /// <param name="stringComparison"> Enables case sensitivity (as IScalar of bool) </param>
    public static IFact AsContains(Func<string> inputValue, Func<string> pattern, Func<StringComparison> stringComparison) =>
        new Contains(inputValue, pattern, stringComparison);
}
