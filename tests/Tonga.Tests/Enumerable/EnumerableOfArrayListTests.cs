using System.Collections;
using Tonga.Enumerable;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public class EnumerableOfArrayListTests
    {
        [Fact]
        public void BuildsFromStrings()
        {
            var arr = new ArrayList() { "A", "B", "C" };

            Assert.Equal(
                "A",
                First._(
                    new EnumerableOfArrayList(arr)
                ).Value()
            );
        }
    }
}
