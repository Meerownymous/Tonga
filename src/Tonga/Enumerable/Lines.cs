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
    public sealed class Lines(IText source, bool skipEmpty = false) : IEnumerable<string>
    {
        /// <summary>
        /// Lines in a given text.
        /// </summary>
        public Lines(string source, bool skipEmpty = false) : this(AsText._(source), skipEmpty)
        { }

        public IEnumerator<string> GetEnumerator()
        {
            using StringReader reader = new StringReader(source.AsString());
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if(!(skipEmpty && String.IsNullOrEmpty(line)))
                    yield return line;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <summary>
        /// Lines in a given text.
        /// </summary>
        public static IEnumerable<string> _(IText source) =>
            new Lines(source);
    }

    public static class LinesSmarts
    {
        public static IEnumerable<string> Lines(this IText source, bool skipEmpty = false) =>
            new Lines(source, skipEmpty);
    }
}
