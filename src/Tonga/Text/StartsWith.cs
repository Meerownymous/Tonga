

using System.Text.RegularExpressions;
using Tonga.Scalar;

namespace Tonga.Text
{
    /// <summary>
    /// Checks if a text starts with a given content.
    /// </summary>
    public sealed class StartsWith : IScalar<bool>
    {
        private readonly ScalarOf<bool> result;

        /// <summary>
        /// Checks if a <see cref="IText"/> starts with a given <see cref="string"/>
        /// </summary>
        /// <param name="text">Text to test</param>
        /// <param name="start">Starting content to use in the test</param>
        public StartsWith(IText text, string start) : this(
            text,
            new LiveText(start)
        )
        { }

        /// <summary>
        /// Checks if a <see cref="IText"/> starts with a given <see cref="IText"/>
        /// </summary>
        /// <param name="text">Text to test</param>
        /// <param name="start">Starting content to use in the test</param>
        public StartsWith(IText text, IText start)
        {
            this.result =
                new ScalarOf<bool>(() =>
                {
                    var regex = new Regex("^" + Regex.Escape(start.AsString()));
                    return regex.IsMatch(text.AsString());
                });
        }

        /// <summary>
        /// Gets the result
        /// </summary>
        /// <returns>The result</returns>
        public bool Value()
        {
            return this.result.Value();
        }
    }
}
