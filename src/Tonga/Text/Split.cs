

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Tonga.Enumerable;

#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable CS1591
namespace Tonga.Text
{
    /// <summary>
    /// A <see cref="IText"/> which has been splitted at the given string.
    /// </summary>
    public sealed class Split : EnumerableEnvelope
    {
        /// <summary>
        /// A <see cref="IText"/> which has been splitted at the given string.
        /// </summary>
        /// <param name="text">text to split</param>
        /// <param name="rgx">regex to use for splitting</param>
        /// <param name="remBlank">switch to remove empty or whitspace stirngs from result or not</param>
        public Split(String text, String rgx, bool remBlank = true) : this(
            AsText._(text),
            AsText._(rgx),
            remBlank)
        { }

        /// <summary>
        /// A <see cref="IText"/> which has been splitted at the given string.
        /// </summary>
        /// <param name="text">text to split</param>
        /// <param name="rgx">regex to use for splitting</param>
        /// <param name="remBlank">switch to remove empty or whitspace stirngs from result or not</param>
        public Split(String text, IText rgx, bool remBlank = true) : this(
            AsText._(text),
            rgx,
            remBlank)
        { }

        /// <summary>
        /// A <see cref="IText"/> which has been splitted at the given string.
        /// </summary>
        /// <param name="text">text to split</param>
        /// <param name="rgx">regex to use for splitting</param>
        /// <param name="remBlank">switch to remove empty or whitspace stirngs from result or not</param>
        public Split(IText text, String rgx, bool remBlank = true) : this(
            text,
            AsText._(rgx),
            remBlank
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> which has been splitted at the given string.
        /// </summary>
        /// <param name="text">text to split</param>
        /// <param name="rgx">regex to use for splitting</param>
        /// <param name="remBlank">switch to remove empty or whitspace stirngs from result or not</param>
        public Split(IText text, IText rgx, bool remBlank = true) : base(() =>
            {
                IEnumerable<string> split =
                    AsEnumerable._(
                        new Regex(rgx.AsString())
                            .Split(text.AsString())
                    );

                return
                    remBlank ?
                    Filtered._(
                        (str) => !String.IsNullOrWhiteSpace(str),
                        split
                    )
                    :
                    split;
            }
        )
        { }
    }
}
