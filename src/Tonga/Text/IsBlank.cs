

using System;
using System.Linq;
using Tonga.Scalar;

namespace Tonga.Text
{
    /// <summary>
    /// Checks if a text is whitespace.
    /// </summary>
    public sealed class IsBlank : IScalar<Boolean>
    {
        private readonly Func<bool> result;

        /// <summary>
        /// Checks if a A <see cref="string"/> is whitespace.
        /// </summary>
        /// <param name="text">text to check</param>
        public IsBlank(string text) : this(
            new AsText(text)
        )
        { }

        /// <summary>
        /// Checks if a A <see cref="IText"/> is whitespace.
        /// </summary>
        /// <param name="text">text to check</param>
        public IsBlank(IText text)
        {
            this.result = () => !text.AsString().ToCharArray().Any(c => !String.IsNullOrWhiteSpace(c + ""));
        }

        /// <summary>
        /// Get the result.
        /// </summary>
        /// <returns>the result</returns>
        public Boolean Value()
        {
            return this.result();
        }
    }
}
