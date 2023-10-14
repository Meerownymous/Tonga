

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
        public SubText(String text, int start) : this(new LiveText(text), start)
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="string"/>.
        /// </summary>
        public SubText(String text, int start, int length) : this(
            new LiveText(text),
            start,
            length
        )
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="IText"/>.
        /// </summary>
        public SubText(IText text, int start) : this(
            text,
            new Live<Int32>(start),
            new Live<Int32>(() => text.AsString().Length - start)
        )
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="IText"/>.
        /// </summary>
        public SubText(IText text, int start, int length) : this(
            text,
            new Live<Int32>(start),
            new Live<Int32>(length)
        )
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="IText"/>.
        /// </summary>
        public SubText(
            IText text,
            Live<Int32> start,
            Live<Int32> length,
            bool live = false
        ) : this(
            text,
            () => start.Value(),
            () => length.Value()
        )
        { }

        /// <summary>
        /// Extracted subtext from a <see cref="IText"/>.
        /// </summary>
        public SubText(IText text, Func<Int32> start, Func<Int32> length) : base(() =>
            {
                return
                    text.AsString().Substring(
                        start(),
                        length()
                    );
            },
            false
        )
        { }
    }
}
