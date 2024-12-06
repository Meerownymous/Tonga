

using System.Collections;
using Xunit;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.List.Tests
{
    public class ManyOfArrayListTest
    {
        [Fact]
        public void BuildsFromStrings()
        {
            var arr = new ArrayList() { "A", "B", "C" };

            Assert.Equal("A",
                new ItemAt<object>(
                    new EnumerableOfArrayList(arr)
                ).Value().ToString()
            );
        }
    }
}
