using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Fact;
using Xunit;

namespace Tonga.Tests.Fact;

public sealed class AndTest
{
    [Fact]
    public void IsTrueOnAllTrue()
    {
        Assert.True(
            new And(
                new True(),
                new True(),
                new True()
            ).IsTrue()
        );
    }

    [Fact]
    public void IsFalseOnOneFalse()
    {
        Assert.True(
            new And(
                new True(),
                new False(),
                new True()
            ).IsFalse()
        );
    }

    [Fact]
    public void AllFalse()
    {
        Assert.False(
            new And(
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
            new And(new None<IFact>())
                .IsTrue()
        );
    }

    [Fact]
    public void IsTrueOnBooleans()
    {
        Assert.True(
            new And(true, true, true).IsTrue()
        );
    }

    [Fact]
    public void IsFalseOnOneBooleanFalse()
    {
        Assert.False(
            new And(
                new List<bool> { true, false, true }
            ).IsTrue()
        );
    }
}
