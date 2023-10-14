

using System.Text.RegularExpressions;
using Tonga.Scalar;

namespace Tonga.Text
{
    /// <summary>
    /// Checks if a text ends with a given content.
    /// </summary>
    public sealed class EndsWith : IScalar<bool>
    {
        private readonly ScalarOf<bool> result;

        /// <summary>
        /// Checks if a <see cref="IText"/> ends with a given <see cref="string"/>
        /// </summary>
        /// <param name="text">Text to test</param>
        /// <param name="tail">Ending content to use in the test</param>
        public EndsWith(IText text, string tail) : this(
            text,
            new LiveText(tail)
        )
        { }

        /// <summary>
        /// Checks if a <see cref="IText"/> ends with a given <see cref="IText"/>
        /// </summary>
        /// <param name="text">Text to test</param>
        /// <param name="tail">Ending content to use in the test</param>
        public EndsWith(IText text, IText tail)
        {
            this.result =
                new ScalarOf<bool>(() =>
                {
                    var regex = new Regex(Regex.Escape(tail.AsString()) + "$");
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
