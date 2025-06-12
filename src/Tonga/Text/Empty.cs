

using System;

namespace Tonga.Text
{
    /// <summary>
    /// A blank text.
    /// </summary>
    public sealed class Empty() : IText
    {
        public string Str() => String.Empty;
    }
}
