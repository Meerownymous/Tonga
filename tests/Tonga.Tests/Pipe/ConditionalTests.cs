using Tonga.Fact;
using Tonga.Pipe;
using Xunit;

namespace Tonga.Tests.Pipe;

public class ConditionalTests
{
    [Fact]
    public void ConditionalYieldsIfYesWhenConditionIsTrue() =>
        Assert.Equal(
            "yes",
            new Conditional<int, string>(
                _ => true,
                _ => "yes",
                _ => "no"
            ).Yield(0)
        );

    [Fact]
    public void ConditionalYieldsIfNoWhenConditionIsFalse() =>
        Assert.Equal(
            "no",
            new Conditional<int, string>(
                _ => false,
                _ => "yes",
                _ => "no"
            ).Yield(0)
        );

    [Fact]
    public void ConditionalWithFactYieldsIfYesWhenFactIsTrue()
        => Assert.Equal(
            "yes",
            new Conditional<int, string>(
                new True(),
                _ => "yes",
                _ => "no"
            ).Yield(0)
        );

    [Fact]
    public void ConditionalWithFactYieldsIfNoWhenFactIsFalse() =>
        Assert.Equal(
            "no",
            new Conditional<int, string>(
                new False(),
                _ => "yes",
                _ => "no"
            ).Yield(0)
        );

    [Fact]
    public void ConditionalWithPipeYieldsIfYes() =>
        Assert.Equal(
            "A",
            new Conditional<int, string>(
                _ => true,
                new AsPipe<int, string>("A"),
                new AsPipe<int, string>("B")
            ).Yield(0)
        );

    [Fact]
    public void ConditionalWithPipeYieldsIfNo() =>
        Assert.Equal(
            "B",
            new Conditional<int, string>(
                _ => false,
                new AsPipe<int, string>("A"),
                new AsPipe<int, string>("B")
            ).Yield(0)
        );

    [Fact]
    public void ConditionalWithFactAndPipesYieldsIfYes() =>
        Assert.Equal(
            "X",
            new Conditional<int, string>(
                new True(),
                new AsPipe<int, string>("X"),
                new AsPipe<int, string>("Y")
            ).Yield(0)
        );

    [Fact]
    public void ConditionalWithFactAndPipesYieldsIfNo()
        => Assert.Equal("Y", new Conditional<int, string>(
                new False(),
                new AsPipe<int, string>("X"),
                new AsPipe<int, string>("Y")
            ).Yield(0)
        );
}
