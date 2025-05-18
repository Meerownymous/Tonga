using System;
using Tonga.Fact;
using Tonga.Pipe;
using Xunit;

namespace Tonga.Tests.Pipe;

public class MuxTests
{
    [Fact]
    public void MuxOutputsFallbackWhenNoConditionMatches() =>
        Assert.Equal(
            "fallback",
            new Mux<int, string>(
                [(_ => false, _ => "nope")],
                _ => "fallback"
            ).Yield(42)
        );

    [Fact]
    public void MuxOutputsMatchingConsequence()
        => Assert.Equal(
            "yes",
            new Mux<int, string>(
            [
                (_ => false, _ => "no"),
                (_ => true, _ => "yes"),
                (_ => true, _ => "late")
            ],
            _ => "fallback"
        ).Yield(1)
    );

    [Fact]
    public void MuxThrowsWhenNoMatchAndNoFallbackProvided() =>
        Assert.Throws<ArgumentException>(
            () => new Mux<int, string>(
                (_ => false, _ => "x")
            ).Yield(9)
        );

    [Fact]
    public void MuxWithOnlyParamsMatchesCorrectly() =>
        Assert.Equal(
            "matched",
            new Mux<int, string>(
                (x => x == 3, _ => "matched")
            ).Yield(3)
        );

    [Fact]
    public void MuxWithIFactEvaluatesCondition()
        => Assert.Equal(
            "ok",
            new Mux<int, string>(
                (new True(), _ => "ok")
            ).Yield(5)
        );

    [Fact]
    public void MuxWithIFactAndFallbackEvaluatesFallback() =>
        Assert.Equal("fb", new Mux<int, string>(
            [(new False(), _ => "never")],
            _ => "fb"
        ).Yield(0)
    );

    [Fact]
    public void MuxRejectsWhenConstructedWithOnlyPathsAndNoMatchOccurs() =>
        Assert.Throws<ArgumentException>(() =>
            new Mux<int, string>(
                (_ => false, _ => "nope")
            ).Yield(123)
        );

    [Fact]
    public void MuxWithFactAndConsequenceYieldsFirstMatchingValue()
        => Assert.Equal(
            "yes",
            new Mux<int, string>(
                (new False(), () => "no"),
                (new True(), () => "yes")
            ).Yield(0)
        );

    [Fact]
    public void MuxWithFactAndConsequenceThrowsWhenNoneMatch()
        => Assert.Throws<ArgumentException>(() => new Mux<int, string>(
            (new False(), () => "no")
        ).Yield(1));

    [Fact]
    public void MuxWithFactAndConsequenceUsesFallbackWhenProvided() =>
        Assert.Equal(
            "fallback",
            new Mux<int, string>(
                [(new False(), () => "x")],
                () => "fallback"
            ).Yield(99)
        );

    [Fact]
    public void MuxWithFactAndFixedConsequenceThrowsWhenNoneMatch() =>
        Assert.Throws<ArgumentException>(() =>
            new Mux<int, string>(
                (new False(), "never")
            ).Yield(42)
        );
}
