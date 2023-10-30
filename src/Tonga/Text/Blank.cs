

using System;

namespace Tonga.Text
{
    /// <summary>
    /// A blank text.
    /// </summary>
    public sealed class Blank : IText
    {
        /// <summary>
        /// A blank text.
        /// </summary>
        public Blank()
        { }

        public string AsString()
        {
            return String.Empty;
        }

        public static Blank New() => new Blank();
    }
}
