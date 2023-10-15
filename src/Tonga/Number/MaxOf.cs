

using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.Number
{
    /// <summary>
    /// The maximum of the given numbers
    /// </summary>
    public sealed class MaxOf : NumberEnvelope
    {
        /// <summary>
        /// The maximum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MaxOf(params int[] src) : this(
            Enumerable.EnumerableOf.Pipe(src))
        { }

        /// <summary>
        /// The maximum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MaxOf(IEnumerable<int> src) : base(
            new ScalarOf<double>(() =>
            {
                var max = double.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = e.Current;
                }
                return max;
            }),
            new ScalarOf<int>(() =>
            {
                var max = int.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = e.Current;
                }
                return max;
            }),
            new ScalarOf<long>(() =>
            {
                var max = long.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = e.Current;
                }
                return max;
            }),
            new ScalarOf<float>(() =>
            {
                var max = float.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = e.Current;
                }
                return max;
            })
            )
        { }

        /// <summary>
        /// The maximum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MaxOf(params double[] src) : this(
            Enumerable.EnumerableOf.Pipe(src))
        { }

        /// <summary>
        /// The maximum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MaxOf(IEnumerable<double> src) : base(
            new ScalarOf<double>(() =>
            {
                var max = double.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = e.Current;
                }
                return max;
            }),
            new ScalarOf<int>(() =>
            {
                var max = int.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = (int)e.Current;
                }
                return max;
            }),
            new ScalarOf<long>(() =>
            {
                var max = long.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = (long)e.Current;
                }
                return max;
            }),
            new ScalarOf<float>(() =>
            {
                var max = float.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = (float)e.Current;
                }
                return max;
            })
        )
        { }

        /// <summary>
        /// The maximum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MaxOf(params long[] src) : this(
            Enumerable.EnumerableOf.Pipe(src))
        { }

        /// <summary>
        /// The maximum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MaxOf(IEnumerable<long> src) : base(
            new ScalarOf<double>(() =>
            {
                var max = double.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = e.Current;
                }
                return max;
            }),
            new ScalarOf<int>(() =>
            {
                var max = int.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = (int)e.Current;
                }
                return max;
            }),
            new ScalarOf<long>(() =>
            {
                var max = long.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = (long)e.Current;
                }
                return max;
            }),
            new ScalarOf<float>(() =>
            {
                var max = float.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = (float)e.Current;
                }
                return max;
            })
        )
        { }

        /// <summary>
        /// The maximum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MaxOf(params float[] src) : this(
            Enumerable.EnumerableOf.Pipe(src))
        { }

        /// <summary>
        /// The maximum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MaxOf(IEnumerable<float> src) : base(
            new ScalarOf<double>(() =>
            {
                var max = double.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = e.Current;
                }
                return max;
            }),
            new ScalarOf<int>(() =>
            {
                var max = int.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = (int)e.Current;
                }
                return max;
            }),
            new ScalarOf<long>(() =>
            {
                var max = long.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = (long)e.Current;
                }
                return max;
            }),
            new ScalarOf<float>(() =>
            {
                var max = float.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = (float)e.Current;
                }
                return max;
            })
        )
        { }
    }
}
