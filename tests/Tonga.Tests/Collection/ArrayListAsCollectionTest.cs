

using System.Collections;
using Xunit;
using Tonga.Collection;

namespace Tonga.Enumerable.Test
{
    public class ArrayListAsCollectionTest
    {
        [Fact]
        public void BuildsFromStrings()
        {
            var arr = new ArrayList() { "A", "B", "C" };

            Assert.Equal(
                "A",
                new ItemAt<object>(
                    new ArrayListAsCollection(arr)
                )
                .Value()
                .ToString()
            );
        }
    }
}
