

using Tonga.Fact;

namespace Tonga.Bytes;

/// <summary>
/// Equality for <see cref="IBytes"/>
/// </summary>
public sealed class IsEqual(IBytes left, IBytes right) : FactEnvelope(
    () =>
    {
        var leftByte = left.Bytes();
        var rightByte = right.Bytes();
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
