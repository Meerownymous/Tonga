using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class ReplacedTest
    {
        [Fact]
        public void ReplacesElementAtIndex()
        {
            Assert.Equal(
                "F",
                ( "A", "B", "C", "D", "E" )
                    .AsEnumerable()
                    .AsReplaced(2, "F")
                    .ItemAt(2)
                    .Value()
            );
        }
    }
}
