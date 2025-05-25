using System;

namespace Tonga.Text;

    /// <summary>
    /// A Text that can be compared using the Equals method.
    /// </summary>
    public sealed class Comparable(IText text) : TextEnvelope(text), IComparable
    {
        public int CompareTo(object obj)
        {
            if (!(obj is IText))
                throw new InvalidOperationException("Cannot compare, because given object is not of type IText");
            return String.Compare(text.Str(), ((IText)obj).Str(), StringComparison.Ordinal);
        }
    }

    public static partial class TextSmarts
    {

    /// <summary>
        /// A Text that can be compared using the Equals method.
        /// </summary>
        public static Comparable AsComparable(this IText text) => new(text);
    }
