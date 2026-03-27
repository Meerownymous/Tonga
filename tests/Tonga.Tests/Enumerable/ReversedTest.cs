using Tonga.Enumerable;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public class ReversedTest
    {
        [Fact]
        public void ReversesIterable()
        {
            AssertText.Equal(
                "dude world hello",
                new Joined(" ", ("hello", "world", "dude")
                    .AsEnumerable()
                    .AsReversed()
                )
            );
        }
    }
}
