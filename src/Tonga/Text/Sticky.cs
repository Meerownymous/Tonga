using System;
namespace Tonga.Text
{
    /// <summary>
    /// Text which is sticky - it remembers its output instead of regenerating every time.
    /// </summary>
    public sealed class AsSticky : IText
    {
        private Lazy<string> result;

        /// <summary>
        /// Text which is sticky - it remembers its output instead of regenerating every time.
        /// </summary>
        public AsSticky(IText origin)
        {
            this.result = new Lazy<string>(origin.AsString);
        }

        public string AsString()
        {
            return this.result.Value;
        }

        public static AsSticky From(IText origin) => new AsSticky(origin);
    }
}

