

using System.Text.RegularExpressions;
using Tonga.Scalar;

namespace Tonga.Text
{
    /// <summary>
    /// Checks if a text starts with a given content.
    /// </summary>
    public sealed class StartsWith : ScalarEnvelope<bool>
    {
        /// <summary>
        /// Checks if a <see cref="IText"/> starts with a given <see cref="string"/>
        /// </summary>
        /// <param name="text">Text to test</param>
        /// <param name="start">Starting content to use in the test</param>
        public StartsWith(IText text, string start) : this(
            text,
            AsText._(start)
        )
        { }

        /// <summary>
        /// Checks if a <see cref="IText"/> starts with a given <see cref="IText"/>
        /// </summary>
        /// <param name="text">Text to test</param>
        /// <param name="start">Starting content to use in the test</param>
        public StartsWith(IText text, IText start) : base(() =>
        {
            var regex = new Regex("^" + Regex.Escape(start.AsString()));
            return regex.IsMatch(text.AsString());
        })
        { }
    }
}
