using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Number;

/// <summary>
/// Average of numbers.
/// </summary>
public sealed class Average : NumberEnvelope
{
    public Average(params int[] src) : this(src.AsMapped(x => (double)x)) { }

    public Average(params long[] src) : this(src.AsMapped(x => (double)x)) { }

    public Average(params float[] src) : this(src.AsMapped(x => (double)x)) { }

    public Average(params double[] src) : this((IEnumerable<double>)src) { }

    public Average(IEnumerable<int> src) : this(src.AsMapped(x => (double)x)) { }

    public Average(IEnumerable<long> src) : this(src.AsMapped(x => (double)x)) { }

    public Average(IEnumerable<float> src) : this(src.AsMapped(x => (double)x)) { }

    public Average(IEnumerable<double> src) : base(
        () =>
        {
            double sum = 0D, total = 0D;
            foreach (var val in src) { sum += val; total++; }
            return sum / (total == 0D ? 1D : total);
        },
        () =>
        {
            int sum = 0, total = 0;
            foreach (var val in src) { sum += (int)val; total++; }
            return sum / (total == 0 ? 1 : total);
        },
        () =>
        {
            long sum = 0, total = 0;
            foreach (var val in src) { sum += (long)val; total++; }
            return sum / (total == 0L ? 1L : total);
        },
        () =>
        {
            float sum = 0F, total = 0F;
            foreach (var val in src) { sum += (float)val; total++; }
            return sum / (total == 0F ? 1F : total);
        }
    )
    { }
}
