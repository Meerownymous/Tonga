

using System;
using Tonga.Scalar;

#pragma warning disable MaxClassLength // Class length max
namespace Tonga.Text
{
    /// <summary>
    /// A <see cref="IText"/> envelope.
    /// The envelope can work in live or in sticky mode.
    /// </summary>
    public abstract class TextEnvelope : IText
    {
        private readonly Func<IText> origin;

        /// <summary>
        /// A <see cref="IText"/> envelope.
        /// The envelope can work in live or in sticky mode.
        /// </summary>
        /// <param name="text">Origin text</param>
        /// <param name="live">should the value be created every time the object is used?</param>
        public TextEnvelope(IText text) : this(() => text)
        { }

        /// <summary>
        /// A <see cref="IText"/> envelope.
        /// The envelope can work in live or in sticky mode.
        /// </summary>
        /// <param name="origin">How to create the value</param>
        /// <param name="live">should the value be created every time the object is used?</param>
        public TextEnvelope(Func<IText> origin)
        {
            this.origin = origin;
        }

        /// <summary>
        /// Gives the text as a string.
        /// </summary>
        /// <returns></returns>
        public String AsString()
        {
            return this.origin.Invoke().AsString();
        }
    }
}
