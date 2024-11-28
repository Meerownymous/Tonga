using Tonga.Optional;
using Xunit;

namespace Tonga.Optional.Tests;

public sealed class OptFullTests
{
    [Fact]
    public void DeliversValue()
    {
        Assert.Equal(
            1910,
            new OptFull<int>(1910).Value()
        );
    }

    [Fact]
    public void PerformsHasAction()
    {
        int result = 0;
        new OptFull<int>(1910).IfHas((value) => result = value);
        Assert.Equal(1910, result);
    }

    [Fact]
    public void DoesNotPerformsNotAction()
    {
        bool result = false;
        new OptFull<int>(1910).IfNot(() => result = true);
        Assert.False(result);
    }
}
