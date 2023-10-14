

using System;
using System.Globalization;
using System.Linq;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.Text
{
    /// <summary>
    /// A <see cref="IText"/> formatted with arguments.
    /// Use C# formatting syntax: new FormattedText("{0} is {1}", "OOP", "great").AsString() will be "OOP is great"
    /// </summary>
    public sealed class Formatted : TextEnvelope
    {
        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="arguments">arguments to apply</param>
        public Formatted(String ptn, params IText[] arguments) : this(
            new LiveText(ptn),
            CultureInfo.InvariantCulture,
            () =>
            new Mapped<IText, string>(
                txt => txt.AsString(),
                arguments
            ).ToArray()
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="arguments">arguments to apply</param>
        public Formatted(String ptn, params object[] arguments) : this(
            new LiveText(ptn), CultureInfo.InvariantCulture, new Live<object[]>(arguments))
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="arguments">arguments to apply</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Formatted(String ptn, bool live, params object[] arguments) : this(
            new LiveText(ptn), live, CultureInfo.InvariantCulture, arguments)
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="arguments">arguments to apply</param>
        public Formatted(IText ptn, params object[] arguments) : this(
            ptn,
            CultureInfo.InvariantCulture,
            false,
            arguments
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="arguments">arguments to apply</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Formatted(IText ptn, bool live, params object[] arguments) : this(
            ptn, CultureInfo.InvariantCulture, live, arguments
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern</param>
        /// <param name="local">CultureInfo</param>
        /// <param name="arguments">arguments to apply</param>
        public Formatted(IText ptn, CultureInfo local, params object[] arguments) : this(
            ptn, local, false, arguments
            )
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern</param>
        /// <param name="local">CultureInfo</param>
        /// <param name="arguments">arguments to apply</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Formatted(IText ptn, CultureInfo local, bool live, params object[] arguments) : this(
            ptn, local, () => arguments, live
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments to apply</param>
        public Formatted(String ptn, CultureInfo locale, params object[] arguments) : this(
            new LiveText(ptn), locale, false, arguments)
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments to apply</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Formatted(String ptn, CultureInfo locale, bool live, params object[] arguments) : this(
            new LiveText(ptn), locale, live, arguments)
        { }

        /// <summary>
        ///  A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="arguments">arguments as <see cref="IText"/> to apply</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Formatted(string ptn, bool live, params IText[] arguments) : this(
            new LiveText(ptn),
            CultureInfo.InvariantCulture,
            () =>
            {
                object[] strings = new object[new LengthOf(arguments).Value()];
                for (int i = 0; i < arguments.Length; i++)
                {
                    strings[i] = arguments[i].AsString();
                }
                return strings;
            },
            live
        )
        { }

        /// <summary>
        ///  A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments as <see cref="IText"/> to apply</param>

        public Formatted(string ptn, CultureInfo locale, params IText[] arguments) : this(
            new LiveText(ptn),
            locale,
            false,
            arguments
        )
        { }

        /// <summary>
        ///  A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments as <see cref="IText"/> to apply</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>

        public Formatted(string ptn, CultureInfo locale, bool live, params IText[] arguments) : this(
            new LiveText(ptn),
            locale,
            () =>
            {
                object[] strings = new object[new LengthOf(arguments).Value()];
                for (int i = 0; i < arguments.Length; i++)
                {
                    strings[i] = arguments[i].AsString();
                }
                return strings;
            },
            live
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments to apply</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Formatted(
            IText ptn,
            CultureInfo locale,
            IScalar<object[]> arguments,
            bool live = false
        ) : this(
            ptn,
            locale,
            () => arguments.Value(),
            live
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments to apply</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Formatted(
            IText ptn,
            CultureInfo locale,
            Func<object[]> arguments,
            bool live = false
        ) : base(
            () => String.Format(locale, ptn.AsString(), arguments()),
            live
        )
        { }
    }
}
