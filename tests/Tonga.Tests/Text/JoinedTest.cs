

using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text;

public sealed class JoinedTest
{
    [Fact]
    public void JoinsStrings()
    {
        AssertText.Equal(
            "hello world",
            new Joined(
                " ",
                "hello",
                "world"
            )
        );
    }

    [Fact]
    public void JoinsTexts()
    {
        Assert.Equal(
            "foo bar",
            new Joined(
                " ",
                "foo",
                "bar"
            ).Str()
        );
    }
}
