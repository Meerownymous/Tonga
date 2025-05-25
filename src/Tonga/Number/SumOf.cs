

using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.Number
{
    /// <summary>
    /// Calculated sum of numbers.
    /// </summary>
    public sealed class SumOf : NumberEnvelope
    {
        /// <summary>
        /// A sum of floats
        /// </summary>
        /// <param name="src">source floats</param>
        public SumOf(params float[] src) : this(src.AsEnumerable())
        { }

        /// <summary>
        /// A sum of longs
        /// </summary>
        /// <param name="src">source longs</param>
        public SumOf(params long[] src) : this(
            src.AsEnumerable()
        )
        { }

        /// <summary>
        /// A sum of ints
        /// </summary>
        /// <param name="src">source ints</param>
        public SumOf(params int[] src) : this(src.AsEnumerable())
        { }

        /// <summary>
        /// A sum of doubles
        /// </summary>
        /// <param name="src">source doubles</param>
        public SumOf(params double[] src) : this(
            src.AsEnumerable()
        )
        { }

        /// <summary>
        /// A sum of doubles
        /// </summary>
        /// <param name="src">source doubles</param>
        public SumOf(IEnumerable<double> src) : base(
            new AsScalar<double>(() =>
            {
                double sum = 0D;
                foreach (int val in src)
                {
                    sum += val;
                }
                return sum;
            }),
            new AsScalar<int>(() =>
            {
                int sum = 0;
                foreach (int val in src)
                {
                    sum += val;
                }
                return sum;
            }),
            new AsScalar<long>(() =>
            {
                long sum = 0L;
                foreach (long val in src)
                {
                    sum += val;
                }
                return sum;
            }),
            new AsScalar<float>(() =>
            {
                float sum = 0F;
                foreach (float val in src)
                {
                    sum += val;
                }
                return sum;
            }))
        { }

        /// <summary>
        /// A sum of integers
        /// </summary>
        /// <param name="src">source integers</param>
        public SumOf(IEnumerable<int> src) : base(
            new AsScalar<double>(() =>
            {
                double sum = 0D;
                foreach (int val in src)
                {
                    sum += val;
                }
                return sum;
            }),
            new AsScalar<int>(() =>
            {
                int sum = 0;
                foreach (int val in src)
                {
                    sum += val;
                }
                return sum;
            }),
            new AsScalar<long>(() =>
            {
                long sum = 0L;
                foreach (long val in src)
                {
                    sum += val;
                }
                return sum;
            }),
            new AsScalar<float>(() =>
            {
                float sum = 0F;
                foreach (float val in src)
                {
                    sum += val;
                }
                return sum;
            }))
        { }

        /// <summary>
        /// A sum of longs
        /// </summary>
        /// <param name="src">source longs</param>
        public SumOf(IEnumerable<long> src) : base(
            new AsScalar<double>(() =>
            {
                double sum = 0D;
                foreach (int val in src)
                {
                    sum += val;
                }
                return sum;
            }),
            new AsScalar<int>(() =>
            {
                int sum = 0;
                foreach (int val in src)
                {
                    sum += val;
                }
                return sum;
            }),
            new AsScalar<long>(() =>
            {
                long sum = 0L;
                foreach (long val in src)
                {
                    sum += val;
                }
                return sum;
            }),
            new AsScalar<float>(() =>
            {
                float sum = 0F;
                foreach (float val in src)
                {
                    sum += val;
                }
                return sum;
            }))
        { }

        /// <summary>
        /// A sum of floats
        /// </summary>
        /// <param name="src">source floats</param>
        public SumOf(IEnumerable<float> src) : base(
            new AsScalar<double>(() =>
            {
                double sum = 0D;
                foreach (int val in src)
                {
                    sum += val;
                }
                return sum;
            }),
            new AsScalar<int>(() =>
            {
                int sum = 0;
                foreach (int val in src)
                {
                    sum += val;
                }
                return sum;
            }),
            new AsScalar<long>(() =>
            {
                long sum = 0L;
                foreach (long val in src)
                {
                    sum += val;
                }
                return sum;
            }),
            new AsScalar<float>(() =>
            {
                float sum = 0F;
                foreach (float val in src)
                {
                    sum += val;
                }
                return sum;
            }))
        { }
    }
}
