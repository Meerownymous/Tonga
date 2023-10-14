

using Xunit;
using Tonga.Text;

namespace Tonga.Enumerable.Test
{
    public class ReversedTest
    {
        [Fact]
        public void ReversesIterable()
        {
            Assert.True(
                new Text.Joined(
                    " ",
                    new Reversed<string>(
                        new ManyOf<string>(
                            "hello", "world", "dude"
                        )
                    )
                ).AsString() == "dude world hello");
        }
    }
}
