using System;
using System.Collections.Generic;
using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Scalar;

public sealed class ItemAtTests
{
    [Fact]
    public void DeliversElementByPos()
    {
        Assert.Equal(
            2,
            (1, 2, 3).AsEnumerable()
            .ItemAt(1)
            .Value()
        );
    }

    [Fact]
    public void DeliversElementByPosWithFallback()
    {
        Assert.Equal(
            2,
            (1, 2, 3)
                .AsEnumerable()
                .ItemAt(1,4)
                .Value()
        );
    }

    [Fact]
    public void FailsForEmptyCollection()
    {
        Assert.Throws<ArgumentException>(() =>
            new List<int>()
                .ItemAt(0)
                .Value()
        );
    }

    [Fact]
    public void DeliversFallback()
    {
        String fallback = "fallback";
        Assert.Equal(
            fallback,
            new None<string>()
                .ItemAt(12, fallback)
                .Value()
        );
    }

    [Fact]
    public void FallbackShowsError()
    {
        Assert.Throws<InvalidOperationException>(() =>
                new None<string>()
                    .ItemAt(12,(ex, _) => throw ex)
                    .Value()
        );
    }

    [Fact]
    public void FallbackShowsGivenErrorWithPosition()
    {
        Assert.Throws<NotFiniteNumberException>(() =>
            new None<string>()
                .ItemAt(12, new NotFiniteNumberException("Cannot do this!"))
                .Value()
        );
    }

    [Fact]
    public void FallbackShowsGivenErrorForNegativePosition()
    {
        Assert.Throws<NotFiniteNumberException>(() =>
            new None<string>()
                .ItemAt(-12,new NotFiniteNumberException("Cannot do this!"))
                .Value()
        );
    }

    [Fact]
    public void SensesChanges()
    {
        var list = new List<string>{ "pre" };
        var transient = list.ItemAt(0);
        transient.Value();
        list.Clear();
        list.Add("post");

        Assert.Equal("post", transient.Value());
    }

    [Fact]
    public void DeliversLogicErrorMessage()
    {
        try
        {
            new[] { "one", "two", "three" }
                .ItemAt(3)
                .Value();
        }
        catch (Exception ex)
        {
            Assert.Equal(
                "Cannot get element at position 4: Cannot get item 4 - The enumerable has only 3 items.",
                ex.Message
            );
        }
    }
}
