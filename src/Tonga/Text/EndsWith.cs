using System.Text.RegularExpressions;
using Tonga.Fact;

namespace Tonga.Text
{
    /// <summary>
    /// Checks if a text ends with a given content.
    /// </summary>
    public sealed class EndsWith(IText text, IText tail) : FactEnvelope(
        new AsFact(() =>
            new Regex(Regex.Escape(tail.AsString()) + "$").IsMatch(text.AsString())
        )
    )
    {
        /// <summary>
        /// Checks if a <see cref="IText"/> ends with a given <see cref="string"/>
        /// </summary>
        /// <param name="text">Text to test</param>
        /// <param name="tail">Ending content to use in the test</param>
        public EndsWith(IText text, string tail) : this(
            text,
            AsText._(tail)
        )
        { }
    }
}
