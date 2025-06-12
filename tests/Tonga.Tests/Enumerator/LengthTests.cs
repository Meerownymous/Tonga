using Tonga.Enumerable;
using Xunit;
using Length = Tonga.Enumerator.Length;

namespace Tonga.Tests.Enumerator;

public sealed class LengthTests
{
    [Fact]
    public void Counts()
    {
        Assert.Equal(
            5,
            new Length(
                (1, 2, 3, 4, 5)
                    .AsEnumerable()
                    .GetEnumerator()
            ).Value()
        );
    }
}
