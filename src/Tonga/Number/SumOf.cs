

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
        public SumOf(params float[] src) : this(
            new ManyOf<float>(src))
        { }

        /// <summary>
        /// A sum of longs
        /// </summary>
        /// <param name="src">source longs</param>
        public SumOf(params long[] src) : this(
            new ManyOf<long>(src))
        { }

        /// <summary>
        /// A sum of ints
        /// </summary>
        /// <param name="src">source ints</param>
        public SumOf(params int[] src) : this(
            new ManyOf<int>(src))
        { }

        /// <summary>
        /// A sum of doubles
        /// </summary>
        /// <param name="src">source doubles</param>
        public SumOf(params double[] src) : this(
            new ManyOf<double>(src))
        { }

        /// <summary>
        /// A sum of doubles
        /// </summary>
        /// <param name="src">source doubles</param>
        public SumOf(IEnumerable<double> src) : base(
            new ScalarOf<double>(() =>
            {
                double sum = 0D;
                foreach (int val in src)
                {
                    sum += (double)val;
                }
                return sum;
            }),
            new ScalarOf<int>(() =>
            {
                int sum = 0;
                foreach (int val in src)
                {
                    sum += (int)val;
                }
                return sum;
            }),
            new ScalarOf<long>(() =>
            {
                long sum = 0L;
                foreach (long val in src)
                {
                    sum += (long)val;
                }
                return sum;
            }),
            new ScalarOf<float>(() =>
            {
                float sum = 0F;
                foreach (float val in src)
                {
                    sum += (float)val;
                }
                return sum;
            }))
        { }

        /// <summary>
        /// A sum of integers
        /// </summary>
        /// <param name="src">source integers</param>
        public SumOf(IEnumerable<int> src) : base(
            new ScalarOf<double>(() =>
            {
                double sum = 0D;
                foreach (int val in src)
                {
                    sum += (double)val;
                }
                return sum;
            }),
            new ScalarOf<int>(() =>
            {
                int sum = 0;
                foreach (int val in src)
                {
                    sum += (int)val;
                }
                return sum;
            }),
            new ScalarOf<long>(() =>
            {
                long sum = 0L;
                foreach (long val in src)
                {
                    sum += (long)val;
                }
                return sum;
            }),
            new ScalarOf<float>(() =>
            {
                float sum = 0F;
                foreach (float val in src)
                {
                    sum += (float)val;
                }
                return sum;
            }))
        { }

        /// <summary>
        /// A sum of longs
        /// </summary>
        /// <param name="src">source longs</param>
        public SumOf(IEnumerable<long> src) : base(
            new ScalarOf<double>(() =>
            {
                double sum = 0D;
                foreach (int val in src)
                {
                    sum += (double)val;
                }
                return sum;
            }),
            new ScalarOf<int>(() =>
            {
                int sum = 0;
                foreach (int val in src)
                {
                    sum += (int)val;
                }
                return sum;
            }),
            new ScalarOf<long>(() =>
            {
                long sum = 0L;
                foreach (long val in src)
                {
                    sum += (long)val;
                }
                return sum;
            }),
            new ScalarOf<float>(() =>
            {
                float sum = 0F;
                foreach (float val in src)
                {
                    sum += (float)val;
                }
                return sum;
            }))
        { }

        /// <summary>
        /// A sum of floats
        /// </summary>
        /// <param name="src">source floats</param>
        public SumOf(IEnumerable<float> src) : base(
            new ScalarOf<double>(() =>
            {
                double sum = 0D;
                foreach (int val in src)
                {
                    sum += (double)val;
                }
                return sum;
            }),
            new ScalarOf<int>(() =>
            {
                int sum = 0;
                foreach (int val in src)
                {
                    sum += (int)val;
                }
                return sum;
            }),
            new ScalarOf<long>(() =>
            {
                long sum = 0L;
                foreach (long val in src)
                {
                    sum += (long)val;
                }
                return sum;
            }),
            new ScalarOf<float>(() =>
            {
                float sum = 0F;
                foreach (float val in src)
                {
                    sum += (float)val;
                }
                return sum;
            }))
        { }
    }
}
