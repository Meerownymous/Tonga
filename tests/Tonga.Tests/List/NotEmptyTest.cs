using System;
using Tonga.List;
using Tonga.Scalar;
using Xunit;
using NotEmpty = Tonga.List.NotEmpty;

namespace Tonga.Tests.List;

public sealed class NotEmptyTest
{
    [Fact]
    public void EmptyCollectionThrowsExeption()
    {
        Assert.Throws<Exception>(() =>
            Length._(
                NotEmpty._(
                    AsList._()
                )
            ).Value()
        );
    }

    [Fact]
    public void NotEmptyCollectionThrowsNoExeption()
    {
        Assert.Equal(
            1,
            Length._(
                NotEmpty._(
                    AsList._("test")
                )
            ).Value()
        );
    }

    [Fact]
    public void EmptyCollectionThrowsCustomExeption()
    {
        Assert.Throws<OperationCanceledException>(() =>
            Length._(
                NotEmpty._(
                    AsList._(),
                    new OperationCanceledException()
                )
            ).Value()
        );
    }
}
