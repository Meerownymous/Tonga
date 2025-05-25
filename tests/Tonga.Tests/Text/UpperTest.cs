

using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text;

public sealed class UpperTest
{
    [Fact]
    public void ConvertsText()
    {
        Assert.Equal(
            "HELLO!",
            new Upper(AsText._("Hello!")).Str()
        );
    }

}
