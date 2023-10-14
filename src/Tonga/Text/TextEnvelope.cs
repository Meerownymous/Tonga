

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
        private readonly Func<string> origin;
        private readonly ScalarOf<string> fixedOrigin;
        private readonly bool live;

        /// <summary>
        /// A <see cref="IText"/> envelope.
        /// The envelope can work in live or in sticky mode.
        /// </summary>
        /// <param name="text">Origin text</param>
        /// <param name="live">should the value be created every time the object is used?</param>
        public TextEnvelope(IText text, bool live) : this(() => text.AsString(), live)
        { }

        /// <summary>
        /// A <see cref="IText"/> envelope.
        /// The envelope can work in live or in sticky mode.
        /// </summary>
        /// <param name="origin">How to create the value</param>
        /// <param name="live">should the value be created every time the object is used?</param>
        public TextEnvelope(Func<string> origin, bool live)
        {
            this.origin = origin;
            this.live = live;
            this.fixedOrigin = new ScalarOf<string>(() => origin());
        }

        /// <summary>
        /// Gives the text as a string.
        /// </summary>
        /// <returns></returns>
        public String AsString()
        {
            var result = string.Empty;
            if (this.live)
            {
                result = this.origin();
            }
            else
            {
                result = this.fixedOrigin.Value();
            }
            return result;
        }
    }
}
