

using System;
using Tonga.Scalar;

namespace Tonga.Fact
{
    /// <summary>
    /// Checks the equality of contents.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class IsEqual<T> : FactEnvelope
        where T : IComparable<T>
    {
        /// <summary>
        /// Checks the equality of contents.
        /// </summary>
        /// <param name="first">function to return first value to compare</param>
        /// <param name="second">function to return second value to compare</param>
        public IsEqual(Func<T> first, Func<T> second) : this(
            new AsScalar<T>(first),
            new AsScalar<T>(second)
        )
        { }

        /// <summary>
        /// Checks the equality of contents.
        /// </summary>
        /// <param name="first">first value to compare</param>
        /// <param name="second">second value to compare</param>
        public IsEqual(T first, T second) : this(
            new AsScalar<T>(first),
            new AsScalar<T>(second)
        )
        { }

        /// <summary>
        /// Checks the equality of contents.
        /// </summary>
        /// <param name="first">scalar of first value to compare</param>
        /// <param name="second">scalar of second value to compare</param>
        public IsEqual(IScalar<T> first, IScalar<T> second) : base(
            new AsFact(() => first.Value().CompareTo(second.Value()) == 0)
        )
        { }
    }

    public static class FactSmarts
    {
        /// <summary>
        /// Checks the equality of contents.
        /// </summary>
        /// <param name="first">function to return first value to compare</param>
        /// <param name="second">function to return second value to compare</param>
        public static IFact Equals<T>(this Func<T> first, Func<T> second)
            where T : IComparable<T>
            => new IsEqual<T>(first, second);

        /// <summary>
        /// Checks the equality of contents.
        /// </summary>
        /// <param name="first">first value to compare</param>
        /// <param name="second">second value to compare</param>
        public static IFact Equals<T>(this T first, T second)
            where T : IComparable<T>
            => new IsEqual<T>(first, second);

        /// <summary>
        /// Checks the equality of contents.
        /// </summary>
        /// <param name="first">scalar of first value to compare</param>
        /// <param name="second">scalar of second value to compare</param>
        public static IFact Equals<T>(this IScalar<T> first, IScalar<T> second)
            where T : IComparable<T>
            => new IsEqual<T>(first, second);
    }
}
