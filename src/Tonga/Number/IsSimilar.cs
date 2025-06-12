

using System;
using Tonga.Fact;

namespace Tonga.Number;

/// <summary>
/// Checks if two Numbers are similar with a given accuracy
/// </summary>
public sealed class IsSimilar(INumber first, INumber second, int accuracy = 0) : FactEnvelope(() =>
    {
        bool isSimilar;
        if (accuracy > 0)
        {
            isSimilar =
                Math.Abs(
                    first.Double() - second.Double()
                ) <
                Math.Pow(
                    10, -1 * accuracy
                );
        }
        else
        {
            isSimilar =
                Math.Abs(
                    first.Double() - second.Double()
                ) == 0;
        }


        return isSimilar;

    }
);

public static partial class NumberSmarts
{
    public static IFact IsSimilar(this INumber first, INumber second, int accuracy = 0) =>
        new IsSimilar(first, second, accuracy);
}
