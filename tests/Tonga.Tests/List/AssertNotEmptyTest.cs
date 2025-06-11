using System;
using Tonga.Enumerable;
using Tonga.List;
using Xunit;

namespace Tonga.Tests.List;

public sealed class AssertNotEmptyTest
{
    [Fact]
    public void EmptyCollectionThrowsExeption()
    {
        Assert.Throws<Exception>(() =>
            new Length(
                new AssertNotEmpty<int>(
                    new AsList<int>()
                )
            ).Value()
        );
    }

    [Fact]
    public void NotEmptyCollectionThrowsNoExeption()
    {
        Assert.Equal(
            1,
            new Length(
                new AssertNotEmpty<string>(
                    new AsList<string>("test")
                )
            ).Value()
        );
    }

    [Fact]
    public void EmptyCollectionThrowsCustomExeption()
    {
        Assert.Throws<OperationCanceledException>(() =>
            new Length(
                new AssertNotEmpty<string>(
                    new AsList<string>(),
                    new OperationCanceledException()
                )
            ).Value()
        );
    }
}
