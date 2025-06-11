

using Tonga.Fact;

namespace Tonga.Bytes;

/// <summary>
/// Equality for <see cref="IBytes"/>
/// </summary>
public sealed class IsEqual(IBytes left, IBytes right) : FactEnvelope(
    () =>
    {
        var leftByte = left.Raw();
        var rightByte = right.Raw();
        var equal = leftByte.Length == rightByte.Length;

        for (var i = 0; i < leftByte.Length && equal; i++)
        {
            if (leftByte[i] != rightByte[i])
            {
                equal = false;
                break;
            }
        }
        return equal;
    }
);
