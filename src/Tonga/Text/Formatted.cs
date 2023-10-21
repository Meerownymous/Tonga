

using System;
using System.Globalization;
using System.Linq;
using Tonga.Enumerable;
using Tonga.Scalar;
using Tonga.Text;

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
            AsText._(ptn),
            CultureInfo.InvariantCulture,
            () =>
            Mapped._(
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
            AsText._(ptn),
            CultureInfo.InvariantCulture,
            AsScalar._(arguments)
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="arguments">arguments to apply</param>
        public Formatted(IText ptn, params object[] arguments) : this(
            ptn,
            CultureInfo.InvariantCulture,
            arguments
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern</param>
        /// <param name="local">CultureInfo</param>
        /// <param name="arguments">arguments to apply</param>
        public Formatted(IText ptn, CultureInfo local, params object[] arguments) : this(
            ptn, local, () => arguments
            )
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments to apply</param>
        public Formatted(String ptn, CultureInfo locale, params object[] arguments) : this(
            AsText._(ptn), locale, arguments)
        { }

        /// <summary>
        ///  A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments as <see cref="IText"/> to apply</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>

        public Formatted(string ptn, CultureInfo locale, params IText[] arguments) : this(
            AsText._(ptn),
            locale,
            () =>
            {
                object[] strings = new object[new LengthOf(arguments).Value()];
                for (int i = 0; i < arguments.Length; i++)
                {
                    strings[i] = arguments[i].AsString();
                }
                return strings;
            }
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
            IScalar<object[]> arguments
        ) : this(
            ptn,
            locale,
            arguments.Value
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
            Func<object[]> arguments
        ) : base(
            () => String.Format(locale, ptn.AsString(), arguments())
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="arguments">arguments to apply</param>
        public static Formatted From(String ptn, params IText[] arguments) =>
            new Formatted(ptn, arguments);

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="arguments">arguments to apply</param>
        public static Formatted From(String ptn, params object[] arguments) =>
            new Formatted(ptn, arguments);


        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="arguments">arguments to apply</param>
        public static Formatted From(IText ptn, params object[] arguments) =>
            new Formatted(ptn, arguments);

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern</param>
        /// <param name="local">CultureInfo</param>
        /// <param name="arguments">arguments to apply</param>
        public static Formatted From(IText ptn, CultureInfo local, params object[] arguments) =>
            new Formatted(ptn, local, arguments);

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments to apply</param>
        public static Formatted From(String ptn, CultureInfo locale, params object[] arguments) =>
            new Formatted(ptn, locale, arguments);

        /// <summary>
        ///  A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments as <see cref="IText"/> to apply</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>

        public static Formatted From(string ptn, CultureInfo locale, params IText[] arguments) =>
            new Formatted(ptn, locale, arguments);

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments to apply</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public static Formatted From(IText ptn, CultureInfo locale, IScalar<object[]> arguments) =>
            new Formatted(ptn, locale, arguments);

        /// <summary>
        /// A <see cref="IText"/> formatted with arguments.
        /// </summary>
        /// <param name="ptn">pattern to put arguments in</param>
        /// <param name="locale">a specific culture</param>
        /// <param name="arguments">arguments to apply</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public static Formatted From(IText ptn, CultureInfo locale, System.Func<object[]> arguments) =>
            new Formatted(ptn, locale, arguments);
    }
}
