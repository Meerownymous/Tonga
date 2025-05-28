

using System;

namespace Tonga.IO
{
    /// <summary>
    /// www <see cref="Uri"/> from a <see cref="string"/> which checks for format.
    /// </summary>
    public sealed class Url(string src) : IScalar<Uri>
    {
        /// <summary>
        /// Get the uri.
        /// </summary>
        /// <returns></returns>
        public Uri Value()
        {
            if (!src.StartsWith("http://") && !src.StartsWith("https://") && !src.StartsWith("ftp://"))
            {
                throw new ArgumentException("url must start with http or https");
            }
            return new Uri(src);
        }
    }
}
