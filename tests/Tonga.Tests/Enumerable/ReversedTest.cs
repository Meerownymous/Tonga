using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public class ReversedTest
    {
        [Fact]
        public void ReversesIterable()
        {
            Assert.Equal(
                "dude world hello",
                new global::Tonga.Text.Joined(
                    " ",
                    new Reversed<string>(
                        AsEnumerable._(
                            "hello", "world", "dude"
                        )
                    )
                ).Str()
            );
        }
    }
}
