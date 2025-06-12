

using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text;

public sealed class JoinedTest
{
    [Fact]
    public void JoinsStrings()
    {
        Assert.Equal(
            "hello world",
            new Joined(
                " ",
                "hello",
                "world"
            ).Str()
        );
    }

    [Fact]
    public void JoinsTexts()
    {
        Assert.Equal(
            "foo bar",
            new Joined(
                " ".AsText(),
                "foo".AsText(),
                "bar".AsText()
            ).Str()
        );
    }
}
