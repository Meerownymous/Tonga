using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Tonga.Text;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Lines in a given text.
    /// </summary>
    public sealed class Lines : IEnumerable<string>
    {
        private readonly IText source;
        private readonly bool skipEmpty;

        /// <summary>
        /// Lines in a given text.
        /// </summary>
        public Lines(string source, bool skipEmpty = false) : this(AsText._(source), skipEmpty)
        { }

        /// <summary>
        /// Lines in a given text.
        /// </summary>
        public Lines(IText source, bool skipEmpty = false)
        {
            this.source = source;
            this.skipEmpty = skipEmpty;
        }

        public IEnumerator<string> GetEnumerator()
        {
            using (StringReader reader = new StringReader(this.source.AsString()))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if(!(skipEmpty && String.IsNullOrEmpty(line)))
                        yield return line;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <summary>
        /// Lines in a given text.
        /// </summary>
        public static IEnumerable<string> _(IText source) =>
            new Lines(source);
    }
}
