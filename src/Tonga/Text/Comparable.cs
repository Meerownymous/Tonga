using System;

namespace Tonga.Text
{
    /// <summary>
    /// A Text that can be compared using the Equals method.
    /// </summary>
    public sealed class Comparable : TextEnvelope, IComparable
    {
        private readonly IText text;

        /// <summary>
        /// A Text that can be compared using the Equals method.
        /// The text is always sticky (non live)
        /// </summary>
        public Comparable(IText text) : base(text, false)
        {
            this.text = new TextOf(() => text.AsString());
        }

        public int CompareTo(object obj)
        {
            if (!(obj is IText))
                throw new InvalidOperationException("Cannot compare, because given object is not of type IText");
            return this.text.AsString().CompareTo(((IText)obj).AsString());
        }
    }
}
