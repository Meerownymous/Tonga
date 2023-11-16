

using System;

namespace Tonga.Text
{
    /// <summary>
    /// A blank text.
    /// </summary>
    public sealed class Empty : IText
    {
        /// <summary>
        /// A blank text.
        /// </summary>
        public Empty()
        { }

        public string AsString()
        {
            return String.Empty;
        }

        public static Empty _() => new Empty();
    }
}
