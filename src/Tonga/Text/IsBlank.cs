using System;
using System.Linq;
using Tonga.Enumerable;
using Tonga.Fact;
using Tonga.Scalar;

namespace Tonga.Text;

/// <summary>
/// Checks if a text is whitespace.
/// </summary>
public sealed class IsBlank(Func<string> str) : FactEnvelope(() =>
    string.IsNullOrWhiteSpace(str())
)
{
    /// <summary>
    /// Checks if a A <see cref="string"/> is whitespace.
    /// </summary>
    public IsBlank(string str) : this(() => str)
    { }

    /// <summary>
    /// Checks if a A <see cref="string"/> is whitespace.
    /// </summary>
    public IsBlank(IText txt) : this(txt.Str)
    { }
}
