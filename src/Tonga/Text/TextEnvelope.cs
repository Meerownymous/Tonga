

using System;
using Tonga.Scalar;

namespace Tonga.Text
{
    /// <summary>
    /// A <see cref="IText"/> envelope.
    /// The envelope can work in live or in sticky mode.
    /// </summary>
    public abstract class TextEnvelope(Func<string> origin) : IText
    {
        /// <summary>
        /// A <see cref="IText"/> envelope.
        /// The envelope can work in live or in sticky mode.
        /// </summary>
        /// <param name="origin">How to create the value</param>
        public TextEnvelope(IText origin) : this(origin.Str)
        { }

        /// <summary>
        /// Gives the text as a string.
        /// </summary>
        /// <returns></returns>
        public String Str() => origin();
    }
}
