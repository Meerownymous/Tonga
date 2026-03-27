

using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text;

public sealed class UpperTest
{
    [Fact]
    public void ConvertsText()
    {
        AssertText.Equal(
            "HELLO!",
            "Hello!".AsUpper()
        );
    }

}
