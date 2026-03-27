

using Tonga.Fact;

namespace Tonga.Bytes;

/// <summary>
/// Equality for <see cref="IBytes"/>
/// </summary>
public sealed class IsEqual(BytesMorph left, BytesMorph right) : FactEnvelope(() =>
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
)
{
    public IsEqual(IBytes left, IBytes right) : this(new BytesMorph(left), new BytesMorph(right))
    {
    }

    public IsEqual(IBytes left, BytesMorph right) : this(new BytesMorph(left), new BytesMorph(right))
    {
    }
}
