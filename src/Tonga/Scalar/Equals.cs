

using System;

namespace Tonga.Scalar
{
    /// <summary>
    /// Checks the equality of contents.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Equals<T> : ScalarEnvelope<Boolean>
        where T : IComparable<T>
    {
        /// <summary>
        /// Checks the equality of contents.
        /// </summary>
        /// <param name="first">function to return first value to compare</param>
        /// <param name="second">function to return second value to compare</param>
        public Equals(Func<T> first, Func<T> second) : this(
            AsScalar._(first),
            AsScalar._(second)
        )
        { }

        /// <summary>
        /// Checks the equality of contents.
        /// </summary>
        /// <param name="first">first value to compare</param>
        /// <param name="second">second value to compare</param>
        public Equals(T first, T second) : this(
            AsScalar._(first),
            AsScalar._(second)
        )
        { }

        /// <summary>
        /// Checks the equality of contents.
        /// </summary>
        /// <param name="first">scalar of first value to compare</param>
        /// <param name="second">scalar of second value to compare</param>
        public Equals(IScalar<T> first, IScalar<T> second) : base(
            () => first.Value().CompareTo(second.Value()) == 0
        )
        { }
    }

    public static class Equals
    {
        /// <summary>
        /// Checks the equality of contents.
        /// </summary>
        /// <param name="first">function to return first value to compare</param>
        /// <param name="second">function to return second value to compare</param>
        public static Equals<T> _<T>(Func<T> first, Func<T> second)
            where T : IComparable<T>
            => new Equals<T>(first, second);

        /// <summary>
        /// Checks the equality of contents.
        /// </summary>
        /// <param name="first">first value to compare</param>
        /// <param name="second">second value to compare</param>
        public static Equals<T> _<T>(T first, T second)
            where T : IComparable<T>
            => new Equals<T>(first, second);

        /// <summary>
        /// Checks the equality of contents.
        /// </summary>
        /// <param name="first">scalar of first value to compare</param>
        /// <param name="second">scalar of second value to compare</param>
        public static Equals<T> _<T>(ScalarEnvelope<T> first, ScalarEnvelope<T> second)
            where T : IComparable<T>
            => new Equals<T>(first, second);
    }
}
