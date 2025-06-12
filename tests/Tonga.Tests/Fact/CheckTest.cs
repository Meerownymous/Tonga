using Tonga.Enumerable;
using Tonga.Fact;
using Xunit;

namespace Tonga.Tests.Fact;

public sealed class CheckTest
{
    [Fact]
    public void IsTrueWhenAllTrue()
    {
        Assert.True(
            new Check(
                new True(),
                new True(),
                new True()
            ).IsTrue()
        );
    }

    [Fact]
    public void IsFalseWhenOneFalse()
    {
        Assert.True(
            new Check(
                new True(),
                new False(),
                new True()
            ).IsFalse()
        );
    }

    [Fact]
    public void IsFalseOnAllFalse()
    {
        Assert.False(
            new Check(
                (
                    new False(),
                    new False(),
                    new False()
                ).AsEnumerable()
            ).IsTrue()
        );
    }

    [Fact]
    public void IsTrueOnEmptyEnumerable()
    {
        Assert.True(
            new Check(
                    new None<IFact>()
            )
            .IsTrue()
        );
    }
}
