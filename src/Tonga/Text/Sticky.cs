using System;
namespace Tonga.Text
{
    /// <summary>
    /// Text which is sticky - it remembers its output instead of regenerating every time.
    /// </summary>
    public sealed class Sticky : IText
    {
        private Lazy<string> result;

        /// <summary>
        /// Text which is sticky - it remembers its output instead of regenerating every time.
        /// </summary>
        public Sticky(IText origin)
        {
            this.result = new Lazy<string>(origin.AsString);
        }

        public string AsString()
        {
            return this.result.Value;
        }

        public static Sticky _(IText origin) => new Sticky(origin);
    }
}

