using System;

namespace Tonga.Text
{
    /// <summary>
    /// A <see cref="IText"/> envelope.
    /// The envelope can work in live or in sticky mode.
    /// </summary>
    public class TextEnvelope(Func<string> origin) : IText
    {
        private TextEnvelope(string origin) : this(() => origin)
        { }

        /// <summary>
        /// A <see cref="IText"/> envelope.
        /// The envelope can work in live or in sticky mode.
        /// </summary>
        /// <param name="origin">How to create the value</param>
        public TextEnvelope(IText origin) : this(origin.Str)
        { }

        /// <summary>
        /// A <see cref="IText"/> envelope.
        /// The envelope can work in live or in sticky mode.
        /// </summary>
        /// <param name="origin">How to create the value</param>
        public TextEnvelope(TextMorph origin) : this(origin.Str)
        { }

        /// <summary>
        /// Gives the text as a string.
        /// </summary>
        /// <returns></returns>
        public String Str() => origin();
    }
}
