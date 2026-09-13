using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// Lines in a given text, read asynchronously.
/// </summary>
public sealed class Lines(Func<CancellationToken, ValueTask<TextReader>> source, bool skipEmpty = false) :
    IAsyncEnumerable<string>
{
    /// <summary>
    /// Lines in a given text.
    /// </summary>
    public Lines(Func<string> source, bool skipEmpty = false) : this(
        _ => new ValueTask<TextReader>(new StringReader(source())), skipEmpty
    )
    { }

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

    public async IAsyncEnumerator<string> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var reader = await source(cancellation);
        try
        {
            string line;
            while ((line = await reader.ReadLineAsync(cancellation)) != null)
            {
                if (!(skipEmpty && string.IsNullOrEmpty(line)))
                    yield return line;
            }
        }
        finally
        {
            reader.Dispose();
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// Lines in a given text, read asynchronously.
    /// </summary>
    public static IAsyncEnumerable<string> AsAsyncLines(this IText source) => new Lines(source);

    /// <summary>
    /// Lines in a given text, read asynchronously.
    /// </summary>
    public static IAsyncEnumerable<string> AsAsyncLines(this IText source, bool skipEmpty) =>
        new Lines(source, skipEmpty);

    /// <summary>
    /// Lines in a given <see cref="TextReader"/>, read asynchronously.
    /// </summary>
    public static IAsyncEnumerable<string> AsAsyncLines(this TextReader source, bool skipEmpty = false) =>
        new Lines(_ => new ValueTask<TextReader>(source), skipEmpty);
}
