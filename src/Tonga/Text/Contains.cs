

using System;
using Tonga.Scalar;

namespace Tonga.Text
{
    /// <summary> Check if a text contains a pattern </summary>
    public sealed class Contains : Scalar.ScalarEnvelope<bool>
    {
        private readonly AsScalar<bool> result;

        /// <summary> Checks if a text contains a pattern using strings </summary>
        /// <param name="inputStr"> text as string </param>
        /// <param name="patternStr"> pattern as string </param>
        /// <param name="ignoreCase"> Enables case sensitivity </param>
        public Contains(string inputStr, string patternStr, bool ignoreCase = false) : this(
            AsScalar._(inputStr),
            AsScalar._(patternStr),
            AsScalar._(() => ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture)
        )
        { }

        /// <summary> Checks if a text contains a pattern using IText </summary>
        /// <param name="inputText"> text as IText </param>
        /// <param name="patternText"> pattern as IText </param>
        /// <param name="ignoreCase"> Enables case sensitivity </param>
        public Contains(IText inputText, IText patternText, bool ignoreCase = false) : this(
            AsScalar._(inputText.AsString),
            AsScalar._(patternText.AsString),
            AsScalar._(() => ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture)
        )
        { }

        /// <summary> Checks if a text contains a pattern using IScalar </summary>
        /// <param name="inputValue"> text as IScalar of string </param>
        /// <param name="pattern"> pattern as IScalar of string </param>
        public Contains(IScalar<string> inputValue, IScalar<string> pattern) : this(
            inputValue,
            pattern,
            AsScalar._(StringComparison.CurrentCulture)
        )
        { }

        /// <summary> Checks if a text contains a pattern using IScalar </summary>
        /// <param name="inputValue"> text as IScalar of string </param>
        /// <param name="pattern"> pattern as IScalar of string </param>
        /// <param name="stringComparison"> Enables case sensitivity (as IScalar of bool) </param>
        public Contains(IScalar<string> inputValue, IScalar<string> pattern, IScalar<StringComparison> stringComparison) : this(
            inputValue.Value,
            pattern.Value,
            stringComparison.Value
        )
        { }

        /// <summary> Checks if a text contains a pattern using IScalar </summary>
        /// <param name="inputValue"> text as IScalar of string </param>
        /// <param name="pattern"> pattern as IScalar of string </param>
        /// <param name="stringComparison"> Enables case sensitivity (as IScalar of bool) </param>
        public Contains(Func<string> inputValue, Func<string> pattern, Func<StringComparison> stringComparison) : base(
            () => inputValue().IndexOf(pattern(), stringComparison()) >= 0
        )
        { }
    }
}
