

using System;
using Tonga.Scalar;

namespace Tonga.Text
{
    /// <summary>
    /// Extracted subtext from a <see cref="IText"/>.
    /// </summary>
    public sealed class SubText : TextEnvelope
    {
        /// <summary>
        /// Extracted subtext from a <see cref="string"/>.
        /// </summary>
        public SubText(String text, int start) : this(AsText._(text), start)
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="string"/>.
        /// </summary>
        public SubText(String text, int start, int length) : this(
            AsText._(text),
            start,
            length
        )
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="IText"/>.
        /// </summary>
        public SubText(IText text, int start) : this(
            text,
            AsScalar._(start),
            AsScalar._(() => text.AsString().Length - start)
        )
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="IText"/>.
        /// </summary>
        public SubText(IText text, int start, int length) : this(
            text,
            AsScalar._(start),
            AsScalar._(length)
        )
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="IText"/>.
        /// </summary>
        public SubText(IText text, IScalar<Int32> start, IScalar<Int32> length) : this(
            text,
            start.Value,
            length.Value
        )
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="IText"/>.
        /// </summary>
        public SubText(IText text, Func<int> start, Func<int> length) : base(() =>
            {
                return
                    text.AsString().Substring(
                        start(),
                        length()
                    );
            }
        )
        { }
    }
}
