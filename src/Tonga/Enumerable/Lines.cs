using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Tonga.Enumerable;

/// <summary>
/// Lines in a given text.
/// </summary>
public sealed class Lines(Func<string> source, bool skipEmpty = false) : IEnumerable<string>
{
    /// <summary>
    /// Lines in a given text.
    /// </summary>
    public Lines(string source, bool skipEmpty = false) : this(() => source, skipEmpty)
    { }

    /// <summary>
    /// Lines in a given text.
    /// </summary>
    public Lines(IText source, bool skipEmpty = false) : this(source.Str, skipEmpty)
    { }

    public IEnumerator<string> GetEnumerator()
    {
        using StringReader reader = new StringReader(source());
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            if(!(skipEmpty && String.IsNullOrEmpty(line)))
                yield return line;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}

public static partial class EnumerableSmarts
{
    public static IEnumerable<string> AsLines(this IText source, bool skipEmpty = false) =>
        new Lines(source, skipEmpty);

    /// <summary>
    /// Lines in a given text.
    /// </summary>
    public static IEnumerable<string> AsLines(this IText source) => new Lines(source);
}
