using System.Collections;
using Tonga.Collection;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Collection
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
