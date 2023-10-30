

using System;

namespace Tonga.Text
{
    /// <summary>
    /// A <see cref="IText"/> which has been reversed.
    /// </summary>
    public sealed class Reversed : TextEnvelope
    {
        /// <summary>
        /// A <see cref="IText"/> which has been reversed.
        /// </summary>
        /// <param name="text">text to reverse</param>
        public Reversed(IText text) : base(() =>
           {
               char[] chararray = text.AsString().ToCharArray();
               Array.Reverse(chararray);
               string reverseTxt = "";
               for (int i = 0; i <= chararray.Length - 1; i++)
               {
                   reverseTxt += chararray.GetValue(i);
               }
               return reverseTxt;
           }
        )
        { }
    }
}
