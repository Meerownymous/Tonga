

using System;
using System.Text;

namespace Tonga.Text
{
    /// <summary>
    /// A <see cref="IText"/> repeated multiple times.
    /// </summary>
    public sealed class Repeated : TextEnvelope
    {
        /// <summary>
        /// A <see cref="IText"/>  repeated multiple times.
        /// </summary>
        /// <param name="text">text to repeat</param>
        /// <param name="count">how often to repeat</param>
        public Repeated(String text, int count) : this(
            new LiveText(text),
            count
        )
        { }

        /// <summary>
        /// A <see cref="IText"/>  repeated multiple times.
        /// </summary>
        /// <param name="text">text to repeat</param>
        /// <param name="count">how often to repeat</param>
        public Repeated(IText text, int count) : base(() =>
            {
                StringBuilder output = new StringBuilder();
                for (int cnt = 0; cnt < count; ++cnt)
                {
                    output.Append(text.AsString());
                }
                return output.ToString();
            },
            false
        )
        { }
    }
}
