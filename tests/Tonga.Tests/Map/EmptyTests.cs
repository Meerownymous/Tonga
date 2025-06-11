using Tonga.Map;
using Xunit;

namespace Tonga.Tests.Map;

public sealed class EmptyMapTests
{
    [Fact]
    public void GenericIsEmpty()
    {
        Assert.Empty(new Empty<double, string>().Pairs());
    }
}
