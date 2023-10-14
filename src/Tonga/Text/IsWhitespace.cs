

using System;
using System.Linq;
using Tonga.Scalar;

namespace Tonga.Text
{
    /// <summary>
    /// Checks if a text is whitespace.
    /// </summary>
    public sealed class IsWhitespace : IScalar<Boolean>
    {
        private readonly ScalarOf<bool> result;

        /// <summary>
        /// Checks if a A <see cref="string"/> is whitespace.
        /// </summary>
        /// <param name="text">text to check</param>
        public IsWhitespace(string text) : this(
            new TextOf(text)
        )
        { }

        /// <summary>
        /// Checks if a A <see cref="IText"/> is whitespace.
        /// </summary>
        /// <param name="text">text to check</param>
        public IsWhitespace(IText text)
        {
            this.result = new ScalarOf<bool>(() => !text.AsString().ToCharArray().Any(c => !String.IsNullOrWhiteSpace(c + "")));
        }

        /// <summary>
        /// Get the result.
        /// </summary>
        /// <returns>the result</returns>
        public Boolean Value()
        {
            return this.result.Value();
        }
    }
}
