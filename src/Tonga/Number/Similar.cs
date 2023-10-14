

using System;
using Tonga.Scalar;

namespace Tonga.Number
{
    /// <summary>
    /// Checks if two Numbers are similar with a given accuracy
    /// </summary>
    public sealed class Similar : IScalar<Boolean>
    {
        private readonly IScalar<Boolean> isSimilar;

        /// <summary>
        /// Checks if two Numbers are similar with a given accuracy
        /// </summary>
        /// <param name="first">First Number</param>
        /// <param name="second">Second Number</param>
        public Similar(INumber first, INumber second) : this(first, second, 0)
        { }


        /// <summary>
        /// Checks if two Numbers are similar with a given accuracy
        /// </summary>
        /// <param name="first">First Number</param>
        /// <param name="second">Second Number</param>
        /// <param name="accuracy">Number of equal decimal places</param>
        public Similar(INumber first, INumber second, int accuracy)
        {
            this.isSimilar =
                new ScalarOf<Boolean>(() =>
                {
                    bool isSimilar;
                    if (accuracy > 0)
                    {
                        isSimilar =
                            Math.Abs(
                                first.AsDouble() - second.AsDouble()
                            ) <
                            Math.Pow(
                                10, -1 * accuracy
                            );
                    }
                    else
                    {
                        isSimilar =
                            Math.Abs(
                                    first.AsDouble() - second.AsDouble()
                                ) == 0;
                    }


                    return isSimilar;

                });
        }

        public bool Value()
        {
            return this.isSimilar.Value();
        }
    }
}
