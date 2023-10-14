

using Xunit;

namespace Tonga.Text.Test
{
    public sealed class JoinedTest
    {
        [Fact]
        public void JoinsStrings()
        {
            Assert.True(
                new Joined(
                    " ",
                    "hello",
                    "world"
                ).AsString() == "hello world"
            );
        }

        [Fact]
        public void JoinsTexts()
        {
            Assert.True(
                new Joined(
                    new LiveText(" "),
                    new LiveText("foo"),
                    new LiveText("bar")
                ).AsString() == "foo bar"
            );
        }
    }
}
