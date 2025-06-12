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
            Assert.Equal(
                "dude world hello",
                ("hello", "world", "dude")
                .AsEnumerable()
                .AsReversed()
                .AsJoined(" ")
                .Str()
            );
        }
    }
}
