

using System;

namespace Tonga.Scalar
{
    /// <summary>
    /// A <see cref="char"/> out of other objects.
    /// </summary>
    public sealed class CharOf : IScalar<char>
    {
        private readonly IScalar<char> converter;

        /// <summary>
        /// Converts the value of the specified 32-bit signed integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="integer">The 32-bit signed integer to convert.</param>
        public CharOf(int integer) :
            this(AsScalar._(() => Convert.ToChar(integer)))
        { }

        /// <summary>
        /// Converts the value of the specified 32-bit unsigned integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="uInteger">The 32-bit unsigned integer to convert.</param>
        public CharOf(uint uInteger) :
            this(AsScalar._(() => Convert.ToChar(uInteger)))
        { }

        /// <summary>
        /// Converts the value of the specified 16-bit signed integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="shrt">The 16-bit signed integer to convert.</param>
        public CharOf(short shrt) :
            this(AsScalar._(() => Convert.ToChar(shrt)))
        { }

        /// <summary>
        /// Converts the value of the specified 16-bit unsigned integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="uShort">The 16-bit unsigned integer to convert.</param>
        public CharOf(ushort uShort) :
            this(AsScalar._(() => Convert.ToChar(uShort)))
        { }

        /// <summary>
        /// Converts the value of the specified 64-bit signed integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="lng">The 64-bit signed integer to convert.</param>
        public CharOf(long lng) :
            this(AsScalar._(() => Convert.ToChar(lng)))
        { }

        /// <summary>
        /// Converts the value of the specified 64-bit unsigned integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="ulng">The 64-bit unsigned integer to convert.</param>
        public CharOf(ulong ulng) :
            this(AsScalar._(() => Convert.ToChar(ulng)))
        { }

        /// <summary>
        /// Converts the value of the specified double-precision floating-point number to an equivalent Unicode character.
        /// </summary>
        /// <param name="dbl">The double-precision floating-point number to convert.</param>
        public CharOf(double dbl) :
            this(AsScalar._(() => Convert.ToChar(Convert.ToInt64(dbl))))
        { }

        /// <summary>
        /// Converts the value of the specified single-precision floating-point number to an equivalent Unicode character.
        /// </summary>
        /// <param name="flt">The single-precision floating-point number.</param>
        public CharOf(float flt) :
            this(AsScalar._(() => Convert.ToChar(Convert.ToInt64(flt))))
        { }

        /// <summary>
        /// Converts the first character of a specified string to a Unicode character.
        /// </summary>
        /// <param name="str">A string of length 1.</param>
        public CharOf(string str) :
            this(AsScalar._(() => Convert.ToChar(str)))
        { }

        /// <summary>
        /// Converts the value of the specified 8-bit unsigned integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="byt">The 8-bit unsigned integer to convert.</param>
        public CharOf(byte byt) :
            this(AsScalar._(() => Convert.ToChar(byt)))
        { }

        /// <summary>
        /// Converts the value of the specified 8-bit signed integer to its equivalent Unicode character.
        /// </summary>
        /// <param name="sByt">The 8-bit signed integer to convert.</param>
        public CharOf(sbyte sByt) :
            this(AsScalar._(() => Convert.ToChar(sByt)))
        { }

        /// <summary>
        /// Primary ctor
        /// </summary>
        /// <param name="converter">Converter method who returns the character.</param>
        private CharOf(IScalar<char> converter)
        {
            this.converter = converter;
        }

        /// <summary>
        /// the char
        /// </summary>
        /// <returns></returns>
        public char Value()
        {
            return converter.Value();
        }
    }
}
