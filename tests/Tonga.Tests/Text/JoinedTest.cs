

using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
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
                ).Str() == "hello world"
            );
        }

        [Fact]
        public void JoinsTexts()
        {
            Assert.True(
                new Joined(
                    AsText._(" "),
                    AsText._("foo"),
                    AsText._("bar")
                ).Str() == "foo bar"
            );
        }
    }
}
