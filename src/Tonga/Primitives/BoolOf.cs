

using System;
using System.IO;
using Tonga.Scalar;

namespace Tonga.Text
{
    /// <summary>
    /// A bool out of text objects.
    /// </summary>
    public sealed class BoolOf : IScalar<Boolean>
    {
        private readonly AsScalar<bool> bl;

        /// <summary>
        /// <see cref="string"/> as bool
        /// </summary>
        /// <param name="str">source string</param>
        public BoolOf(String str) : this(new AsText(str))
        { }

        /// <summary>
        /// <see cref="IText"/> as bool
        /// </summary>
        /// <param name="text">source text "true" or "false"</param>
        public BoolOf(IText text)
        {
            this.bl =
                new AsScalar<bool>(() =>
                {
                    try
                    {
                        return Convert.ToBoolean(text.AsString());
                    }
                    catch (FormatException ex)
                    {
                        throw new IOException(ex.Message, ex);
                    }
                });
        }

        /// <summary>
        /// Bool value
        /// </summary>
        /// <returns>true or false</returns>
        public Boolean Value()
        {
            return this.bl.Value();
        }
    }
}
