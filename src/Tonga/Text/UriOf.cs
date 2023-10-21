

using System;
using Tonga.Scalar;

namespace Tonga.Text
{
    /// <summary>
    /// A text as a <see cref="Uri"/>
    /// </summary>
    public sealed class UriOf : IScalar<Uri>
    {
        private readonly AsScalar<Uri> source;

        /// <summary>
        /// A <see cref="string"/> as a <see cref="Uri"/>
        /// </summary>
        /// <param name="url">url as a string</param>
        public UriOf(String url) : this(AsText._(url))
        { }

        /// <summary>
        /// A <see cref="IText"/> as a <see cref="Uri"/>
        /// </summary>
        /// <param name="url">uri as text</param>
        public UriOf(IText url)
        {
            this.source =
                new AsScalar<Uri>(
                    new UriBuilder(url.AsString()).Uri
                );
        }

        /// <summary>
        /// Get the uri.
        /// </summary>
        /// <returns>the uri</returns>
        public Uri Value()
        {
            return this.source.Value();
        }
    }
}
