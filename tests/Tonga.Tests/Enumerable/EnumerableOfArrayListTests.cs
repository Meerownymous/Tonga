

using System.Collections;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Enumerable.Test
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
                    new EnumerabeOfArrayList(arr)
                ).Value()
            );
        }
    }
}
