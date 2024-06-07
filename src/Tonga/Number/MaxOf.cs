using System.Collections.Generic;
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
            Enumerable.AsEnumerable._(src))
        { }

        /// <summary>
        /// The maximum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MaxOf(IEnumerable<int> src) : base(
            new AsScalar<double>(() =>
            {
                var max = double.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = e.Current;
                }
                e.Dispose();
                return max;
            }),
            new AsScalar<int>(() =>
            {
                var max = int.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = e.Current;
                }
                e.Dispose();
                return max;
            }),
            new AsScalar<long>(() =>
            {
                var max = long.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = e.Current;
                }
                e.Dispose();
                return max;
            }),
            new AsScalar<float>(() =>
            {
                var max = float.MinValue;
                using(var e = src.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        if (e.Current > max) max = e.Current;
                    }
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
            Enumerable.AsEnumerable._(src))
        { }

        /// <summary>
        /// The maximum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MaxOf(IEnumerable<double> src) : base(
            new AsScalar<double>(() =>
            {
                var max = double.MinValue;
                using (var e = src.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        if (e.Current > max) max = e.Current;
                    }
                }

                return max;
            }),
            new AsScalar<int>(() =>
            {
                var max = int.MinValue;
                using (var e = src.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        if (e.Current > max) max = (int)e.Current;
                    }
                }

                return max;
            }),
            new AsScalar<long>(() =>
            {
                var max = long.MinValue;
                using (var e = src.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        if (e.Current > max) max = (long)e.Current;
                    }
                }
                return max;
            }),
            new AsScalar<float>(() =>
            {
                var max = float.MinValue;
                using (var e = src.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        if (e.Current > max) max = (float)e.Current;
                    }
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
            Enumerable.AsEnumerable._(src))
        { }

        /// <summary>
        /// The maximum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MaxOf(IEnumerable<long> src) : base(
            new AsScalar<double>(() =>
            {
                var max = double.MinValue;
                using (var e = src.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        if (e.Current > max) max = e.Current;
                    }
                }
                return max;
            }),
            new AsScalar<int>(() =>
            {
                var max = int.MinValue;
                using (var e = src.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        if (e.Current > max) max = (int)e.Current;
                    }
                }
                return max;
            }),
            new AsScalar<long>(() =>
            {
                var max = long.MinValue;
                using (var e = src.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        if (e.Current > max) max = e.Current;
                    }
                }
                return max;
            }),
            new AsScalar<float>(() =>
            {
                var max = float.MinValue;
                using (var e = src.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        if (e.Current > max) max = e.Current;
                    }
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
            Enumerable.AsEnumerable._(src))
        { }

        /// <summary>
        /// The maximum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MaxOf(IEnumerable<float> src) : base(
            new AsScalar<double>(() =>
            {
                var max = double.MinValue;
                using (var e = src.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        if (e.Current > max) max = e.Current;
                    }
                }
                return max;
            }),
            new AsScalar<int>(() =>
            {
                var max = int.MinValue;
                using (var e = src.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        if (e.Current > max) max = (int)e.Current;
                    }
                }
                return max;
            }),
            new AsScalar<long>(() =>
            {
                var max = long.MinValue;
                using (var e = src.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        if (e.Current > max) max = (long)e.Current;
                    }
                }
                return max;
            }),
            new AsScalar<float>(() =>
            {
                var max = float.MinValue;
                using (var e = src.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        if (e.Current > max) max = e.Current;
                    }
                }
                return max;
            })
        )
        { }
    }
}
